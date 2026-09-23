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
        public void Like(Guid cvId, Recruter user);
        public void Unlike(Guid cvId, Recruter user);
        public bool IsCVLikedBy(Guid cvId, Recruter user);
        public CV GetById(Guid id);
        public IEnumerable<CV> GetAll();
        public IEnumerable<CV> GetByOwnerId(Guid ownerId);
        public IEnumerable<CV> GetByPositionId(Guid positionId);
        public IEnumerable<CV> GetByPositionIds(IEnumerable<Guid> positionIds);
    }
}
