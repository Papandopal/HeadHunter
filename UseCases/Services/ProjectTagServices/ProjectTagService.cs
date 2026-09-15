using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using UseCases.Services.ProjectTagServices.DTOs;
using UseCases.Services.ProjectTagServices.Interfaces;

namespace UseCases.Services.ProjectTagServices
{
    public class ProjectTagService : IProjectTagService
    {
        ProjectTag IProjectTagService.GetFromRecord(ProjectTagRecord projectTagRecord)
        {
            return new ProjectTag { Name = projectTagRecord.Name };
        }
    }
}
