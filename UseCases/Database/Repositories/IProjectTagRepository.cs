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
        public IEnumerable<string> GetNamesByPrefix(string? prefix);
    }
}
