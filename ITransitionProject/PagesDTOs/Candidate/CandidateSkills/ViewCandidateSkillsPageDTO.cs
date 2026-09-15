using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace ITransitionProject.PagesDTOs.Candidate.CandidateSkills
{
    public class ViewCandidateSkillsPageDTO
    {
        public IEnumerable<CandidateSkill> CandidateSkills { get; set; }
    }
}
