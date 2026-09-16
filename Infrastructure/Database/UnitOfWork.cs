using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Json;
using UseCases.Database;
using UseCases.Database.Repositories;

namespace Infrastructure.Database
{
    public class UnitOfWork(AppDbContext appDbContext) : IUnitOfWork
    {
        private IDbContextTransaction? dbTransaction = null;
        IUserRepository IUnitOfWork.UserRepository => new UserRepository(appDbContext);
        ICandidateRepository IUnitOfWork.CandidateRepository => new CandidateRepository(appDbContext);
        ICandidateSkillRepository IUnitOfWork.CandidateSkillRepository => new CandidateSkillRepository(appDbContext);
        ISkillRepository IUnitOfWork.SkillRepository => new SkillRepository(appDbContext);
        ICategoryRepository IUnitOfWork.CategoryRepository => new CategoryRepository(appDbContext);
        IRecruterRepostory IUnitOfWork.RecruterRepostory => new RecruterRepository(appDbContext);
        IAccessRuleRepository IUnitOfWork.AccessRuleRepository => new AccessRuleRepository(appDbContext);
        IPositionRepository IUnitOfWork.PositionRepository => new PositionRepository(appDbContext);
        IPositionSkillRepositiry IUnitOfWork.PositionSkillRepositiry => new PositionSkillRepository(appDbContext);
        IProjectTagRepository IUnitOfWork.ProjectTagRepository => new ProjectTagRepository(appDbContext);
        ICVRepository IUnitOfWork.CVRepository => new CVRepository(appDbContext);
        IProjectRepository IUnitOfWork.ProjectRepository => new ProjectRepository(appDbContext);

        void IUnitOfWork.StartTransaction()
        {
            dbTransaction?.Rollback();
            dbTransaction = appDbContext.Database.BeginTransaction();
        }

        async Task IUnitOfWork.StartTransactionAsync()
        {
            if (dbTransaction is not null) await dbTransaction.RollbackAsync();
            dbTransaction = await appDbContext.Database.BeginTransactionAsync();
        }

        void IUnitOfWork.Commit()
        {
            appDbContext.SaveChanges();
            dbTransaction?.Commit();
            dbTransaction?.Dispose();
            dbTransaction = null;
        }

        async Task IUnitOfWork.CommitAsync()
        {
            if (dbTransaction is not null)
            {
                await appDbContext.SaveChangesAsync();
                await dbTransaction.CommitAsync();
                await dbTransaction.DisposeAsync();
                dbTransaction = null;
            }
        }

        void IUnitOfWork.Rollback()
        {
            dbTransaction?.Rollback();
            dbTransaction?.Dispose();
            dbTransaction = null;
        }

        async Task IUnitOfWork.RollbackAsync()
        {
            if (dbTransaction is not null)
            {
                await dbTransaction.RollbackAsync();
                await dbTransaction.DisposeAsync();
                dbTransaction = null;
            }
        }
    }
}
