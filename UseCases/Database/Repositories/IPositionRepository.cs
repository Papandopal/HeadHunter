using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace UseCases.Database.Repositories
{
    public interface IPositionRepository : IRepository<Position>
    {
        public void DeleteRange(IEnumerable<Guid> ids);
        public IEnumerable<Position> GetByIds(IEnumerable<Guid> ids);
    }
}
