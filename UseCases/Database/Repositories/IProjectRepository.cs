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
        public IEnumerable<Project> GetByOwnerId(Guid ownerId);
    }
}
