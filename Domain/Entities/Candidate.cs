using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Candidate
    {
        public Guid Id { get; init; }
        public Guid UserId { get; init; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateOnly Birthday { get; set; }
        public IEnumerable<CandidateSkill> Skills { get; set; }  = new List<CandidateSkill>();
        public IEnumerable<Project> Projects { get; set; } = new List<Project>();
    }
}
