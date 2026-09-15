using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;

namespace UseCases.Database.Repositories
{
    public interface ISkillRepository : IRepository<Skill>
    {
        public Skill GetByName(string name);
        public IEnumerable<Skill> GetByIdRange(IEnumerable<Guid> ids);
        public SkillTypes GetSkillTypeBySkillName(string skillName);
        public Guid GetIdBySkillName(string skillName);
    }
}
