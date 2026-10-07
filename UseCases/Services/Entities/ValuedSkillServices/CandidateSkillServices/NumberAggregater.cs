using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.Services.Entities.ValuedSkillServices.CandidateSkillServices.Interfaces;

namespace UseCases.Services.Entities.ValuedSkillServices.CandidateSkillServices
{
    public class NumberAggregater : INumberAggregater
    {
        double INumberAggregater.Aggreagate(IEnumerable<long> values)
        {
            return (double)values.Sum() / values.Count();
        }
    }
}
