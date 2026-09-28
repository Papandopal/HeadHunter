using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;
using UseCases.Database;
using UseCases.Services.CandidateServices.Interfaces;

namespace UseCases.Services.CandidateServices
{
    public class CandidateService(IUnitOfWork unitOfWork) : ICandidateService
    {
        void ICandidateService.Add(Candidate candidate)
        {
            unitOfWork.StartTransaction();
            unitOfWork.CandidateRepository.Add(candidate);
            unitOfWork.Commit();
        }

        void ICandidateService.Update(Candidate candidate)
        {
            unitOfWork.StartTransaction();
            unitOfWork.CandidateRepository.Update(candidate);
            unitOfWork.Commit();
        }

        Candidate? ICandidateService.GetItemOrDefaultByOwnerId(Guid ownerId)
        {
            try
            {
                var candidate = unitOfWork.CandidateRepository.GetByOwnerId(ownerId);
                return candidate;
            }
            catch (InvalidOperationException)
            {
                return null;    
            }
        }

        void ICandidateService.UpdateRange(IEnumerable<Candidate> candidates)
        {
            unitOfWork.StartTransaction();
            unitOfWork.CandidateRepository.UpdateRange(candidates);
            unitOfWork.Commit();
        }

        void ICandidateService.Block(Guid id)
        {
            unitOfWork.StartTransaction();
            var candidate = unitOfWork.CandidateRepository.GetById(id);
            unitOfWork.UserRepository.Block(candidate.UserId);
            unitOfWork.Commit();
        }

        void ICandidateService.BlockRange(IEnumerable<Guid> ids)
        {
            unitOfWork.StartTransaction();
            var candidates = unitOfWork.CandidateRepository.GetByIds(ids);
            unitOfWork.UserRepository.BlockRange(candidates.Select(x => x.UserId));
            unitOfWork.Commit();
        }

        void ICandidateService.Unblock(Guid id)
        {
            unitOfWork.StartTransaction();
            var canidate = unitOfWork.CandidateRepository.GetById(id);
            unitOfWork.UserRepository.Unblock(canidate.UserId);
            unitOfWork.Commit();
        }

        void ICandidateService.UnblockRange(IEnumerable<Guid> ids)
        {
            unitOfWork.StartTransaction();
            var candidates = unitOfWork.CandidateRepository.GetByIds(ids);
            unitOfWork.UserRepository.UnblockRange(candidates.Select(x => x.UserId));
            unitOfWork.Commit();
        }

        void ICandidateService.Delete(Guid id)
        {
            unitOfWork.StartTransaction();
            var candidate = unitOfWork.CandidateRepository.GetById(id);
            unitOfWork.UserRepository.Delete(candidate.UserId);
            unitOfWork.Commit();
        }

        void ICandidateService.DeleteRange(IEnumerable<Guid> ids)
        {
            unitOfWork.StartTransaction();
            var candidates = unitOfWork.CandidateRepository.GetByIds(ids);
            unitOfWork.UserRepository.DeleteRange(candidates.Select(x => x.UserId));
            unitOfWork.Commit();
        }

        bool ICandidateService.IsBlocked(Candidate candidate)
        {
            return unitOfWork.UserRepository.GetById(candidate.UserId).Status == UserStatus.Blocked;
        }

        IDictionary<Guid, bool> ICandidateService.IsBlockedRange(IEnumerable<Candidate> candidates)
        {
            Dictionary<Guid, Candidate> candidateByUserIdDictionary = candidates.ToDictionary(x=>x.UserId);
            return unitOfWork.UserRepository.GetByIds(candidates.Select(x => x.UserId))
                .ToDictionary(x => candidateByUserIdDictionary[x.Id].Id, x => x.Status == UserStatus.Blocked);
        }

        Candidate ICandidateService.GetById(Guid id)
        {
            return unitOfWork.CandidateRepository.GetById(id);
        }

        IEnumerable<Candidate> ICandidateService.GetAll()
        {
            return unitOfWork.CandidateRepository.GetAll();
        }

        IEnumerable<Candidate> ICandidateService.GetByIds(IEnumerable<Guid> ids)
        {
            return unitOfWork.CandidateRepository.GetByIds(ids);
        }
    }
}
