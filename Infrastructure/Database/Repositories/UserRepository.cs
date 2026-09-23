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

        void IRepository<User>.Delete(Guid id)
        {
            users.Remove(users.First(x => x.Id == id));
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

        bool IRepository<User>.IsExists(User entity)
        {
            return users.FirstOrDefault(x=>x.Id == entity.Id) is not null; 
        }

        void IRepository<User>.Update(User entity)
        {
            users.Update(entity);
        }
    }
}
