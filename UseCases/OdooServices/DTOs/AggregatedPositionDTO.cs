using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.OdooServices.DTOs
{
    public class AggregatedPositionDTO
    {
        public string PositionId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public List<AggregatedSkillDTO> Skills { get; set; } = new();
    }
}
