using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace UseCases.Services.ProjectTagServices.Interfaces
{
    public interface IProjectTagService
    {
        public IEnumerable<ProjectTag> GetFromJSON(string json);
        public IEnumerable<string> GetNamesByPrefix(string? prefix);
    }
}
