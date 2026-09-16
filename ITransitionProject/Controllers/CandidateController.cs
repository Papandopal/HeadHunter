using System.Text.Json;
using Domain;
using Domain.Entities;
using Domain.Enums;
using ITransitionProject.PagesDTOs.Candidate.CandidateSkillsAndProjects;
using ITransitionProject.PagesDTOs.Candidate.CVs;
using ITransitionProject.PagesDTOs.Candidate.Positions;
using ITransitionProject.PagesDTOs.Candidate.Profile;
using Microsoft.AspNetCore.Mvc;
using UseCases.Services.AuthServices.Interfaces;
using UseCases.Services.CandidateServices.DTOs;
using UseCases.Services.CandidateServices.Interfaces;
using UseCases.Services.CVServices.DTOs;
using UseCases.Services.CVServices.Interfaces;
using UseCases.Services.PositionServices.Interfaces;
using UseCases.Services.ProjectServices.DTOs;
using UseCases.Services.ProjectServices.Interfaces;
using UseCases.Services.ProjectTagServices.Interfaces;
using UseCases.Services.SkillServices.Interfaces;
using UseCases.Services.ValuedSkillServices.CandidateSkillServices.DTOs;
using UseCases.Services.ValuedSkillServices.CandidateSkillServices.Interfaces;
using UseCases.Services.ValuedSkillServices.GeneralDTOs;

namespace ITransitionProject.Controllers
{
    [EnumAuthorize(UserRoles.Candidate)]
    public class CandidateController(IAuthService authService, ICandidateSkillService candidateSkillService, ISkillService skillService,
        ICandidateService candidateService, IPositionService positionService, ICVService cVService, IProjectTagService projectTagService,
        IProjectService projectService) : Controller
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
            return View("Profile/Home");
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
            var candidate = Candidate();
            var candidateSkills = candidateSkillService.GetCandidateSkillsByOwnerId(candidate.Id);
            var projects = projectService.GetByOwnerId(candidate.Id).ToList();
            if (candidate is null) return RedirectToAction("Home");
            var dto = new CandidateProfilePageDTO
            {
                FirstName = candidate.FirstName,
                LastName = candidate.LastName,
                CandidateSkills = candidateSkills,
                Projects = projects
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
            var projects = projectService.GetByOwnerId(candidate.Id);
            return View("CandidateSkillsAndProjects/CandidateSkillsAndProjectsEdit",
                new EditCandidateSkillsAndProjectsPageDTO
                {
                    CandidateSkills = skills,
                    Projects = projects,
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
        public IActionResult AddCandidateSkillsAndProjects()
        {
            var model = new AddCandidateSkillsAndProjectsPageDTO
            {
                ActionForGetSkillsNames = "GetSkillsNames",
                ControllerForGetSkillsNames = ControllerContext.ActionDescriptor.ControllerName,
                ActionForGetSkillForm = "GetAddingSkillForm",
                ControllerForGetSkillForm = ControllerContext.ActionDescriptor.ControllerName,
                ActionForGetProjectForm = "GetAddingProjectForm",
                ControllerForGetProjectForm = ControllerContext.ActionDescriptor.ControllerName,
                ActionForSubmit = "AddCandidateSkillsAndProjects",
                ControllerForSubmit = ControllerContext.ActionDescriptor.ControllerName
            };
            return View("CandidateSkillsAndProjects/CandidateSkillsAndProjectsAddMainForm", model);
        }

        [HttpGet]
        public IEnumerable<string> GetSkillsNames(string? prefix)
        {
            authService.Validate();
            var i = HttpContext.Request.Query["prefix"];
            var skills = skillService.GetAllSkillsNamesByPrefix(i).ToList();
            return skills;
        }

        [HttpGet]
        public IEnumerable<string> GetProjectTagsNames(string? prefix)
        {
            authService.Validate();
            var i = HttpContext.Request.Query["prefix"];
            var projectTags = projectTagService.GetNamesByPrefix(i);
            return projectTags;
        }

        [HttpGet]
        public IActionResult GetAddingSkillForm(string skillName)
        {
            CandidateSkill CandidateSkill = new CandidateSkill { Skill = skillService.GetByName(skillName) };
            var dto = new AddCandidateSkillFormPageDTO
            {
                Skill = CandidateSkill.Skill
            };
            return PartialView("CandidateSkillsAndProjects/CandidateSkillAddPartialForm", dto);
        }

        [HttpGet]
        public IActionResult GetAddingProjectForm(int projectIndex)
        {
            var dto = new AddProjectFormPageDTO
            {
                ProjectIndex = projectIndex,
                ActionForGetProjectTags = "GetProjectTagsNames",
                ControllerForGetProjectTags = ControllerContext.ActionDescriptor.ControllerName
            };
            return PartialView("CandidateSkillsAndProjects/CandidateProjectAddPartialForm", dto);
        }

        [HttpPost]
        public IActionResult AddCandidateSkillsAndProjects(AddCandidateSkillsAndProjectsDTO addCandidateSkillsAndProjectsDTO)
        {
            IEnumerable<AddValuedSkillDTO> skillDTOs = JsonSerializer.Deserialize<IEnumerable<AddValuedSkillDTO>>(addCandidateSkillsAndProjectsDTO.BufferForSkills);
            IEnumerable<ProjectRecordDTO> projectsRecordDTOs = JsonSerializer.Deserialize<IEnumerable<ProjectRecordDTO>>(addCandidateSkillsAndProjectsDTO.BufferForProjects);
            IEnumerable<AddProjectDTO> projectDTOs = projectService.Deserialize(projectsRecordDTOs);
            candidateSkillService.AddRange(skillDTOs, Candidate().Id);
            projectService.AddRange(projectDTOs, Candidate().Id);
            return RedirectToAction("Profile");
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
            IEnumerable<CandidateSkill> candidateSkills = candidateSkillService.GetCandidateSkillsByOwnerId(Candidate().Id).ToList();
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
                PositionId = positionId,
                ActionForSubmit = "GenerateCV",
                ValuedSkills = valuedSkills,
                NotValuedSkills = notValuedSkills,
                ControllerForSubmit = ControllerContext.ActionDescriptor.ControllerName
            };
            return View("CVs/CVAdd", dto);
        }

        [HttpPost]
        public IActionResult GenerateCV(AddCVDTO addCVDTO)
        {
            IEnumerable<AddValuedSkillDTO> newSkills = JsonSerializer.Deserialize<IEnumerable<AddValuedSkillDTO>>(addCVDTO.BufferForNotValuedSkills);
            IEnumerable<UpdateValuedSkillDTO> updatedSkillDTOs = JsonSerializer.Deserialize<IEnumerable<UpdateValuedSkillDTO>>(addCVDTO.BufferForValuedSkills);

            if (updatedSkillDTOs.Count() != 0) candidateSkillService.UpdateCandidateSkills(updatedSkillDTOs);

            candidateSkillService.AddRange(newSkills, Candidate().Id);
            cVService.AddCV(Candidate().Id, addCVDTO.PositionId);
            return RedirectToAction("ViewCVs");
        }

        [HttpGet]
        public IActionResult ViewCVs()
        {
            IEnumerable<CV> cvs = cVService.GetByOwnerId(Candidate().Id);
            IEnumerable<Position> positions = positionService.GetByIds(cvs.Select(x => x.PositionId));
            var dto = new ViewCVsPageDTO
            {
                CVs = cvs,
                Positions = positions,
                ActionForViewCV = "ViewCV",
                ControllerForViewCV = ControllerContext.ActionDescriptor.ControllerName
            };
            return View("CVs/CVsView", dto);
        }

        [HttpGet]
        public IActionResult ViewCV(Guid cvId)
        {
            CV cv = cVService.GetById(cvId);
            var dto = new ViewCVPageDTO { CV = cv };
            return View("CVs/CVView", dto);
        }
    }
}
