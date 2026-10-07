using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.Services.Entities.ValuedSkillServices.CandidateSkillServices.Interfaces;

namespace UseCases.Services.Entities.ValuedSkillServices.CandidateSkillServices
{
    public class OneOfManyAggregater : IOneOfManyAggregater
    {
        string IOneOfManyAggregater.Aggregate(IEnumerable<string> values)
        {
            return values.GroupBy(x=>x).OrderByDescending(x=>x.Count()).ThenBy(x=>x.Key).FirstOrDefault()?.Key ?? string.Empty;
        }
    }
}
