using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.S3.Model;
using Domain.Entities;
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

        void IProjectTagRepository.Update(ProjectTag tag)
        {
            tag.Popularity++;
        }

        void IProjectTagRepository.UpdateRange(IEnumerable<ProjectTag> tags)
        {
            foreach(var tag in tags)
            {
                tag.Popularity++;
            }
            projectTags.UpdateRange(tags);
        }

        void IRepository<ProjectTag>.Delete(Guid id)
        {
            var entity = projectTags.First(x => x.Id == id);
            projectTags.Remove(entity);
        }

        IQueryable<ProjectTag> IRepository<ProjectTag>.GetAll()
        {
            return projectTags;
        }

        ProjectTag IRepository<ProjectTag>.GetById(Guid id)
        {
            return projectTags.First(x => x.Id == id);
        }

        IEnumerable<ProjectTag> IProjectTagRepository.GetByNamesOrDefault(IEnumerable<string> names)
        {
            List<ProjectTag> existedTags = projectTags.Where(x => names.Contains(x.Name)).ToList();
            var existedTagsNames = existedTags.Select(x => x.Name).ToHashSet();
            IEnumerable<ProjectTag> addedTags = names.Where(x => !existedTagsNames.Contains(x)).Select(x=> new ProjectTag { Name = x });
            existedTags.AddRange(addedTags);
            return existedTags;
        }

        IEnumerable<string> IProjectTagRepository.GetNamesByPrefix(string? prefix)
        {
            if (prefix is null) return projectTags.Select(x => x.Name);
            else return projectTags.Where(x => x.Name.StartsWith(prefix)).Select(x => x.Name);
        }

        IEnumerable<ProjectTag> IProjectTagRepository.GetPopularTags(uint limit)
        {
            return projectTags.OrderBy(x => x.Popularity).Take((int)limit);
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
