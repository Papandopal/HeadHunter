using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public record Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public long Version { get; set; } = 0;
    }
}
