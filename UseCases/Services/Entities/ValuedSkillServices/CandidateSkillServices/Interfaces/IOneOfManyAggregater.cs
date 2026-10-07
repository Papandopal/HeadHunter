using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.Services.Entities.ValuedSkillServices.CandidateSkillServices.Interfaces
{
    public interface IOneOfManyAggregater
    {
        public string Aggregate(IEnumerable<string> values);
    }
}
