using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.Entities;
using UseCases.Database;
using UseCases.Services.SkillServices;
using UseCases.Services.SkillServices.Interfaces;
using UseCases.Services.ValuedSkillServices.CandidateSkillServices.Interfaces;
using UseCases.Services.ValuedSkillServices.General.DTOs;

namespace UseCases.Services.ValuedSkillServices.CandidateSkillServices
{
    public class CandidateSkillService(IUnitOfWork unitOfWork, ISkillService skillService) : ICandidateSkillService
    {
        void ICandidateSkillService.Add(AddValuedSkillDTO skillDTO, Guid candidateId)
        {
            var skill = unitOfWork.SkillRepository.GetById(skillDTO.SkillId);
            CandidateSkill newCandidateSkill = new CandidateSkill
            {
                CandidateId = candidateId,
                Skill = skill,
                SkillId = skill.Id,
                Value = skillDTO.Value
            };
            skillService.PopularityUp(skill.Id);
            unitOfWork.StartTransaction();
            unitOfWork.CandidateSkillRepository.Add(newCandidateSkill);
            unitOfWork.Commit();
        }

        void ICandidateSkillService.Update(EditValuedSkillDTO skillDTO)
        {
            var candidateSkill = unitOfWork.CandidateSkillRepository.GetById(skillDTO.ValuedSkillId);
            candidateSkill.ChangeValue(skillDTO.Value);
            candidateSkill.UpdateVersion(skillDTO.Version);
            unitOfWork.StartTransaction();
            unitOfWork.CandidateSkillRepository.Update(candidateSkill);
            unitOfWork.Commit();

        }

        async Task ICandidateSkillService.AddRangeAsync(IEnumerable<AddValuedSkillDTO> skillDTOs, Guid candidateId)
        {
            List<CandidateSkill> newCandidateSkills = new();
            var skills = unitOfWork.SkillRepository.GetByIdRange(skillDTOs.Select(x => x.SkillId)).ToDictionary(x => x.Id);
            foreach (var dto in skillDTOs)
            {
                var skill = skills[dto.SkillId];
                CandidateSkill newCandidateSkill = new CandidateSkill
                {
                    CandidateId = candidateId,
                    Skill = skill,
                    SkillId = skill.Id,
                    Value = dto.Value
                };
                newCandidateSkills.Add(newCandidateSkill);
            }
            await skillService.PopulariyUpRangeAsync(newCandidateSkills.Select(x => x.SkillId));

            unitOfWork.StartTransaction();
            unitOfWork.CandidateSkillRepository.AddRange(newCandidateSkills);
            unitOfWork.Commit();
        }

        void ICandidateSkillService.UpdateRange(IEnumerable<EditValuedSkillDTO> skillDTOs)
        {
            var skills = skillDTOs.Select(x => unitOfWork.CandidateSkillRepository.GetById(x.ValuedSkillId));
            var iterator = skillDTOs.GetEnumerator();
            foreach (var skill in skills)
            {
                iterator.MoveNext();
                skill.ChangeValue(iterator.Current.Value);
                skill.UpdateVersion(iterator.Current.Version);
            }

            unitOfWork.StartTransaction();
            unitOfWork.CandidateSkillRepository.UpdateRange(skills);
            unitOfWork.Commit();
        }

        async Task ICandidateSkillService.ChangeCurrentSkillsAsync(IEnumerable<EditValuedSkillDTO> skillDTOs, Guid ownerId)
        {
            var candidateSkills = skillDTOs.Select(x => unitOfWork.CandidateSkillRepository.GetById(x.ValuedSkillId));
            var iterator = skillDTOs.GetEnumerator();
            foreach (var skill in candidateSkills)
            {
                iterator.MoveNext();
                skill.ChangeValue(iterator.Current.Value);
                skill.UpdateVersion(iterator.Current.Version);
            }

            var deletedCandidateSkills = unitOfWork.CandidateSkillRepository.GetAllExceptOf(skillDTOs.Select(x => x.ValuedSkillId), ownerId);

            var updatedSkillsIds = deletedCandidateSkills.Select(x => x.SkillId);

            await skillService.PopulariyDownRangeAsync(updatedSkillsIds);

            unitOfWork.StartTransaction();
            unitOfWork.CandidateSkillRepository.DeleteRange(deletedCandidateSkills);
            unitOfWork.CandidateSkillRepository.UpdateRange(candidateSkills);
            unitOfWork.Commit();
        }

        CandidateSkill ICandidateSkillService.GetByName(string name, Guid ownerId)
        {
            return unitOfWork.CandidateSkillRepository.GetByName(name, ownerId);
        }

        IEnumerable<CandidateSkill> ICandidateSkillService.GetCandidateSkillsByOwnerId(Guid ownerId)
        {
            return unitOfWork.CandidateSkillRepository.GetByOwnerId(ownerId);
        }

        IEnumerable<CandidateSkill> ICandidateSkillService.GetCandidateSkillsByOwnerIdWithPrefix(Guid ownerId, string prefix)
        {
            return unitOfWork.CandidateSkillRepository.GetByOwnerId(ownerId).Where(x => x.Skill.Name.StartsWith(prefix));
        }
    }
}
