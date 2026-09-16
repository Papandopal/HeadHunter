using System;
using System.Collections.Generic;
using System.Linq;
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
                DatePeroid = projectDTO.DatePeriod,
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
                    DatePeroid = projectDTO.DatePeriod,
                    ProjectTags = projectDTO.ProjectTags,
                    OwnerId = ownerId
                };
                newProjects.Add(project);
            }
            unitOfWork.StartTransaction();
            unitOfWork.ProjectRepository.AddRange(newProjects);
            unitOfWork.Commit();    
        }

        private AddProjectDTO Build(IEnumerable<ProjectRecordDTO> dtos)
        {
            bool sourceHaveMinimumRecords = dtos.Count() == 3;
            bool sourceHaveProjectTags = dtos.Count() == 4;
            if (!sourceHaveMinimumRecords && !sourceHaveProjectTags) throw new Exception("Invalide date for build \"Project\"");
            string title = dtos.Where(x => x.PropName.ToLower() == "title").Select(x => x.PropValue).First();
            string description = dtos.Where(x => x.PropName.ToLower() == "description").Select(x => x.PropValue).First();
            string datePeriod = dtos.Where(x => x.PropName.ToLower() == "dateperiod").Select(x => x.PropValue).First();
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

        IEnumerable<AddProjectDTO> IProjectService.Deserialize(IEnumerable<ProjectRecordDTO> dtos)
        {
            List<AddProjectDTO> result = new();
            IEnumerable<IEnumerable<ProjectRecordDTO>> groups = dtos.GroupBy(x => x.ProjectIndex).ToList();
            foreach (var group in groups)
            {
                result.Add(Build(group.Select(x=>x).ToList()));
            }
            return result;
        }

        IEnumerable<Project> IProjectService.GetByOwnerId(Guid ownerId)
        {
            return unitOfWork.ProjectRepository.GetByOwnerId(ownerId);
        }
    }
}
