using System;
using Microsoft.AspNetCore.Mvc;
using UseCases.Services.PositionServices.Interfaces;
using UseCases.Services.ValuedSkillServices.CandidateSkillServices.Interfaces;

namespace ITransitionProject.Controllers
{
    public class OdooController(IPositionService positionService, ICandidateSkillService candidateSkillService) : Controller
    {
        public class AggregatedPositionDTO
        {
            public string PositionId { get; set; } = string.Empty;
            public string Title { get; set; } = string.Empty;
            public List<AggregatedSkillDTO> Skills { get; set; } = new();
        }

        public class AggregatedSkillDTO
        {
            public string Name { get; set; } = string.Empty;
            public string Type { get; set; } = string.Empty;
            public string Value { get; set; } = string.Empty;
        }

        public IEnumerable<AggregatedPositionDTO> Aggregate([FromQuery] string? positionIds = null)
        {
            if (string.IsNullOrWhiteSpace(positionIds))
                return Array.Empty<AggregatedPositionDTO>();

            IEnumerable<Guid> guids = positionIds
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(x => Guid.Parse(x));

            if (guids.Count() == 0)
                return Array.Empty<AggregatedPositionDTO>();

            var positions = positionService.GetByIds(guids);

            var result = positions.Select(x => new AggregatedPositionDTO
            {
                PositionId = x.Id.ToString(),
                Title = x.Title,
                Skills = x.PositionSkills.Select(y => new AggregatedSkillDTO
                {
                    Name = y.Skill.Name,
                    Type = y.Skill.Type.ToString(),
                    Value = candidateSkillService.AggregatedValueBySkill(y.Skill)
                }).ToList()
            });
            return result;
        }
    }
}
