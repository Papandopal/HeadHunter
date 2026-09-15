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
    internal class CategoryRepository(AppDbContext dbContext) : ICategoryRepository
    {
        private DbSet<Category> categories = dbContext.Set<Category>();    
        void IRepository<Category>.Add(Category entity)
        {
            categories.Add(entity);
        }

        void IRepository<Category>.Delete(Guid id)
        {
            categories.Remove(categories.First(x => x.Id == id));
        }

        IQueryable<Category> IRepository<Category>.GetAll()
        {
            return categories;
        }

        Category IRepository<Category>.GetById(Guid id)
        {
            return categories.First(x => x.Id == id);
        }

        async Task<Category> ICategoryRepository.GetByNameAsync(string name)
        {
            return await categories.FirstAsync(x => x.Name == name);
        }

        bool IRepository<Category>.IsExists(Category entity)
        {
            return categories.FirstOrDefault(x => x.Id == entity.Id) is not null;
        }

        void IRepository<Category>.Update(Category entity)
        {
            categories.Update(entity);
        }
    }
}
