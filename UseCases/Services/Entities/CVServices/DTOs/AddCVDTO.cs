using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.Services.CVServices.DTOs
{
    public class AddCVDTO
    {
        public Guid PositionId { get; set; }
        public string BufferForNotValuedSkills { get; set; } = string.Empty;
        public string BufferForValuedSkills { get; set;} = string.Empty;
    }
}
