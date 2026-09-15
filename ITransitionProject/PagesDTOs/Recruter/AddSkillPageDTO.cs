using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;

namespace ITransitionProject.PagesDTOs.Recruter
{
    public class AddSkillPageDTO
    {
        public string ActionToSubmit { get; set; } = string.Empty;
        public string ControllerToSubmit { get; set; } = string.Empty;
        public IEnumerable<Category> Categories { get; set; } = Enumerable.Empty<Category>();
        public int CountOfRequiredProperties { get; set; }
    }
}
