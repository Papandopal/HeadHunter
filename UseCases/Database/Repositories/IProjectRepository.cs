using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace UseCases.Database.Repositories
{
    public interface IProjectRepository : IRepository<Project>
    {
        public void AddRange(IEnumerable<Project> projects);
        public void UpdateRange(IEnumerable<Project> projects);
        public void DeleteRange(IEnumerable<Project> projects); 
        public void RemoveAll();
        public IEnumerable<Project> GetByOwnerId(Guid ownerId);
        public IEnumerable<Project> GetPersonaledProjectsByTags(Guid ownerId, IEnumerable<ProjectTag> tags, uint limit);
        public IEnumerable<Project> GetByIds(IEnumerable<Guid> ids);
        public IEnumerable<Project> GetAllByOwnerIdExceptOf(Guid ownerId, IEnumerable<Guid> ids, uint limit = 0);
    }
}
