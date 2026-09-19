using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using UseCases.Database;
using UseCases.Services.ProjectServices.DTOs;
using UseCases.Services.ProjectServices.Interfaces;
using UseCases.Services.ProjectTagServices.Interfaces;

namespace UseCases.Services.ProjectServices
{
    public class ProjectService(IUnitOfWork unitOfWork, IProjectTagService projectTagService) : IProjectService
    {
        void IProjectService.Add(AddProjectDTO projectDTO, Guid ownerId)
        {
            var project = new Project
            {
                Title = projectDTO.Title,
                Description = projectDTO.Description,
                DatePeriod = projectDTO.DatePeriod,
                ProjectTags = projectDTO.ProjectTags,
                OwnerId = ownerId
            };
            unitOfWork.StartTransaction();
            unitOfWork.ProjectRepository.Add(project);
            unitOfWork.Commit();
        }

        void IProjectService.AddRange(IEnumerable<AddProjectDTO> projectDTOs, Guid ownerId)
        {
            List<Project> newProjects = new();

            foreach (var projectDTO in projectDTOs)
            {
                var project = new Project
                {
                    Title = projectDTO.Title,
                    Description = projectDTO.Description,
                    DatePeriod = projectDTO.DatePeriod,
                    ProjectTags = projectDTO.ProjectTags,
                    OwnerId = ownerId
                };
                newProjects.Add(project);
            }
            unitOfWork.StartTransaction();
            unitOfWork.ProjectRepository.AddRange(newProjects);
            unitOfWork.Commit();
        }

        void IProjectService.Update(EditProjectDTO projectDTO)
        {
            var project = unitOfWork.ProjectRepository.GetById(projectDTO.Id);
            project.Title = projectDTO.Title ?? project.Title;
            project.Description = projectDTO.Description ?? project.Description;    
            project.DatePeriod = projectDTO.DatePeriod ?? project.DatePeriod;
            project.ProjectTags = projectDTO.ProjectTags ?? project.ProjectTags;
            unitOfWork.StartTransaction();
            unitOfWork.ProjectRepository.Update(project);
            unitOfWork.Commit();
        }

        void IProjectService.ChangeCurrentProjects(IEnumerable<EditProjectDTO> projectDTOs, Guid ownerId)
        {
            IEnumerable<Project> projects = unitOfWork.ProjectRepository.GetByIds(projectDTOs.Select(x => x.Id));
            var dto = projectDTOs.GetEnumerator();
            foreach(var project in projects)
            {
                dto.MoveNext();
                project.Title = dto.Current.Title ?? project.Title;
                project.Description = dto.Current.Description ?? project.Description;
                project.DatePeriod = dto.Current.DatePeriod ?? project.DatePeriod;
                project.ProjectTags = dto.Current.ProjectTags ?? project.ProjectTags;
            }

            var deletedProjects = unitOfWork.ProjectRepository.GetAllExceptOf(projectDTOs.Select(x => x.Id), ownerId);

            unitOfWork.StartTransaction();
            unitOfWork.ProjectRepository.UpdateRange(projects);
            unitOfWork.ProjectRepository.DeleteRange(deletedProjects);
            unitOfWork.Commit();    
        }

        private AddProjectDTO BuildAddDTO(IEnumerable<ProjectRecordDTO> dtos)
        {
            bool sourceHaveMinimumRecords = dtos.Count() == 3;
            bool sourceHaveProjectTags = dtos.Count() == 4;
            if (!sourceHaveMinimumRecords && !sourceHaveProjectTags) throw new Exception("Invalide date for build \"Project\"");
            string title = dtos.Where(x => x.PropName.ToLower() == "title").Select(x => x.PropValue).First();
            string description = dtos.Where(x => x.PropName.ToLower() == "description").Select(x => x.PropValue).First();
            var datePeriod = dtos.Where(x => x.PropName.ToLower() == "dateperiod").Select(x => x.PropValue).First();
            IEnumerable<ProjectTag> projectTags = Enumerable.Empty<ProjectTag>();
            if (sourceHaveProjectTags)
            {
                string projectTagsJSON = dtos.Where(x => x.PropName.ToLower() == "projecttags").Select(x => x.PropValue).First();
                projectTags = projectTagService.GetFromJSON(projectTagsJSON);
            }

            return new AddProjectDTO
            {
                Title = title,
                Description = description,
                DatePeriod = datePeriod,
                ProjectTags = projectTags
            };
        }

        private EditProjectDTO BuildEditDTO(IEnumerable<ProjectRecordDTO> dtos)
        {
            Guid id;
            try
            {
                id = Guid.Parse(dtos.Where(x => x.PropName.ToLower() == "id").First().PropValue);

            }
            catch
            {
                throw new Exception("Invalide date for build \"Project\"");
            }
            var result = new EditProjectDTO { Id = id };
            result.Title = dtos.Where(x => x.PropName.ToLower() == "title").Select(x => x.PropValue).FirstOrDefault();
            result.Description = dtos.Where(x => x.PropName.ToLower() == "description").Select(x => x.PropValue).FirstOrDefault();
            result.DatePeriod = dtos.Where(x => x.PropName.ToLower() == "dateperiod").Select(x => x.PropValue).FirstOrDefault();
            string? projectTagsJSON = dtos.Where(x => x.PropName.ToLower() == "projecttags").Select(x => x.PropValue).FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(projectTagsJSON)) result.ProjectTags = projectTagService.GetFromJSON(projectTagsJSON);
            return result;
        }

        IEnumerable<EditProjectDTO> IProjectService.DeserializeEditDTOs(IEnumerable<ProjectRecordDTO> dtos)
        {
            if (dtos.Count() == 0) return Enumerable.Empty<EditProjectDTO>();
            List<EditProjectDTO> result = new();
            IEnumerable<IEnumerable<ProjectRecordDTO>> groups = dtos.GroupBy(x => x.ProjectIndex).ToList();

            foreach (var group in groups)
            {
                List<ProjectRecordDTO> items = group.Select(x => x).ToList();

                items.Add(new ProjectRecordDTO
                {
                    ProjectIndex = items.First().ProjectIndex,
                    PropName = "Id",
                    PropValue = items.First().ProjectIndex
                });
                result.Add(BuildEditDTO(items));
            }
            return result;
        }

        IEnumerable<AddProjectDTO> IProjectService.DeserializeToAddDTOs(IEnumerable<ProjectRecordDTO> dtos)
        {
            if (dtos.Count() == 0) return Enumerable.Empty<AddProjectDTO>();
            List<AddProjectDTO> result = new();
            IEnumerable<IEnumerable<ProjectRecordDTO>> groups = dtos.GroupBy(x => x.ProjectIndex).ToList();
            foreach (var group in groups)
            {
                result.Add(BuildAddDTO(group.Select(x => x).ToList()));
            }
            return result;
        }

        IEnumerable<Project> IProjectService.GetByOwnerId(Guid ownerId)
        {
            return unitOfWork.ProjectRepository.GetByOwnerId(ownerId);
        }
    }
}
