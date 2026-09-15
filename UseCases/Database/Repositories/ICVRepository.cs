using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace UseCases.Database.Repositories
{
    public interface ICVRepository : IRepository<CV>
    {
        public IEnumerable<CV> GetByOwnerId(Guid ownerId);
    }
}
