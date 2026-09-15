using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using UseCases.Database;
using UseCases.Services.CVServices.Interfaces;

namespace UseCases.Services.CVServices
{
    public class CVService(IUnitOfWork unitOfWork) : ICVService
    {
        void ICVService.AddCV(Guid ownerId, Guid positionId)
        {
            var candidateSkills = unitOfWork.CandidateSkillRepository.GetByOwnerId(ownerId);
            var positionSkillsSkillsIds = unitOfWork.PositionSkillRepositiry.GetByPositionId(positionId).Select(x => x.SkillId).ToHashSet();
            var cvSkills = candidateSkills.Where(x => positionSkillsSkillsIds.Contains(x.SkillId)).ToList();

            var newCV = new CV
            {
                PositionId = positionId,
                CandidateId = ownerId,
                ValuedSkills = cvSkills
            };

            unitOfWork.StartTransaction();
            unitOfWork.CVRepository.Add(newCV);
            unitOfWork.Commit();
        }

        IEnumerable<CV> ICVService.GetAll()
        {
            return unitOfWork.CVRepository.GetAll();
        }

        IEnumerable<CV> ICVService.GetByOwnerId(Guid ownerId)
        {
            return unitOfWork.CVRepository.GetByOwnerId(ownerId);
        }
    }
}
