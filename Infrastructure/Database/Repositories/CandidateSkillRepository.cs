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
    public class CandidateSkillRepository(AppDbContext dbContext) : ICandidateSkillRepository
    {
        private DbSet<CandidateSkill> skills = dbContext.Set<CandidateSkill>();
        void IRepository<CandidateSkill>.Add(CandidateSkill entity)
        {
            skills.Add(entity);
        }

        void IRepository<CandidateSkill>.Delete(Guid id)
        {
            skills.Remove(skills.First(x => x.Id == id));
        }

        IQueryable<CandidateSkill> IRepository<CandidateSkill>.GetAll()
        {
            return skills.Include(x=>x.Skill);
        }

        CandidateSkill IRepository<CandidateSkill>.GetById(Guid id)
        {
            return skills.Include(x => x.Skill).First(x => x.Id == id);
        }

        CandidateSkill ICandidateSkillRepository.GetByName(Guid ownerId, string name)
        {
            return skills.Include(x => x.Skill).Where(x=> x.CandidateId == ownerId && x.Skill.Name == name).First();
        }

        IEnumerable<CandidateSkill> ICandidateSkillRepository.GetByOwnerId(Guid ownerId)
        {
            return skills.Include(x => x.Skill).Where(x=>x.CandidateId == ownerId);
        }

        bool IRepository<CandidateSkill>.IsExists(CandidateSkill entity)
        {
            return skills.Include(x => x.Skill).FirstOrDefault(x => x.Id == entity.Id) is not null;
        }

        void IRepository<CandidateSkill>.Update(CandidateSkill entity)
        {
            skills.Update(entity);
        }

        void ICandidateSkillRepository.UpdateRange(IEnumerable<CandidateSkill> items)
        {
            skills.UpdateRange(items);
        }
    }
}
