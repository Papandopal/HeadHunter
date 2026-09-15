using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.Entities;
using UseCases.Database;
using UseCases.Services.ValuedSkillServices.CandidateSkillServices.Interfaces;
using UseCases.Services.ValuedSkillServices.GeneralDTOs;

namespace UseCases.Services.ValuedSkillServices.CandidateSkillServices
{
    public class CandidateSkillService(IUnitOfWork unitOfWork) : ICandidateSkillService
    {
        void ICandidateSkillService.Add(CandidateSkill skill)
        {
            unitOfWork.StartTransaction();
            unitOfWork.CandidateSkillRepository.Add(skill);
            unitOfWork.Commit();
        }

        CandidateSkill ICandidateSkillService.GetByName(Guid ownerId, string name)
        {
            return unitOfWork.CandidateSkillRepository.GetByName(ownerId, name);
        }

        IEnumerable<CandidateSkill> ICandidateSkillService.GetCandidateSkillsByOwnerId(Guid ownerId)
        {
            return unitOfWork.CandidateSkillRepository.GetByOwnerId(ownerId);
        }

        IEnumerable<CandidateSkill> ICandidateSkillService.GetCandidateSkillsByOwnerIdWithPrefix(Guid ownerId, string prefix)
        {
            return unitOfWork.CandidateSkillRepository.GetByOwnerId(ownerId).Where(x=>x.Skill.Name.StartsWith(prefix));
        }

        void ICandidateSkillService.UpdateCandidateSkills(IEnumerable<UpdateValuedSkillDTO> skillDTOs)
        {
            unitOfWork.StartTransaction();
            var skills = skillDTOs.Select(x => unitOfWork.CandidateSkillRepository.GetById(x.ValuedSkillId));
            var iterator = skillDTOs.GetEnumerator();
            foreach (var skill in skills)
            {
                iterator.MoveNext();
                skill.ChangeValue(iterator.Current.Value);
            }
            unitOfWork.CandidateSkillRepository.UpdateRange(skills);
            unitOfWork.Commit();
        }
    }
}
