using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace ITransitionProject.PagesDTOs.Recruter.Skills
{
    public class ViewSkillsPageDTO
    {
        public required IEnumerable<Skill> Skills { get; set; }
    }
}
