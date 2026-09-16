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
            var names = json.Split(Separators.ProjectTagsSeparator);
            return names.Select(x => new ProjectTag { Name = x });
        }
    }
}
