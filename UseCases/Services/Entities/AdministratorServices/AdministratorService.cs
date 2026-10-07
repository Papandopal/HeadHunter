using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;
using UseCases.Database;
using UseCases.Services.Entities.AdministratorServices.Interfaces;

namespace UseCases.Services.Entities.AdministratorServices
{
    public class AdministratorService(IUnitOfWork unitOfWork) : IAdministratorService
    {
        IEnumerable<string> IAdministratorService.GetAdministratorsEmails()
        {
            return unitOfWork.UserRepository.GetAll().Where(x => x.Role == UserRoles.Administrator.ToString()).Select(x=>x.Email);
        }
    }
}
