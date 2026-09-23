using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace UseCases.Services.ProjectTagServices.Interfaces
{
    public interface IProjectTagService
    {
        public void Update(ProjectTag tag);
        public void UpdateRange(IEnumerable<ProjectTag> tags);
        public IEnumerable<ProjectTag> GetFromJSON(string json);
        public IEnumerable<ProjectTag> GetPopularTags(uint limit);
        public IEnumerable<ProjectTag> GetByNamesOrDefault(IEnumerable<string> names);
        public IEnumerable<string> GetNamesByPrefix(string? prefix);
    }
}
