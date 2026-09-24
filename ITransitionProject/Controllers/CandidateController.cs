using System.Text.Json;
using Domain;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Database.Exceptions;
using ITransitionProject.PagesDTOs.Candidate.CandidateSkillsAndProjects;
using ITransitionProject.PagesDTOs.Candidate.CVs;
using ITransitionProject.PagesDTOs.Candidate.Positions;
using ITransitionProject.PagesDTOs.Candidate.Profile;
using Microsoft.AspNetCore.Mvc;
using UseCases.Services;
using UseCases.Services.AuthServices.Interfaces;
using UseCases.Services.CandidateServices.DTOs;
using UseCases.Services.CandidateServices.Interfaces;
using UseCases.Services.CVServices.DTOs;
using UseCases.Services.CVServices.Interfaces;
using UseCases.Services.Entities.CVServices.DTOs;
using UseCases.Services.Exceptions;
using UseCases.Services.ImageServices.Interfaces;
using UseCases.Services.PositionServices.Interfaces;
using UseCases.Services.ProjectServices.DTOs;
using UseCases.Services.ProjectServices.Interfaces;
using UseCases.Services.ProjectTagServices.Interfaces;
using UseCases.Services.SkillServices.Interfaces;
using UseCases.Services.ValuedSkillServices.CandidateSkillServices.DTOs;
using UseCases.Services.ValuedSkillServices.CandidateSkillServices.Interfaces;
using UseCases.Services.ValuedSkillServices.General.DTOs;

namespace ITransitionProject.Controllers
{
    [EnumAuthorize(UserRoles.Candidate)]
    public class CandidateController(IAuthService authService, ICandidateSkillService candidateSkillService, ISkillService skillService,
        ICandidateService candidateService, IPositionService positionService, ICVService cVService, IProjectTagService projectTagService,
        IProjectService projectService, IImageService imageService, IConfiguration configuration, AlertService alertService) : Controller
    {

        private Candidate? Candidate()
        {
            return candidateService.GetItemOrDefaultByOwnerId(authService.User().Id);
        }

        private IActionResult ValidationDecorator(Func<IActionResult> action, string reconnectActionName)
        {
            try
            {
                if (!authService.Validate()) throw new FailedAuthValidationException("Auth validation failed");
                if (Candidate() is null) throw new FailedAuthValidationException("Candidate not created");
                return action.Invoke();
            }
            catch (NotEqualItemVersionException ex)
            {
                alertService.RaiseAlert(ex.Message, AlertTypes.Danger);
                return RedirectToAction(reconnectActionName);
            }
            catch (FailedAuthValidationException ex)
            {
                alertService.RaiseAlert(ex.Message, AlertTypes.Danger);
                return RedirectToAction(nameof(Home));
            }
            catch (Exception ex)
            {
                alertService.RaiseAlert(ex.Message, AlertTypes.Warning);
                return RedirectToAction("Logout", "Auth");
            }
        }

        private async Task<IActionResult> ValidationDecoratorAsync(Func<Task<IActionResult>> action, string reconnectActionName)
        {
            try
            {
                if (!authService.Validate()) throw new FailedAuthValidationException("Auth validation failed");
                if (Candidate() is null) throw new Exception("Candidate not created");
                return await action.Invoke();
            }
            catch (NotEqualItemVersionException ex)
            {
                alertService.RaiseAlert(ex.Message, AlertTypes.Danger);
                return RedirectToAction(reconnectActionName);
            }
            catch (FailedAuthValidationException ex)
            {
                alertService.RaiseAlert(ex.Message, AlertTypes.Danger);
                return RedirectToAction("Logout", "Auth");
            }
            catch (Exception ex)
            {
                alertService.RaiseAlert(ex.Message, AlertTypes.Warning);
                return RedirectToAction(nameof(Home));
            }
        }

        [HttpGet]
        public IActionResult Home()
        {
            if (Candidate() is null)
            {
                var createCandidateDTO = new CreateCandidateProfilePageDTO
                {
                    ControllerForSubmit = ControllerContext.ActionDescriptor.ControllerName,
                    ActionForSubmit = "CreateProfile",
                };
                return View("Profile/CreateProfile", createCandidateDTO);
            }
            var homeDTO = new CandidateHomePageDTO { Candidate = Candidate() };
            return View("Profile/Home", homeDTO);
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
            return ValidationDecorator(() =>
            {
                var candidate = Candidate();
                var candidateSkills = candidateSkillService.GetCandidateSkillsByOwnerId(candidate.Id);
                var projects = projectService.GetByOwnerId(candidate.Id).ToList();
                var dto = new CandidateProfilePageDTO
                {
                    FirstName = candidate.FirstName,
                    LastName = candidate.LastName,
                    CandidateSkills = candidateSkills,
                    Projects = projects
                };
                return View("Profile/Profile", dto);
            }, nameof(Home));
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
        public IEnumerable<string> GetPopularSkillsNames(int count)
        {
            authService.Validate();
            var skills = skillService.GetPopularSkillsNames(count);
            return skills;
        }


        [HttpGet]
        public IEnumerable<string> GetPopularProjectTags(int limit)
        {
            authService.Validate();
            var projectTags = projectTagService.GetPopularTags((uint)limit).Select(x => x.Name).ToList();
            return projectTags;
        }

        [HttpGet]
        public IActionResult GetAddingSkillForm(string skillName, string eventsHandlers)
        {
            CandidateSkill CandidateSkill = new CandidateSkill { Skill = skillService.GetByName(skillName) };
            Dictionary<string, string> EventsHandlers = JsonSerializer.Deserialize<Dictionary<string, string>>(eventsHandlers);
            var dto = new AddCandidateSkillFormPageDTO
            {
                Skill = CandidateSkill.Skill,
                EventsHandlers = EventsHandlers
            };
            return PartialView("CandidateSkillsAndProjects/CandidateSkillAddPartialForm", dto);
        }

        [HttpGet]
        public IActionResult GetAddingProjectForm(int projectIndex, string eventsHandlers)
        {
            Dictionary<string, string> EventsHandlers = JsonSerializer.Deserialize<Dictionary<string, string>>(eventsHandlers);
            var dto = new AddProjectFormPageDTO
            {
                ProjectIndex = projectIndex,
                EventsHandlers = EventsHandlers,
                ActionForGetProjectTags = "GetProjectTagsNames",
                ControllerForGetProjectTags = ControllerContext.ActionDescriptor.ControllerName,
                ActionForGetPopularProjectTags = "GetPopularProjectTags",
                ControllerForGetPopularProjectTags = ControllerContext.ActionDescriptor.ControllerName
            };
            return PartialView("CandidateSkillsAndProjects/CandidateProjectAddPartialForm", dto);
        }

        [HttpPost]
        public async Task<string> UploadImage([FromForm] string oldImageName, [FromForm] IFormFile image)
        {
            if (oldImageName == configuration["ImageServices:EmptyImage"])
            {
                return await imageService.UploadImageAsync(image);
            }
            else return await imageService.ReplaceImageAsync(oldImageName, image) ?? configuration["ImageServices:EmptyImage"] ?? "";
        }

        [HttpGet]
        public IActionResult EditSkillsAndProjects()
        {
            return ValidationDecorator(() =>
            {
                var projects = projectService.GetByOwnerId(Candidate().Id);
                var candidateSkills = candidateSkillService.GetCandidateSkillsByOwnerId(Candidate().Id);
                var dto = new EditCandidateSkillsAndProjectsPageDTO
                {
                    CandidateSkills = candidateSkills,
                    Projects = projects,
                    ActionForGetSkillsNames = "GetSkillsNames",
                    ControllerForGetSkillsNames = ControllerContext.ActionDescriptor.ControllerName,
                    ActionForGetSkillForm = "GetAddingSkillForm",
                    ControllerForGetSkillForm = ControllerContext.ActionDescriptor.ControllerName,
                    ActionForGetProjectForm = "GetAddingProjectForm",
                    ControllerForGetProjectForm = ControllerContext.ActionDescriptor.ControllerName,
                    ActionForGetProjectTags = "GetProjectTagsNames",
                    ControllerForGetProjectTags = ControllerContext.ActionDescriptor.ControllerName,
                    ActionForUploadImage = "UploadImage",
                    ControllerForUploadImage = ControllerContext.ActionDescriptor.ControllerName,
                    ActionForGetPopularSkillNames = "GetPopularSkillsNames",
                    ControllerForGetPopularSkillNames = ControllerContext.ActionDescriptor.ControllerName,
                    ActionForGetPopularProjectTags = "GetPopularProjectTags",
                    ControllerForGetPopularProjectTags = ControllerContext.ActionDescriptor.ControllerName,
                    ActionToSubmit = "EditSkillsAndProjects",
                    ControllerToSubmit = ControllerContext.ActionDescriptor.ControllerName
                };
                return View("CandidateSkillsAndProjects/CandidateSkillsAndProjectsEditMainForm", dto);
            }, nameof(Home));
        }

        private IEnumerable<EditProjectDTO> GetEditDTOsFromJSON(string buffer)
        {
            IEnumerable<ProjectRecordDTO> projectRecordDTOs = JsonSerializer.Deserialize<IEnumerable<ProjectRecordDTO>>(buffer);
            IEnumerable<EditProjectDTO> editProjectDTOs = projectService.DeserializeEditDTOs(projectRecordDTOs);
            return editProjectDTOs;
        }

        private IEnumerable<AddProjectDTO> GetAddDTOsFromJSON(string buffer)
        {
            IEnumerable<ProjectRecordDTO> projectsRecordDTOs = JsonSerializer.Deserialize<IEnumerable<ProjectRecordDTO>>(buffer);
            IEnumerable<AddProjectDTO> addProjectDTOs = projectService.DeserializeToAddDTOs(projectsRecordDTOs);
            return addProjectDTOs;
        }

        private async Task TryAddNewItems(string skillsBuffer, string projectsBuffer)
        {
            IEnumerable<AddValuedSkillDTO> addedSkillDTOs = JsonSerializer.Deserialize<IEnumerable<AddValuedSkillDTO>>(skillsBuffer);
            IEnumerable<AddProjectDTO> addedProjectDTOs = GetAddDTOsFromJSON(projectsBuffer);
            await candidateSkillService.AddRangeAsync(addedSkillDTOs, Candidate().Id);
            projectService.AddRange(addedProjectDTOs, Candidate().Id);
        }

        private async Task TryUpdateItems(string skillsBuffer, string projectsBuffer)
        {
            IEnumerable<EditValuedSkillDTO> updatedSkillDTOs = JsonSerializer.Deserialize<IEnumerable<EditValuedSkillDTO>>(skillsBuffer);
            IEnumerable<EditProjectDTO> updatedProjectDTOs = GetEditDTOsFromJSON(projectsBuffer);
            await candidateSkillService.ChangeCurrentSkillsAsync(updatedSkillDTOs, Candidate().Id);
            projectService.ChangeCurrentProjects(updatedProjectDTOs, Candidate().Id);
        }

        [HttpPost]
        public async Task<IActionResult> EditSkillsAndProjects(EditCandidateSkillsAndProjectsDTO dto)
        {
            return await ValidationDecoratorAsync(async () =>
            {
                await TryUpdateItems(dto.BufferForUpdatingSkills, dto.BufferForUpdatingProjects);
                await TryAddNewItems(dto.BufferForAddedSkills, dto.BufferForAddedProjects);
                return RedirectToAction("Profile");
            }, nameof(EditSkillsAndProjects));
        }

        [HttpGet]
        public IActionResult ViewPositions()
        {
            return ValidationDecorator(() =>
            {
                var positions = positionService.GetPersonaledPositions(Candidate());
                var dto = new ViewReadOnlyPositionsPageDTO
                {
                    Positions = positions,
                    ActionForViewPosition = "ViewPosition",
                    ControllerForViewPosition = ControllerContext.ActionDescriptor.ControllerName
                };
                return View("Positions/PositionsReadOnlyView", dto);
            }, nameof(Home));
        }

        [HttpGet]
        public IActionResult ViewPosition(Guid positionId)
        {
            return ValidationDecorator(() =>
            {
                var position = positionService.GetById(positionId);
                var dto = new ViewReadOnlyPositionPageDTO
                {
                    Position = position,
                    ActionForGenerateCV = "GenerateCV",
                    ControllerForGenerateCV = ControllerContext.ActionDescriptor.ControllerName
                };
                return View("Positions/PositionReadOnlyView", dto);
            }, nameof(ViewPositions));
        }
        [HttpGet]
        public IActionResult GenerateCV(Guid positionId)
        {
            return ValidationDecorator(() =>
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
                    ValuedSkills = valuedSkills,
                    NotValuedSkills = notValuedSkills,
                    ActionForUploadImage = "UploadImage",
                    ControllerForUploadImage = ControllerContext.ActionDescriptor.ControllerName,
                    ActionForSubmit = "GenerateCV",
                    ControllerForSubmit = ControllerContext.ActionDescriptor.ControllerName
                };
                return View("CVs/CVAdd", dto);
            }, nameof(Home));
        }

        [HttpPost]
        public async Task<IActionResult> GenerateCV(AddCVDTO addCVDTO)
        {
            return await ValidationDecoratorAsync(async () =>
            {
                IEnumerable<AddValuedSkillDTO> newSkills = JsonSerializer.Deserialize<IEnumerable<AddValuedSkillDTO>>(addCVDTO.BufferForNotValuedSkills);
                IEnumerable<EditValuedSkillDTO> updatedSkillDTOs = JsonSerializer.Deserialize<IEnumerable<EditValuedSkillDTO>>(addCVDTO.BufferForValuedSkills);

                if (updatedSkillDTOs.Count() != 0) candidateSkillService.UpdateRange(updatedSkillDTOs);

                await candidateSkillService.AddRangeAsync(newSkills, Candidate().Id);
                cVService.AddCV(Candidate().Id, addCVDTO.PositionId);
                return RedirectToAction("ViewCVs");
            }, nameof(ViewPositions));
        }

        [HttpGet]
        public IActionResult ViewCVs()
        {
            return ValidationDecorator(() =>
            {
                IEnumerable<CV> cvs = cVService.GetByOwnerId(Candidate().Id);
                IEnumerable<Position> positions = positionService.GetByIds(cvs.Select(x => x.PositionId).Distinct());
                var dto = new ViewCVsCandidatePageDTO
                {
                    CVs = cvs,
                    Positions = positions,
                    ActionForViewCV = "ViewCV",
                    ControllerForViewCV = ControllerContext.ActionDescriptor.ControllerName
                };
                return View("CVs/CVsView", dto);
            }, nameof(Home));
        }

        [HttpGet]
        public IActionResult ViewCV(Guid cvId)
        {
            return ValidationDecorator(() =>
            {
                CV cv = cVService.GetById(cvId);
                IEnumerable<CandidateSkill> candidateSkills = candidateSkillService.GetCandidateSkillsByOwnerId(cv.CandidateId);
                IEnumerable<Project> projects =
                    projectService.GetPersonaledProjectsByTags(cv.CandidateId, cv.Position.ProjectTags, (uint)cv.Position.MaxCountOfProject);
                var dto = new ViewCVCandidatePageDTO
                {
                    CV = cv,
                    CandidateSkills = candidateSkills,
                    Projects = projects,
                    ActionForEditCV = "EditCV",
                    ControllerForEditCV = ControllerContext.ActionDescriptor.ControllerName
                };
                return View("CVs/CVView", dto);
            }, nameof(ViewCVs));
        }

        [HttpGet]
        public IActionResult EditCV(Guid cvId)
        {
            return ValidationDecorator(() =>
            {
                CV cv = cVService.GetById(cvId);
                IEnumerable<CandidateSkill> candidateSkills = candidateSkillService.GetCandidateSkillsByOwnerId(cv.CandidateId);
                IEnumerable<Project> projects =
                    projectService.GetPersonaledProjectsByTags(cv.CandidateId, cv.Position.ProjectTags, (uint)cv.Position.MaxCountOfProject);
                var dto = new EditCVPageDTO
                {
                    CV = cv,
                    CandidateSkills = candidateSkills,
                    Projects = projects,
                    ActionForGetProjectTags = "GetProjectTagsNames",
                    ControllerForGetProjectTags = ControllerContext.ActionDescriptor.ControllerName,
                    ActionForUploadImage = "UploadImage",
                    ControllerForUploadImage = ControllerContext.ActionDescriptor.ControllerName,
                    ActionForSubmit = ControllerContext.ActionDescriptor.ActionName,
                    ControllerForSubmit = ControllerContext.ActionDescriptor.ControllerName,
                    ActionForGetPopularProjectTags = "GetPopularProjectTags",
                    ControllerForGetPopularProjectTags = ControllerContext.ActionDescriptor.ControllerName
                };
                return View("CVs/CVEditMainForm", dto);
            }, nameof(ViewCVs));
        }

        [HttpPost]
        public async Task<IActionResult> EditCV(EditCVDTO dto)
        {
            return await ValidationDecoratorAsync(async () =>
            {
                await TryUpdateItems(dto.BufferForUpdatingSkills, dto.BufferForUpdatingProjects);
                return RedirectToAction("Profile");
            }, nameof(ViewCVs));
        }
    }
}
