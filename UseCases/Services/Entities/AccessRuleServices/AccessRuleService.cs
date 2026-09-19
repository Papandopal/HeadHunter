using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;
using UseCases.Services.AccessRuleServices.DTOs;
using UseCases.Services.AccessRuleServices.Interfaces;

namespace UseCases.Services.AccessRuleServices
{
    public class AccessRuleService : IAccessRuleService
    {
        AccessRule IAccessRuleService.GetFromRecords(IEnumerable<AccessRuleRecord> records)
        {
            if (records.Count() != 2) throw new ArgumentException("invalid arguments for construct AccessRule");
            FilterOperators filter = Enum.Parse<FilterOperators>(records.First(x=>x.PropName == "Operator").PropValue);
            string value = records.First(x => x.PropName == "Value").PropValue;
            return new AccessRule { SkillId = records.First().SkillId, Operator = filter, Value = value };
        }
    }
}
