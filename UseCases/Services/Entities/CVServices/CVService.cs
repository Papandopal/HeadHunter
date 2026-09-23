using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.S3.Model.Internal.MarshallTransformations;
using Domain.Entities;
using UseCases.Database;
using UseCases.Services.CVServices.Interfaces;

namespace UseCases.Services.CVServices
{
    public class CVService(IUnitOfWork unitOfWork) : ICVService
    {
        void ICVService.AddCV(Guid ownerId, Guid positionId)
        {
            var newCV = new CV
            {
                PositionId = positionId,
                CandidateId = ownerId,
                LastUpdateTime = DateTime.Now,
            };

            unitOfWork.StartTransaction();
            unitOfWork.CVRepository.Add(newCV);
            unitOfWork.Commit();
        }

        IEnumerable<CV> ICVService.GetAll()
        {
            return unitOfWork.CVRepository.GetAll();
        }

        CV ICVService.GetById(Guid id)
        {
            return unitOfWork.CVRepository.GetById(id);
        }

        IEnumerable<CV> ICVService.GetByOwnerId(Guid ownerId)
        {
            return unitOfWork.CVRepository.GetByOwnerId(ownerId);
        }

        IEnumerable<CV> ICVService.GetByPositionId(Guid positionId)
        {
            return unitOfWork.CVRepository.GetByPositionId(positionId);
        }

        IEnumerable<CV> ICVService.GetByPositionIds(IEnumerable<Guid> positionIds)
        {
            return unitOfWork.CVRepository.GetByPositionIds(positionIds);
        }

        bool ICVService.IsCVLikedBy(Guid cvId, Recruter user)
        {
            return unitOfWork.CVRepository.IsCVLikedBy(cvId, user);
        }

        void ICVService.Like(Guid cvId, Recruter user)
        {
            unitOfWork.StartTransaction();
            unitOfWork.CVRepository.Like(cvId, user);
            unitOfWork.Commit();
        }

        void ICVService.Unlike(Guid cvId, Recruter user)
        {
            unitOfWork.StartTransaction();
            unitOfWork.CVRepository.Unlike(cvId, user);
            unitOfWork.Commit();
        }
    }
}
