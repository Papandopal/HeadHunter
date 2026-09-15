using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Server.Kestrel.Transport.NamedPipes;
using Microsoft.EntityFrameworkCore;
using UseCases.Database.Repositories;

namespace Infrastructure.Database.Repositories
{
    public class ProjectTagRepository(AppDbContext dbContext) : IProjectTagRepository
    {
        private DbSet<ProjectTag> projectTags = dbContext.Set<ProjectTag>();
        void IRepository<ProjectTag>.Add(ProjectTag entity)
        {
            projectTags.Add(entity);
        }

        void IRepository<ProjectTag>.Delete(Guid id)
        {
            var entity = projectTags.First(x=>x.Id == id);
            projectTags.Remove(entity);
        }

        IQueryable<ProjectTag> IRepository<ProjectTag>.GetAll()
        {
            return projectTags;
        }

        ProjectTag IRepository<ProjectTag>.GetById(Guid id)
        {
            return projectTags.First(x=>x.Id==id);
        }

        bool IRepository<ProjectTag>.IsExists(ProjectTag entity)
        {
            return projectTags.FirstOrDefault(x => x.Id == entity.Id) is not null;
        }

        void IRepository<ProjectTag>.Update(ProjectTag entity)
        {
            projectTags.Update(entity);
        }
    }
}
