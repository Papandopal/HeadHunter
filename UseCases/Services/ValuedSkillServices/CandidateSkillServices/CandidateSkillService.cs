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
using UseCases.Services.ValuedSkillServices.CandidateSkillServices.Interfaces;
using UseCases.Services.ValuedSkillServices.GeneralDTOs;

namespace UseCases.Services.ValuedSkillServices.CandidateSkillServices
{
    public class CandidateSkillService(IUnitOfWork unitOfWork) : ICandidateSkillService
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
            unitOfWork.StartTransaction();
            unitOfWork.CandidateSkillRepository.Add(newCandidateSkill);
            unitOfWork.Commit();
        }

        void ICandidateSkillService.Update(EditValuedSkillDTO skillDTO)
        {
            var skill = unitOfWork.CandidateSkillRepository.GetById(skillDTO.ValuedSkillId);
            skill.ChangeValue(skillDTO.Value);
            unitOfWork.StartTransaction();
            unitOfWork.CandidateSkillRepository.Update(skill);
            unitOfWork.Commit();

        }

        void ICandidateSkillService.AddRange(IEnumerable<AddValuedSkillDTO> skillDTOs, Guid candidateId)
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
            }

            var deletedSkills = unitOfWork.CandidateSkillRepository.GetAllExceptOf(skillDTOs.Select(x => x.ValuedSkillId));

            unitOfWork.StartTransaction();
            unitOfWork.CandidateSkillRepository.DeleteRange(deletedSkills);
            unitOfWork.CandidateSkillRepository.UpdateRange(skills);
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
