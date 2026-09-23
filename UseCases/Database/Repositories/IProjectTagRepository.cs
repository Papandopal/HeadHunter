using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace UseCases.Database.Repositories
{
    public interface IProjectTagRepository : IRepository<ProjectTag>
    {
        public void Update(ProjectTag tag);
        public void UpdateRange(IEnumerable<ProjectTag> tags);
        public IEnumerable<string> GetNamesByPrefix(string? prefix);
        public IEnumerable<ProjectTag> GetPopularTags(uint limit);
        public IEnumerable<ProjectTag> GetByNamesOrDefault(IEnumerable<string> names);  
    }
}
