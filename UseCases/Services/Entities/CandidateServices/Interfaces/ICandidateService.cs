using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace UseCases.Services.CandidateServices.Interfaces
{
    public interface ICandidateService
    {
        public void Add(Candidate candidate);
        public void Update(Candidate candidate);
        public void UpdateRange(IEnumerable<Candidate> candidates);
        public void Block(Guid id);
        public void BlockRange(IEnumerable<Guid> ids);
        public void Unblock(Guid id);
        public void UnblockRange(IEnumerable<Guid> ids);
        public void Delete(Guid id);
        public void DeleteRange(IEnumerable<Guid> ids);
        public bool IsBlocked(Candidate candidate); 
        public IDictionary<Guid, bool> IsBlockedRange(IEnumerable<Candidate> candidates);
        public Candidate? GetItemOrDefaultByOwnerId(Guid ownerId);
        public Candidate GetById(Guid id);
        public IEnumerable<Candidate> GetAll();
        public IEnumerable<Candidate> GetByIds(IEnumerable<Guid> ids);
    }
}
