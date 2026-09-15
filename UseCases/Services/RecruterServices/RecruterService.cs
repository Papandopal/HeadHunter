using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using UseCases.Database;
using UseCases.Services.RecruterServices.Interfaces;

namespace UseCases.Services.RecruterServices
{
    public class RecruterService(IUnitOfWork unitOfWork) : IRecruterService
    {
        void IRecruterService.Add(Recruter recruter)
        {
            unitOfWork.StartTransaction();
            unitOfWork.RecruterRepostory.Add(recruter);
            unitOfWork.Commit();
        }

        Recruter? IRecruterService.GetItemOrDefaultByOwnerId(Guid ownerId)
        {
            try
            {
                var recruter = unitOfWork.RecruterRepostory.GetByOwnerId(ownerId);
                return recruter;
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }
    }
}
