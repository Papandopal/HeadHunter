using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using UseCases.Services.AccessRuleServices.DTOs;

namespace UseCases.Services.AccessRuleServices.Interfaces
{
    public interface IAccessRuleService
    {
        public AccessRule GetById(Guid id);
        public AccessRule GetFromRecords(IEnumerable<AccessRuleRecord> records);
    }
}
