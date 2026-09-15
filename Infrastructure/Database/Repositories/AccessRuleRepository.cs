using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UseCases.Database.Repositories;

namespace Infrastructure.Database.Repositories
{
    public class AccessRuleRepository(AppDbContext dbContext) : IAccessRuleRepository
    {
        private DbSet<AccessRule> accessRules = dbContext.Set<AccessRule>();
        void IRepository<AccessRule>.Add(AccessRule entity)
        {
            accessRules.Add(entity);
        }

        void IRepository<AccessRule>.Delete(Guid id)
        {
            var entity = accessRules.First(x => x.Id == id);
            accessRules.Remove(entity);
        }

        IQueryable<AccessRule> IRepository<AccessRule>.GetAll()
        {
            return accessRules;
        }

        AccessRule IRepository<AccessRule>.GetById(Guid id)
        {
            return accessRules.First(x=>x.Id == id);
        }

        bool IRepository<AccessRule>.IsExists(AccessRule entity)
        {
            return accessRules.FirstOrDefault(x => x.Id == entity.Id) is not null;
        }

        void IRepository<AccessRule>.Update(AccessRule entity)
        {
            accessRules.Update(entity);
        }
    }
}
