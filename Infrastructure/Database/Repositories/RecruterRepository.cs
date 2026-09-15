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
    internal class RecruterRepository(AppDbContext dbContext) : IRecruterRepostory
    {
        private DbSet<Recruter> recrutes = dbContext.Set<Recruter>();
        void IRepository<Recruter>.Add(Recruter entity)
        {
            recrutes.Add(entity);
        }

        void IRepository<Recruter>.Delete(Guid id)
        {
            recrutes.Remove(recrutes.First(x => x.Id == id));
        }

        IQueryable<Recruter> IRepository<Recruter>.GetAll()
        {
            return recrutes;
        }

        Recruter IRepository<Recruter>.GetById(Guid id)
        {
            return recrutes.First(x => x.Id == id);
        }

        Recruter IRecruterRepostory.GetByOwnerId(Guid ownerId)
        {
            return recrutes.First(x=>x.OwnerId == ownerId);
        }

        bool IRepository<Recruter>.IsExists(Recruter entity)
        {
            return recrutes.FirstOrDefault(x=>x.Id == entity.Id) is not null;
        }

        void IRepository<Recruter>.Update(Recruter entity)
        {
            recrutes.Update(entity);
        }
    }
}
