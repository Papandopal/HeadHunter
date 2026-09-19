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
        public Candidate? GetItemOrDefaultByOwnerId(Guid ownerId);
    }
}
