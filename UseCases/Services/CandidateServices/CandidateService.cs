using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
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
    }
}
