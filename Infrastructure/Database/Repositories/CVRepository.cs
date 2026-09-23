using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using UseCases.Database.Repositories;

namespace Infrastructure.Database.Repositories
{
    public class CVRepository(AppDbContext dbContext) : ICVRepository
    {
        private DbSet<CV> cvs = dbContext.Set<CV>();
        void IRepository<CV>.Add(CV entity)
        {
            cvs.Add(entity);
        }

        void IRepository<CV>.Delete(Guid id)
        {
            var cv = cvs.First(c => c.Id == id);
            cvs.Remove(cv);
        }

        IQueryable<CV> IRepository<CV>.GetAll()
        {
            return cvs.Include(x=>x.Candidate).ThenInclude(x=>x.Skills);
        }

        CV IRepository<CV>.GetById(Guid id)
        {
            return cvs
                .Include(x=>x.Candidate)
                    .ThenInclude(x=>x.Projects)
                .Include(x=>x.Position)
                    .ThenInclude(x=>x.ProjectTags)
                .Include(x=>x.LikedRecruters)
                .First(x=>x.Id == id);
        }

        IEnumerable<CV> ICVRepository.GetByOwnerId(Guid ownerId)
        {
            return cvs.Where(x=>x.CandidateId == ownerId);
        }

        IEnumerable<CV> ICVRepository.GetByPositionId(Guid positionId)
        {
            return cvs.Where(x=>x.PositionId == positionId);
        }

        IEnumerable<CV> ICVRepository.GetByPositionIds(IEnumerable<Guid> positionIds)
        {
            return cvs.Where(x=> positionIds.Contains(x.PositionId));
        }

        bool ICVRepository.IsCVLikedBy(Guid cvId, Recruter user)
        {
            return cvs.Include(x=>x.LikedRecruters).First(x=>x.Id == cvId).LikedRecruters.Contains(user);
        }

        bool IRepository<CV>.IsExists(CV entity)
        {
            return cvs.FirstOrDefault(x=>x.Id == entity.Id) is not null;
        }

        void ICVRepository.Like(Guid cvId, Recruter user)
        {
            var cv = cvs.Include(x => x.LikedRecruters).First(x => x.Id == cvId);
            var oldLikedRecruters = cv.LikedRecruters.ToList();
            oldLikedRecruters.Add(user);
            cv.LikedRecruters = oldLikedRecruters;
            //dbContext.Entry(cv).Property(x => x.LikedRecruters).IsModified = true;
            cv.Likes++;
        }

        void ICVRepository.Unlike(Guid cvId, Recruter user)
        {
            var cv = cvs.Include(x => x.LikedRecruters).First(x => x.Id == cvId);
            var oldLikedRecruters = cv.LikedRecruters.ToList();
            oldLikedRecruters.Remove(user);
            cv.LikedRecruters = oldLikedRecruters;
            //dbContext.Entry(cv).Property(x=>x.LikedRecruters).IsModified = true;
            cv.Likes--;
        }

        void IRepository<CV>.Update(CV entity)
        {
            cvs.Update(entity);
        }
    }
}
