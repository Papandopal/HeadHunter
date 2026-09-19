using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace UseCases.Services.CategoryServices.Interfaces
{
    public interface ICategoryService 
    {
        public void Add(Category category);
        public IEnumerable<Category> GetAll();
        public Task<Category> GetByNameAsync(string name);
    }
}
