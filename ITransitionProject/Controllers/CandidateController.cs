using System.Text.Json;
using Domain;
using Domain.Entities;
using Domain.Enums;
using ITransitionProject.PagesDTOs.Candidate.CandidateSkills;
using ITransitionProject.PagesDTOs.Candidate.CVs;
using ITransitionProject.PagesDTOs.Candidate.Positions;
using ITransitionProject.PagesDTOs.Candidate.Profile;
using Microsoft.AspNetCore.Mvc;
using UseCases.Services.AuthServices.Interfaces;
using UseCases.Services.CandidateServices.DTOs;
using UseCases.Services.CandidateServices.Interfaces;
using UseCases.Services.PositionServices.Interfaces;
using UseCases.Services.SkillServices.Interfaces;
using UseCases.Services.ValuedSkillServices.CandidateSkillServices.Interfaces;
using UseCases.Services.ValuedSkillServices.GeneralDTOs;

namespace ITransitionProject.Controllers
{
    [EnumAuthorize(UserRoles.Candidate)]
    public class CandidateController(IAuthService authService, ICandidateSkillService candidateSkillService, ISkillService skillService,
        ICandidateService candidateService, IPositionService positionService) : Controller
    {

        private Candidate? Candidate()
        {
            return candidateService.GetItemOrDefaultByOwnerId(authService.User().Id);
        }

        [HttpGet]
        public IActionResult Home()
        {
            authService.Validate();
            if (candidateService.GetItemOrDefaultByOwnerId(authService.User().Id) is null)
            {
                var dto = new CreateCandidateProfilePageDTO
                {
                    ControllerForSubmit = ControllerContext.ActionDescriptor.ControllerName,
                    ActionForSubmit = "CreateProfile",
                };
                return View("Profile/CreateProfile", dto);
            }
            return View("/Profile/Home");
        }

        [HttpPost]
        public IActionResult CreateProfile(CreateCandidateProfileDTO dto)
        {
            var newCandidate = new Candidate
            {
                UserId = authService.User().Id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Birthday = dto.BirthDay
            };
            candidateService.Add(newCandidate);
            return RedirectToAction("Profile");
        }


        [HttpGet]
        public IActionResult Profile()
        {
            var currentUser = authService.User();
            var candidate = Candidate();
            if (candidate is null) return RedirectToAction("Home");
            var dto = new CandidateProfilePageDTO
            {
                FirstName = candidate.FirstName,
                LastName = candidate.LastName,
                CandidateSkills = candidate.Skills
            };
            return View("Profile/Profile", dto);
        }

        [HttpGet]
        public IActionResult EditCandidateSkills()
        {
            var currentUser = authService.User();
            var candidate = Candidate();
            if (candidate is null) return RedirectToAction("Home");
            var skills = candidateSkillService.GetCandidateSkillsByOwnerId(candidate.Id);
            return View("CandidateSkills/CandidateSkillsEdit",
                new EditCandidateSkillsPageDTO
                {
                    CandidateSkills = skills,
                    ActionToSubmit = "UpdateCandidateSkills",
                    ControllerToSubmit = ControllerContext.ActionDescriptor.ControllerName,
                    CountOfRequiredProperties = 0
                });
        }

        [HttpPost]
        public IActionResult UpdateCandidateSkills(string json)
        {
            List<UpdateValuedSkillDTO> dtos = JsonSerializer.Deserialize<List<UpdateValuedSkillDTO>>(json);
            candidateSkillService.UpdateCandidateSkills(dtos);
            return RedirectToAction("Profile");
        }

        [HttpGet]
        public IActionResult AddCandidateSkill()
        {
            var model = new AddCandidateSkillSelectTypePageDTO
            {
                ActionForGetSkillsNames = "GetSkillsNames",
                ControllerForGetSkillsNames = $"{ControllerContext.ActionDescriptor.ControllerName}",
                ActionForGetForm = "GetAddingForm",
                ControllerForGetForm = $"{ControllerContext.ActionDescriptor.ControllerName}"
            };
            return View("CandidateSkills/CandidateSkillAddSelectType", model);
        }

        [HttpGet]
        public IEnumerable<string> GetSkillsNames(string prefix)
        {
            authService.Validate();
            var skills = skillService.GetAllSkillsNamesByPrefix(prefix);
            return skills;
        }

        [HttpGet]
        public IActionResult GetAddingForm(string skillName)
        {
            CandidateSkill CandidateSkill = new CandidateSkill { Skill = skillService.GetByName(skillName) };
            var dto = new AddCandidateSkillFormPageDTO
            {
                Skill = CandidateSkill.Skill,
                ActionForSubmit = "AddCandidateSkill",
                ControllerForSubmit = $"{ControllerContext.ActionDescriptor.ControllerName}",
                CountOfRequiredProperties = CandidateSkill.Skill.Type == SkillTypes.Period ? 2 : 1
            };
            return PartialView("CandidateSkills/CandidateSkillAddForm", dto);
        }

        [HttpPost]
        public IActionResult AddCandidateSkill(string buffer)
        {//to service
            IEnumerable<AddValuedSkillDTO> dto = JsonSerializer.Deserialize<IEnumerable<AddValuedSkillDTO>>(buffer);
            var item = dto.First();
            var skill = skillService.GetById(item.SkillId);
            var candidate = Candidate();
            CandidateSkill newCandidateSkill = new CandidateSkill
            {
                CandidateId = candidate.Id,
                Skill = skill,
                SkillId = skill.Id,
                Value = item.Value
            };
            candidateSkillService.Add(newCandidateSkill);
            return RedirectToAction("AddCandidateSkill");
        }

        [HttpGet]
        public IActionResult ViewPositions()
        {
            var positions = positionService.GetAll();
            var dto = new ViewReadOnlyPositionsPageDTO
            {
                Positions = positions,
                ActionForViewPosition = "ViewPosition",
                ControllerForViewPosition = ControllerContext.ActionDescriptor.ControllerName
            };
            return View("Positions/PositionsReadOnlyView", dto);
        }

        [HttpGet]
        public IActionResult ViewPosition(Guid positionId)
        {
            var position = positionService.GetById(positionId);
            var dto = new ViewReadOnlyPositionPageDTO
            {
                Position = position,
                ActionForGenerateCV = "GenerateCV",
                ControllerForGenerateCV = ControllerContext.ActionDescriptor.ControllerName
            };
            return View("Positions/PositionReadOnlyView", dto);
        }
        [HttpGet]
        public IActionResult GenerateCV(Guid positionId)
        {
            Position position = positionService.GetById(positionId);
            IEnumerable<PositionSkill> positionSkills = position.PositionSkills;
            IEnumerable<CandidateSkill> candidateSkills = candidateSkillService.GetCandidateSkillsByOwnerId(Candidate().Id);
            var valuedSkills = new List<CandidateSkill>();
            var notValuedSkills = new List<Skill>();
            foreach (var skill in positionSkills)
            {
                CandidateSkill? candidateSkill = candidateSkills.FirstOrDefault(x => x.SkillId == skill.SkillId);
                if (candidateSkill is not null) valuedSkills.Add(candidateSkill);
                else notValuedSkills.Add(skill.Skill);
            }
            var dto = new AddCVPageDTO
            {
                ValuedSkills = valuedSkills,
                NotValuedSkills = notValuedSkills
            };
            return View("");
        }
    }
}
