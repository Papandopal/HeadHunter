using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.Entities;
using UseCases.Database;
using UseCases.Services.ProjectTagServices.Interfaces;

namespace UseCases.Services.ProjectTagServices
{
    public class ProjectTagService(IUnitOfWork unitOfWork) : IProjectTagService
    {
        IEnumerable<string> IProjectTagService.GetNamesByPrefix(string? prefix)
        {
            return unitOfWork.ProjectTagRepository.GetNamesByPrefix(prefix);
        }

        IEnumerable<ProjectTag> IProjectTagService.GetFromJSON(string json)
        {
            if(string.IsNullOrWhiteSpace(json)) return Enumerable.Empty<ProjectTag>();      
            var names = json.Split(Separators.ProjectTagsSeparator).Where(x=>!string.IsNullOrWhiteSpace(x));
            return names.Select(x => new ProjectTag { Name = x });
        }

        void IProjectTagService.Update(ProjectTag tag)
        {
            unitOfWork.StartTransaction();
            unitOfWork.ProjectTagRepository.Update(tag);
            unitOfWork.Commit();
        }

        void IProjectTagService.UpdateRange(IEnumerable<ProjectTag> tags)
        {
            unitOfWork.StartTransaction();
            unitOfWork.ProjectTagRepository.UpdateRange(tags);
            unitOfWork.Commit();
        }

        IEnumerable<ProjectTag> IProjectTagService.GetPopularTags(uint limit)
        {
            return unitOfWork.ProjectTagRepository.GetPopularTags(limit);
        }

        IEnumerable<ProjectTag> IProjectTagService.GetByNamesOrDefault(IEnumerable<string> names)
        {
            return unitOfWork.ProjectTagRepository.GetByNamesOrDefault(names);
        }
    }
}
