using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.Database.Repositories
{
    public interface IRepository<T>
    {
        public bool IsExists(T entity);
        public T GetById(Guid id);
        public IQueryable<T> GetAll();
        public void Add(T entity);
        public void Delete(Guid id);
        public void Update(T entity);
    }
}
