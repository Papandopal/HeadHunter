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

        void IProjectRepository.DeleteRange(IEnumerable<Project> projects)
        {
            this.projects.RemoveRange(projects);
        }

        IQueryable<Project> IRepository<Project>.GetAll()
        {
            return projects;
        }

        IEnumerable<Project> IProjectRepository.GetAllByOwnerIdExceptOf(Guid ownerId, IEnumerable<Guid> ids, uint limit = 0)
        {
            if (limit == 0) return projects.Where(x => x.OwnerId == ownerId && !ids.Contains(x.Id));
            return projects.Where(x => x.OwnerId == ownerId && !ids.Contains(x.Id)).Take((int)limit);
        }

        Project IRepository<Project>.GetById(Guid id)
        {
            return projects.First(x => x.Id == id);
        }

        IEnumerable<Project> IProjectRepository.GetByIds(IEnumerable<Guid> ids)
        {
            return projects.Where(x => ids.Contains(x.Id));
        }

        IEnumerable<Project> IProjectRepository.GetByOwnerId(Guid ownerId)
        {
            return projects.Where(x => x.OwnerId == ownerId);
        }

        IEnumerable<Project> IProjectRepository.GetPersonaledProjectsByTags(Guid ownerId, IEnumerable<ProjectTag> tags, uint limit)
        {
            List<Project> result = new();
            foreach (var tag in tags)
            {
                result.AddRange(projects.Where(x => x.OwnerId == ownerId && x.ProjectTags.Select(y => y.Name).Contains(tag.Name)));
                if (result.Count > limit) break;
            }
            return result.Take((int)limit);
        }

        bool IRepository<Project>.IsExists(Project entity)
        {
            return projects.FirstOrDefault(x => x.Id == entity.Id) is not null;
        }

        void IProjectRepository.RemoveAll()
        {
            projects.ExecuteDelete();
        }

        void IRepository<Project>.Update(Project entity)
        {
            projects.Update(entity);
        }

        void IProjectRepository.UpdateRange(IEnumerable<Project> projects)
        {
            this.projects.UpdateRange(projects);
        }
    }
}
