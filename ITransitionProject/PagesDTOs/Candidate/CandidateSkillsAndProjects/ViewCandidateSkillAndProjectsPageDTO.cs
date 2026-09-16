using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace ITransitionProject.PagesDTOs.Candidate.CandidateSkillsAndProjects
{
    public class ViewCandidateSkillsAndProjectsPageDTO
    {
        public IEnumerable<CandidateSkill> CandidateSkills { get; set; }
        public IEnumerable<Project> Projects { get; set; }
    }
}
