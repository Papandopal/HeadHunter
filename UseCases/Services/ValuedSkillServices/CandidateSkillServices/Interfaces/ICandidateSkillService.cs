using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using UseCases.Services.ValuedSkillServices.GeneralDTOs;

namespace UseCases.Services.ValuedSkillServices.CandidateSkillServices.Interfaces
{
    public interface ICandidateSkillService
    {
        public void Add(AddValuedSkillDTO skillDTO, Guid candidateId);
        public void AddRange(IEnumerable<AddValuedSkillDTO> skillDTOs, Guid candidateId);
        public void UpdateCandidateSkills(IEnumerable<UpdateValuedSkillDTO> skillDTOs);
        public CandidateSkill GetByName(Guid ownerId, string name);
        public IEnumerable<CandidateSkill> GetCandidateSkillsByOwnerId(Guid ownerId);
        public IEnumerable<CandidateSkill> GetCandidateSkillsByOwnerIdWithPrefix(Guid ownerId, string prefix);
    }
}
