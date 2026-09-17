using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProjectTag
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public IEnumerable<Position> Positions { get; set; } = new List<Position>(); 
        public IEnumerable<Project> Projects { get; set; } = new List<Project>();
        public int Version { get; init; } = 0;
    }
}
