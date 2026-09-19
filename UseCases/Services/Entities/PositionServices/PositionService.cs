using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Entities;
using UseCases.Database;
using UseCases.Services.AccessRuleServices.DTOs;
using UseCases.Services.AccessRuleServices.Interfaces;
using UseCases.Services.PositionServices.DTOs;
using UseCases.Services.PositionServices.Interfaces;
using UseCases.Services.ProjectTagServices.Interfaces;
using UseCases.Services.ValuedSkillServices.PositionSkillsServices.DTOs;

namespace UseCases.Services.PositionServices
{
    public class PositionService(IUnitOfWork unitOfWork, IAccessRuleService accessRuleService) : IPositionService
    {
        void IPositionService.AddPosition(AddPositionDTO positionDTO, Guid ownerId)
        {
            var accessRuleRecords = JsonSerializer.Deserialize<IEnumerable<AccessRuleRecord>>(positionDTO.BufferForAccessRules);

            var accessRuleRecordsPairs = accessRuleRecords.GroupBy(x => x.SkillId);

            IEnumerable<AccessRule> accessRules = accessRuleRecordsPairs.Select(x => accessRuleService.GetFromRecords(x)).ToList();

            IEnumerable<ProjectTag> projectTags = JsonSerializer.Deserialize<IEnumerable<string>>(positionDTO.BufferForProjectTags).Select(x=>new ProjectTag { Name = x}).ToList();

            var newPosition = new Position
            {
                OwnerId = ownerId,
                Title = positionDTO.Title,
                Description = positionDTO.Description,
                AccessRules = accessRules,
                ProjectTags = projectTags,
                MaxCountOfProject = positionDTO.MaxCountOfProject,
            };

            IEnumerable<AddPositionSkillDTO> newValuedSkills = JsonSerializer.Deserialize<IEnumerable<AddPositionSkillDTO>>(positionDTO.BufferForSkills);
            IEnumerable<Skill> skills = unitOfWork.SkillRepository.GetAll().Where(x => newValuedSkills.Select(y => y.SkillName).Contains(x.Name)).ToList();
            IEnumerable<PositionSkill> newPositionSkills = newValuedSkills.Select(x => new PositionSkill { Skill = skills.First(y => y.Name == x.SkillName), Position = newPosition }).ToList();
            newPosition.PositionSkills = newPositionSkills;
            unitOfWork.StartTransaction();
            unitOfWork.PositionRepository.Add(newPosition);
            unitOfWork.Commit();
        }

        void IPositionService.DeleteRange(IEnumerable<Guid> ids)
        {
            unitOfWork.StartTransaction();
            unitOfWork.PositionRepository.DeleteRange(ids);
            unitOfWork.Commit();
        }

        IEnumerable<Position> IPositionService.GetAll()
        {
            return unitOfWork.PositionRepository.GetAll();
        }

        Position IPositionService.GetById(Guid id)
        {
            return unitOfWork.PositionRepository.GetById(id);
        }

        IEnumerable<Position> IPositionService.GetByIds(IEnumerable<Guid> ids)
        {
            return unitOfWork.PositionRepository.GetByIds(ids);
        }
    }
}
