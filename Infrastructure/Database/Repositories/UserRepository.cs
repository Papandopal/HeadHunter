using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using UseCases.Database.Repositories;

namespace Infrastructure.Database.Repositories
{
    public class UserRepository(AppDbContext dbContext) : IUserRepository
    {
        private DbSet<User> users = dbContext.Set<User>();

        void IRepository<User>.Add(User entity)
        {
            users.Add(entity);
        }

        void IUserRepository.Block(Guid id)
        {
            users.Where(x=>x.Id == id).First().Block();
        }

        void IUserRepository.BlockRange(IEnumerable<Guid> ids)
        {
            foreach(var user in users.Where(x => ids.Contains(x.Id)))
            {
                user.Block();
            }
        }

        void IRepository<User>.Delete(Guid id)
        {
            users.Remove(users.First(x => x.Id == id));
        }

        void IUserRepository.DeleteRange(IEnumerable<Guid> ids)
        {
            users.RemoveRange(users.Where(x => ids.Contains(x.Id)));
        }

        User? IUserRepository.FirstOrDefaultByEmail(string email)
        {
            var user = users.FirstOrDefault(x => x.Email == email);
            return user;
        }

        IQueryable<User> IRepository<User>.GetAll()
        {
            return users;
        }

        User IRepository<User>.GetById(Guid id)
        {
            return users.First(x => x.Id == id);
        }

        IEnumerable<User> IUserRepository.GetByIds(IEnumerable<Guid> ids)
        {
            return users.Where(x=>ids.Contains(x.Id));
        }

        bool IRepository<User>.IsExists(User entity)
        {
            return users.FirstOrDefault(x=>x.Id == entity.Id) is not null; 
        }

        void IUserRepository.Unblock(Guid id)
        {
            users.Where(x=>x.Id == id).First().Unblock();
        }

        void IUserRepository.UnblockRange(IEnumerable<Guid> ids)
        {
            foreach (var user in users.Where(x => ids.Contains(x.Id)))
            {
                user.Unblock();
            }
        }

        void IRepository<User>.Update(User entity)
        {
            users.Update(entity);
        }
    }
}
