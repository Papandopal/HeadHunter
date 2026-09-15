using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace UseCases.Database.Repositories
{
    public interface ICandidateSkillRepository : IRepository<CandidateSkill>
    {
        public void AddRange(IEnumerable<CandidateSkill> items);
        public void UpdateRange(IEnumerable<CandidateSkill> items);
        public CandidateSkill GetByName(Guid ownerId, string name);
        public IEnumerable<CandidateSkill> GetByOwnerId(Guid ownerId);
    }
}
