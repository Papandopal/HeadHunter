using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using UseCases.Database;
using UseCases.Services.CategoryServices.Interfaces;

namespace UseCases.Services.CategoryServices
{
    public class CategoryService(IUnitOfWork unitOfWork) : ICategoryService
    {
        void ICategoryService.Add(Category category)
        {
            unitOfWork.StartTransaction();
            unitOfWork.CategoryRepository.Add(category);
            unitOfWork.Commit();
        }

        IEnumerable<Category> ICategoryService.GetAll()
        {
            return unitOfWork.CategoryRepository.GetAll();
        }

        async Task<Category> ICategoryService.GetByNameAsync(string name)
        {
            return await unitOfWork.CategoryRepository.GetByNameAsync(name);
        }
    }
}
