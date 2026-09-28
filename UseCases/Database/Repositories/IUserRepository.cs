using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace UseCases.Database.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        public void Block(Guid id);
        public void BlockRange(IEnumerable<Guid> ids);
        public void Unblock(Guid id);
        public void UnblockRange(IEnumerable<Guid> ids);
        public void DeleteRange(IEnumerable<Guid> ids);
        public User? FirstOrDefaultByEmail(string email);
        public IEnumerable<User> GetByIds(IEnumerable<Guid> ids);
    }
}
