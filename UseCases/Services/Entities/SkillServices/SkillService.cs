using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;
using UseCases.Database;
using UseCases.Services.SkillServices.DTOs;
using UseCases.Services.SkillServices.Interfaces;

namespace UseCases.Services.SkillServices
{
    public class SkillService(IUnitOfWork unitOfWork) : ISkillService
    {
        async Task ISkillService.AddAsync(Skill skill)
        {
            await unitOfWork.StartTransactionAsync();
            unitOfWork.SkillRepository.Add(skill);
            await unitOfWork.CommitAsync();
        }

        IEnumerable<Skill> ISkillService.GetAll()
        {
            return unitOfWork.SkillRepository.GetAll();
        }

        IEnumerable<string> ISkillService.GetAllSkillsNamesByPrefix(string? prefix)
        {
            var skills = unitOfWork.SkillRepository.GetAll();
            if (prefix is null || string.IsNullOrEmpty(prefix.Trim())) return skills.Select(x => x.Name);
            return skills.Where(x => x.Name.ToLower().StartsWith(prefix.ToLower())).Select(x => x.Name);
        }

        Skill ISkillService.GetById(Guid id)
        {
            return unitOfWork.SkillRepository.GetById(id);
        }

        Skill ISkillService.GetByName(string name)
        {
            return unitOfWork.SkillRepository.GetByName(name);
        }

        Guid ISkillService.GetIdBySkillName(string skillName)
        {
            return unitOfWork.SkillRepository.GetIdBySkillName(skillName);
        }

        IEnumerable<string> ISkillService.GetPopularSkillsNames(int count)
        {
            return unitOfWork.SkillRepository.GetPopularitySkillsNames(count);
        }

        SkillTypes ISkillService.GetSkillTypeBySkillName(string skillName)
        {
            return unitOfWork.SkillRepository.GetSkillTypeBySkillName(skillName);
        }

        void ISkillService.PopularityDown(Guid skillId)
        {
            unitOfWork.SkillRepository.PopularityDown(skillId);
        }

        void ISkillService.PopularityUp(Guid skillId)
        {
            unitOfWork.SkillRepository.PopularityUp(skillId);
        }

        async Task ISkillService.PopulariyDownRangeAsync(IEnumerable<Guid> skillIds)
        {
            unitOfWork.StartTransaction();
            await unitOfWork.SkillRepository.PopulariyDownRangeAsync(skillIds);
            unitOfWork.Commit();
        }

        async Task ISkillService.PopulariyUpRangeAsync(IEnumerable<Guid> skillIds)
        {
            unitOfWork.StartTransaction();
            await unitOfWork.SkillRepository.PopulariyUpRangeAsync(skillIds);
            unitOfWork.Commit();
        }

        void ISkillService.UpdateByDTOs(IEnumerable<UpdateSkillDTO> updateSkillDTOs)
        {
            unitOfWork.StartTransaction();
            foreach (var updateSkillDTO in updateSkillDTOs)
            {
                var skill = unitOfWork.SkillRepository.GetById(updateSkillDTO.SkillId);
                var property = skill.GetType().GetProperty(updateSkillDTO.PropName);
                if (property is null) throw new Exception($"{updateSkillDTO.PropName} not found in destination object");

                var converter = TypeDescriptor.GetConverter(property.PropertyType);

                if (converter is null || !converter.CanConvertFrom(typeof(string)))
                    throw new Exception($"cannot convert string to {property.Name}");

                var convertedValue = converter.ConvertFrom(updateSkillDTO.PropValue);

                property.SetValue(skill, convertedValue);

                unitOfWork.SkillRepository.Update(skill);
            }
            unitOfWork.Commit();
        }
    }
}
