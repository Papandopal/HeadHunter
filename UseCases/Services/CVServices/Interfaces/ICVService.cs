using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using UseCases.Services.CVServices.DTOs;

namespace UseCases.Services.CVServices.Interfaces
{
    public interface ICVService
    {
        public void AddCV(Guid ownerId, Guid positionId);
        public IEnumerable<CV> GetAll();
        public IEnumerable<CV> GetByOwnerId(Guid ownerId);
    }
}
