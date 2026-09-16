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
    public class CVRepository(AppDbContext dbContext) : ICVRepository
    {
        private DbSet<CV> cvs = dbContext.Set<CV>();
        void IRepository<CV>.Add(CV entity)
        {
            cvs.Add(entity);
        }

        void IRepository<CV>.Delete(Guid id)
        {
            var cv = cvs.First(c => c.Id == id);
            cvs.Remove(cv);
        }

        IQueryable<CV> IRepository<CV>.GetAll()
        {
            return cvs;
        }

        CV IRepository<CV>.GetById(Guid id)
        {
            return cvs.First(x=>x.Id == id);
        }

        IEnumerable<CV> ICVRepository.GetByOwnerId(Guid ownerId)
        {
            return cvs.Include(x=>x.ValuedSkills).Where(x=>x.CandidateId == ownerId);
        }

        bool IRepository<CV>.IsExists(CV entity)
        {
            return cvs.FirstOrDefault(x=>x.Id == entity.Id) is not null;
        }

        void IRepository<CV>.Update(CV entity)
        {
            cvs.Update(entity);
        }
    }
}
