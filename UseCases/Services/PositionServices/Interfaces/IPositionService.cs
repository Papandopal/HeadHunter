using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using UseCases.Services.PositionServices.DTOs;

namespace UseCases.Services.PositionServices.Interfaces
{
    public interface IPositionService
    {
        public void AddPosition(AddPositionDTO positionDTO, Guid ownerId);
        public void DeleteRange(IEnumerable<Guid> ids);
        public Position GetById(Guid id);
        public IEnumerable<Position> GetAll();
    }
}
