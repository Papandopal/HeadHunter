using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;

namespace UseCases.Database.Repositories
{
    public interface ISkillRepository : IRepository<Skill>
    {
        public void PopularityUp(Guid skillId);
        public Task PopulariyUpRangeAsync(IEnumerable<Guid> skillIds);
        public void PopularityDown(Guid skillId);
        public Task PopulariyDownRangeAsync(IEnumerable<Guid> skillIds);
        public Skill GetByName(string name);
        public IEnumerable<Skill> GetByIdRange(IEnumerable<Guid> ids);
        public IEnumerable<string> GetPopularitySkillsNames(int count);
        public SkillTypes GetSkillTypeBySkillName(string skillName);
        public Guid GetIdBySkillName(string skillName);
    }
}
