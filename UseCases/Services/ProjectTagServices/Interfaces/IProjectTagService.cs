using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using UseCases.Services.ProjectTagServices.DTOs;

namespace UseCases.Services.ProjectTagServices.Interfaces
{
    public interface IProjectTagService
    {
        public ProjectTag GetFromRecord(ProjectTagRecord projectTagRecord);
    }
}
