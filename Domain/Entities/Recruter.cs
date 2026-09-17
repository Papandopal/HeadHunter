using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Recruter
    {
        public Guid Id { get; init; }
        public Guid OwnerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public IEnumerable<CV> LikedCVs { get; set; }
        public int Version { get; init; } = 0;
    }
}
