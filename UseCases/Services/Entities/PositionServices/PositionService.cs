using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Entities;
using UseCases.Database;
using UseCases.Services.Entities.PositionServices.DTOs;
using UseCases.Services.Entities.PositionSkillsServices.DTOs;
using UseCases.Services.Entities.ValuedSkillServices.AccessRuleServices.DTOs;
using UseCases.Services.Entities.ValuedSkillServices.AccessRuleServices.Interfaces;
using UseCases.Services.PositionServices.DTOs;
using UseCases.Services.PositionServices.Interfaces;
using UseCases.Services.ValuedSkillServices.General.DTOs;

namespace UseCases.Services.PositionServices
{
    public class PositionService(IUnitOfWork unitOfWork, IAccessRuleService accessRuleService) : IPositionService
    {
        private IEnumerable<AccessRule> GetRulesFromRecordsJSON(string json)
        {
            var accessRuleRecords = JsonSerializer.Deserialize<IEnumerable<AccessRuleRecord>>(json);
            var accessRuleRecordsPairs = accessRuleRecords.GroupBy(x => x.SkillId);
            return accessRuleRecordsPairs.Select(x => accessRuleService.GetFromRecords(x)).ToList();
        }

        private IEnumerable<PositionSkill> GetSkillsFromRecordsJSON(string json, Position position)
        {
            IEnumerable<AddPositionSkillDTO> newValuedSkills = JsonSerializer.Deserialize<IEnumerable<AddPositionSkillDTO>>(json);
            IEnumerable<Skill> skills = unitOfWork.SkillRepository.GetAll().Where(x => newValuedSkills.Select(y => y.SkillName).Contains(x.Name)).ToList();
            return newValuedSkills.Select(x => new PositionSkill { Skill = skills.First(y => y.Name == x.SkillName), Position = position }).ToList();
        }

        void IPositionService.AddPosition(AddPositionDTO positionDTO, Guid ownerId)
        {
            IEnumerable<AccessRule> accessRules = GetRulesFromRecordsJSON(positionDTO.BufferForAccessRules);
            IEnumerable<ProjectTag> projectTags = JsonSerializer.Deserialize<IEnumerable<string>>(positionDTO.BufferForProjectTags).Select(x => new ProjectTag { Name = x }).ToList();

            var newPosition = new Position
            {
                OwnerId = ownerId,
                Title = positionDTO.Title,
                Description = positionDTO.Description,
                AccessRules = accessRules,
                ProjectTags = projectTags,
                MaxCountOfProject = positionDTO.MaxCountOfProject,
            };
            newPosition.PositionSkills = GetSkillsFromRecordsJSON(positionDTO.BufferForSkills, newPosition);

            unitOfWork.StartTransaction();
            unitOfWork.PositionRepository.Add(newPosition);
            unitOfWork.Commit();
        }

        void IPositionService.UpdatePosition(EditPositionDTO positionDTO)
        {
            Position position = unitOfWork.PositionRepository.GetById(positionDTO.Id);
            IEnumerable<Guid> oldPositionSkillsIds = position.PositionSkills.Select(x => x.Id);
            IEnumerable<AccessRule> accessRules = GetRulesFromRecordsJSON(positionDTO.BufferForAccessRules);
            IEnumerable<ProjectTag> projectTags = JsonSerializer.Deserialize<IEnumerable<string>>(positionDTO.BufferForProjectTags).Select(x => new ProjectTag { Name = x }).ToList();
            IEnumerable<PositionSkill> positionSkills = GetSkillsFromRecordsJSON(positionDTO.BufferForSkills, position);

            position.Title = positionDTO.Title;
            position.Description = positionDTO.Description;
            position.MaxCountOfProject = positionDTO.MaxCountOfProject;
            position.AccessRules = accessRules;
            position.ProjectTags = projectTags;
            position.PositionSkills = positionSkills;

            unitOfWork.StartTransaction();
            unitOfWork.PositionRepository.Update(position);
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

        IEnumerable<Position> IPositionService.GetPersonaledPositions(Candidate candidate)
        {
            IEnumerable<ValuedSkillDTO> valuedSkills = candidate.Skills.Select(x =>
                new ValuedSkillDTO
                {
                    Skill = x.Skill,
                    Value = x.Value
                });
            return unitOfWork.PositionRepository.GetPersonaledPositionsBySkills(valuedSkills);
        }

        Position IPositionService.GetByOwnerId(Guid ownerId)
        {
           return  unitOfWork.PositionRepository.GetByOwnerId(ownerId);
        }
    }
}
