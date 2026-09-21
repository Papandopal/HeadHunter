using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace UseCases.Database.Repositories
{
    public interface IPositionSkillRepositiry  : IRepository<PositionSkill>
    {
        public void AddRange(IEnumerable<PositionSkill> skills);
        public void RemoveRange(IEnumerable<Guid> ids);
        public IEnumerable<PositionSkill> GetByPositionId(Guid positionId);
    }
}
