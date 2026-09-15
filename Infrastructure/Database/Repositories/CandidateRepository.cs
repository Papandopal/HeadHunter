using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using UseCases.Database.Repositories;

namespace Infrastructure.Database.Repositories
{
    public class CandidateRepository(AppDbContext dbContext) : ICandidateRepository
    {
        private DbSet<Candidate> candidates = dbContext.Set<Candidate>();
        void IRepository<Candidate>.Add(Candidate entity)
        {
            candidates.Add(entity);
        }

        void ICandidateRepository.AddByUser(User user)
        {
            candidates.Add(new Candidate { UserId = user.Id });
        }

        void IRepository<Candidate>.Delete(Guid id)
        {
            candidates.Remove(candidates.First(x => x.Id == id));
        }

        IQueryable<Candidate> IRepository<Candidate>.GetAll()
        {
            return candidates.Include(x=>x.Skills);
        }

        Candidate IRepository<Candidate>.GetById(Guid id)
        {
            return candidates.Include(x => x.Skills).First(x => x.Id == id);
        }

        Candidate ICandidateRepository.GetByOwnerId(Guid id)
        {
            return candidates.Include(x=>x.Skills).First(x=>x.UserId == id);
        }

        bool IRepository<Candidate>.IsExists(Candidate entity)
        {
            return candidates.FirstOrDefault(x=>x.Id == entity.Id) is not null;
        }

        void IRepository<Candidate>.Update(Candidate entity)
        {
            candidates.Update(entity);
        }
    }
}
