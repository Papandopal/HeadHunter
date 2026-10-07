using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.Services.Entities.ValuedSkillServices.CandidateSkillServices.Interfaces
{
    public interface INumberAggregater
    {
        public double Aggreagate(IEnumerable<long> values);
    }
}
