using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace UseCases.Database.Repositories
{
    public interface ICVRepository : IRepository<CV>
    {
        public void Like(Guid cvId, Recruter user);
        public void Unlike(Guid cvId, Recruter user);
        public bool IsCVLikedBy(Guid cvId, Recruter user);
        public IEnumerable<CV> GetByOwnerId(Guid ownerId);
        public IEnumerable<CV> GetByPositionId(Guid positionId);
        public IEnumerable<CV> GetByPositionIds(IEnumerable<Guid> positionIds);
    }
}
