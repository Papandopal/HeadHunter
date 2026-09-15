using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;

namespace UseCases.Services.ValuedSkillServices.PositionSkillsServices.DTOs
{
    public class AddPositionSkillDTO
    {
        public string SkillName { get; set; } = string.Empty;
    }
}
