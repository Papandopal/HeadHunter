using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.Database.Repositories;

namespace UseCases.Database
{
    public interface IUnitOfWork
    {
        public IUserRepository UserRepository { get; }
        public ICandidateRepository CandidateRepository { get; }
        public ICandidateSkillRepository CandidateSkillRepository { get; }
        public ISkillRepository SkillRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public IRecruterRepostory RecruterRepostory { get; }
        public IAccessRuleRepository AccessRuleRepository { get; }
        public IPositionRepository PositionRepository { get; }
        public IPositionSkillRepositiry PositionSkillRepositiry { get; }
        public IProjectTagRepository ProjectTagRepository { get; }
        public ICVRepository CVRepository { get; }
        public IProjectRepository ProjectRepository { get; }
        public void StartTransaction();
        public Task StartTransactionAsync();
        public void Commit();
        public Task CommitAsync();
        public void Rollback();
        public Task RollbackAsync();    
    }
}
