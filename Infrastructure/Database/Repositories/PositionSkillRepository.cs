using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UseCases.Database.Repositories;

namespace Infrastructure.Database.Repositories
{
    public class PositionSkillRepository(AppDbContext dbContext) : IPositionSkillRepositiry
    {
        private DbSet<PositionSkill> positionSkills = dbContext.Set<PositionSkill>();
        void IRepository<PositionSkill>.Add(PositionSkill entity)
        {
            positionSkills.Add(entity);
        }

        void IPositionSkillRepositiry.AddRange(IEnumerable<PositionSkill> skills)
        {
            positionSkills.AddRange(skills);
        }

        void IRepository<PositionSkill>.Delete(Guid id)
        {
            var entity = positionSkills.First(x=>x.Id == id);
            positionSkills.Remove(entity);
        }

        IQueryable<PositionSkill> IRepository<PositionSkill>.GetAll()
        {
            return positionSkills;
        }

        PositionSkill IRepository<PositionSkill>.GetById(Guid id)
        {
            return positionSkills.First(x=>x.Id==id);
        }

        IEnumerable<PositionSkill> IPositionSkillRepositiry.GetByPositionId(Guid positionId)
        {
            return positionSkills.Where(x=>x.PositionId==positionId);
        }

        bool IRepository<PositionSkill>.IsExists(PositionSkill entity)
        {
            return positionSkills.FirstOrDefault(x=>x.Id ==  entity.Id) is not null;
        }

        void IRepository<PositionSkill>.Update(PositionSkill entity)
        {
            positionSkills.Update(entity);
        }
    }
}
