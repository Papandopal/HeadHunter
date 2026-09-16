using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using UseCases.Services.ProjectServices.DTOs;

namespace UseCases.Services.ProjectServices.Interfaces
{
    public interface IProjectService
    {
        public void Add(AddProjectDTO projectDTO, Guid ownerId);
        public void AddRange(IEnumerable<AddProjectDTO> projectDTOs, Guid ownerId);
        public IEnumerable<AddProjectDTO> Deserialize(IEnumerable<ProjectRecordDTO> dtos);
        public IEnumerable<Project> GetByOwnerId(Guid ownerId);
    }
}
