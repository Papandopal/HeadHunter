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
    public class PositionRepository(AppDbContext dbContext) : IPositionRepository
    {
        private DbSet<Position> positions = dbContext.Set<Position>();
        void IRepository<Position>.Add(Position entity)
        {
            positions.Add(entity);
        }

        void IRepository<Position>.Delete(Guid id)
        {
            var entity = positions.First(x => x.Id == id);
            positions.Remove(entity);
        }

        void IPositionRepository.DeleteRange(IEnumerable<Guid> ids)
        {
            var removedPositions = positions.Where(x => ids.Contains(x.Id));
            positions.RemoveRange(removedPositions);    
        }

        IQueryable<Position> IRepository<Position>.GetAll()
        {
            return positions.Include(x=>x.ProjectTags).Include(x=>x.AccessRules).Include(x=>x.PositionSkills).ThenInclude(x=>x.Skill);
        }

        Position IRepository<Position>.GetById(Guid id)
        {
            return positions.Include(x=>x.ProjectTags).Include(x=>x.AccessRules).Include(x=>x.PositionSkills).ThenInclude(y=>y.Skill).First(x=>x.Id == id);
        }

        IEnumerable<Position> IPositionRepository.GetByIds(IEnumerable<Guid> ids)
        {
            return positions.Include(x=>x.ProjectTags).Include(x=>x.AccessRules).Include(x=>x.PositionSkills).ThenInclude(x=>x.Skill).Where(x => ids.Contains(x.Id));
        }

        bool IRepository<Position>.IsExists(Position entity)
        {
            return positions.FirstOrDefault(x => x.Id == entity.Id) is not null;
        }

        void IRepository<Position>.Update(Position entity)
        {
            positions.Update(entity);
        }
    }
}
