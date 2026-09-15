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
        public IEnumerable<Skill> GetAll();
        public IEnumerable<string> GetAllSkillsNamesByPrefix(string? prefix);
        public Skill GetByName(string name);
        public Skill GetById(Guid id);
        public Guid GetIdBySkillName(string name);
        public SkillTypes GetSkillTypeBySkillName(string name);
    }
}
