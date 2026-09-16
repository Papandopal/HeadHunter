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
    public class ProjectRepository(AppDbContext dbContext) : IProjectRepository
    {
        private DbSet<Project> projects = dbContext.Set<Project>();
        void IRepository<Project>.Add(Project entity)
        {
            projects.Add(entity);
        }

        void IProjectRepository.AddRange(IEnumerable<Project> projects)
        {
            this.projects.AddRange(projects);
        }

        void IRepository<Project>.Delete(Guid id)
        {
            var project = projects.First(x => x.Id == id);
            projects.Remove(project);
        }

        IQueryable<Project> IRepository<Project>.GetAll()
        {
            return projects;
        }

        Project IRepository<Project>.GetById(Guid id)
        {
            return projects.First(x => x.Id == id);
        }

        IEnumerable<Project> IProjectRepository.GetByOwnerId(Guid ownerId)
        {
            return projects.Where(x => x.OwnerId == ownerId);
        }

        bool IRepository<Project>.IsExists(Project entity)
        {
            return projects.FirstOrDefault(x => x.Id == entity.Id) is not null;
        }

        void IRepository<Project>.Update(Project entity)
        {
            projects.Update(entity);
        }
    }
}
