using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.Services.Entities.AdministratorServices.Interfaces
{
    public interface IAdministratorService
    {
        public IEnumerable<string> GetAdministratorsEmails();
    }
}
