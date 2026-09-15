using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace ITransitionProject.PagesDTOs.Recruter.Skills
{
    public class EditSkillsPageDTO
    {
        public required IEnumerable<Skill> Skills { get; set; }
        public required IEnumerable<Category> Categories { get; set; }
        public string ActionToSubmit { get; set; } = string.Empty;  
        public string ControllerToSubmit {  get; set; } = string.Empty;
    }
}
