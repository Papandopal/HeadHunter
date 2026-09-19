using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;
using UseCases.Services.SkillServices.DTOs;

namespace UseCases.Services.SkillServices.Interfaces
{
    public interface ISkillService
    {
        public Task AddAsync(Skill skill);
        public void UpdateByDTOs(IEnumerable<UpdateSkillDTO> updateSkillDTOs);
        public void PopularityUp(Guid skillId);
        public Task PopulariyUpRangeAsync(IEnumerable<Guid> skillIds);
        public void PopularityDown(Guid skillId);
        public Task PopulariyDownRangeAsync(IEnumerable<Guid> skillIds);
        public IEnumerable<Skill> GetAll();
        public IEnumerable<string> GetAllSkillsNamesByPrefix(string? prefix);
        public IEnumerable<string> GetPopularSkillsNames(int count);
        public Skill GetByName(string name);
        public Skill GetById(Guid id);
        public Guid GetIdBySkillName(string name);
        public SkillTypes GetSkillTypeBySkillName(string name);
    }
}
