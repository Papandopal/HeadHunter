using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace UseCases.Database.Repositories
{
    public interface ICandidateRepository : IRepository<Candidate>
    {
        public void AddByUser(User user);
        public void UpdateRange(IEnumerable<Candidate> candidates);
        public Candidate GetByOwnerId(Guid id);
        public IEnumerable<Candidate> GetByIds(IEnumerable<Guid> ids);
    }
}
