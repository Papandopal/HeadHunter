using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace UseCases.Services.RecruterServices.DTOs
{
    public class RecruterProfileDTO
    {
        public string Name { get; set; } = string.Empty;
        public required IEnumerable<Skill> Skills { get; set; }
    }
}
