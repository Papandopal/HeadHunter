using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace ITransitionProject.PagesDTOs.Recruter.Profile
{
    public class RecruterHomePageDTO
    {
        public required Domain.Entities.Recruter Recruter { get; set; } 
        public required IEnumerable<Position> Positions { get; set; }
        public required string ActionForAddPosition { get; set; }
        public required string ControllerForAddPosition { get; set; }
        public required string ActionForDeletePositions { get; set; }
        public required string ControllerForDeletePositions { get; set; }
        public required string ActionForViewPosition { get; set; }
        public required string ControllerForViewPosition { get; set; }
    }
}
