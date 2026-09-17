using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CV
    {
        public Guid Id { get; set; }
        public Guid PositionId { get; set; }
        public Position Position { get; set; }
        public Guid CandidateId { get; set; }
        public Candidate Candidate {  get; set; }
        public IEnumerable<CandidateSkill> ValuedSkills { get; set; } = new List<CandidateSkill>();
        public long Likes { get; set; }
        public IEnumerable<Recruter> LikedRecruters { get; set; } = new List<Recruter>();
        public int Version { get; init; } = 0;
    }
}
