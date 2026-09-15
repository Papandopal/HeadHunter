using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITransitionProject.PagesDTOs.Recruter
{
    public class AddCategoryPageDTO
    {
        public string Name { get; set; } = string.Empty;
        public string ActionForSubmit { get; set; } = string.Empty;
        public string ControllerForSubmit { get; set; } = string.Empty;
    }
}
