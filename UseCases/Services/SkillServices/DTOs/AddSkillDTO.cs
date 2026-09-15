using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.Services.SkillServices.DTOs
{
    public class AddSkillDTO
    {
        public string Name { get; set; } = string.Empty;    
        public string PotencialValue { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public string TypeName {  get; set; } = string.Empty;
    }
}
