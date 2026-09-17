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
        public void DeleteRange(IEnumerable<CandidateSkill> items);
        public CandidateSkill GetByName(string name, Guid ownerId);
        public IEnumerable<CandidateSkill> GetByOwnerId(Guid ownerId);
        public IEnumerable<CandidateSkill> GetAllExceptOf(IEnumerable<Guid> ids);
    }
}
