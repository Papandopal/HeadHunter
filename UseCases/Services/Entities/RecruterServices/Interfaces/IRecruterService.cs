using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace UseCases.Services.RecruterServices.Interfaces
{
    public interface IRecruterService
    {
        public void Add(Recruter recruter);
        public Recruter? GetItemOrDefaultByOwnerId(Guid ownerId);
    }
}
