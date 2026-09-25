using System.Text.Json;
using Domain;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Database.Exceptions;
using ITransitionProject.PagesDTOs.Administrator.Positions;
using ITransitionProject.PagesDTOs.Candidate.CVs;
using ITransitionProject.PagesDTOs.Recruter.Positions;
using Microsoft.AspNetCore.Mvc;
using UseCases.Services;
using UseCases.Services.AuthServices.Interfaces;
using UseCases.Services.CVServices.Interfaces;
using UseCases.Services.Entities.CVServices.DTOs;
using UseCases.Services.Entities.PositionServices.DTOs;
using UseCases.Services.Entities.ValuedSkillServices.AccessRuleServices.Interfaces;
using UseCases.Services.Exceptions;
using UseCases.Services.ImageServices.Interfaces;
using UseCases.Services.PositionServices.Interfaces;
using UseCases.Services.ProjectServices.DTOs;
using UseCases.Services.ProjectServices.Interfaces;
using UseCases.Services.ProjectTagServices.Interfaces;
using UseCases.Services.SkillServices.Interfaces;
using UseCases.Services.ValuedSkillServices.CandidateSkillServices.Interfaces;
using UseCases.Services.ValuedSkillServices.General.DTOs;

namespace ITransitionProject.Controllers
{
    [EnumAuthorize(UserRoles.Administrator)]
    public class AdministratorController(ICVService cVService, IPositionService positionService, IProjectService projectService,
        ICandidateSkillService candidateSkillService, IConfiguration configuration, IImageService imageService, ISkillService skillService,
        IAuthService authService, IProjectTagService projectTagService, IAccessRuleService accessRuleService,
        AlertService alertService) : Controller
    {
        private IActionResult ValidationDecorator(Func<IActionResult> action, string reconnectActionName)
        {
            try
            {
                if (!authService.Validate()) throw new FailedAuthValidationException("Auth validation failed");
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
        public IActionResult Home()
        {
            return View("Profile/Home");
        }

        [HttpGet]
        public IActionResult ViewCVs()
        {
            return ValidationDecorator(() =>
            {
                IEnumerable<CV> cvs = cVService.GetAll();
                IEnumerable<Position> positions = positionService.GetByIds(cvs.Select(x => x.PositionId).Distinct());
                var dto = new ViewCVsCandidatePageDTO
                {
                    CVs = cvs,
                    Positions = positions,
                    ActionForViewCV = "ViewCV",
                    ControllerForViewCV = ControllerContext.ActionDescriptor.ControllerName
                };
                return View("../Candidate/CVs/CVsView", dto);
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
                    ActionForEditCV = nameof(EditCV),
                    ControllerForEditCV = ControllerContext.ActionDescriptor.ControllerName
                };
                return View("../Candidate/CVs/CVView", dto);
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
                    ActionForGetProjectTags = nameof(GetProjectTagsNames),
                    ControllerForGetProjectTags = ControllerContext.ActionDescriptor.ControllerName,
                    ActionForUploadImage = nameof(UploadImage),
                    ControllerForUploadImage = ControllerContext.ActionDescriptor.ControllerName,
                    ActionForGetPopularProjectTags = nameof(GetPopularProjectTags),
                    ControllerForGetPopularProjectTags = ControllerContext.ActionDescriptor.ControllerName,
                    ActionForSubmit = ControllerContext.ActionDescriptor.ActionName,
                    ControllerForSubmit = ControllerContext.ActionDescriptor.ControllerName
                };

                return View("../Candidate/CVs/CVEditMainForm", dto);
            }, nameof(ViewCVs));
        }

        [HttpGet]
        public IEnumerable<string> GetSkillsNames(string prefix)
        {
            var skills = skillService.GetAllSkillsNamesByPrefix(prefix);
            return skills;
        }

        [HttpGet]
        public IEnumerable<string> GetPopularSkillsNames(int count)
        {
            var skills = skillService.GetPopularSkillsNames(count);
            return skills;
        }

        [HttpGet]
        public IEnumerable<string> GetProjectTags(string prefix)
        {
            var tags = projectTagService.GetNamesByPrefix(prefix);
            return tags;
        }

        [HttpGet]
        public IEnumerable<string> GetProjectTagsNames(string? prefix)
        {
            var i = HttpContext.Request.Query["prefix"];
            var projectTags = projectTagService.GetNamesByPrefix(i);
            return projectTags;
        }

        [HttpGet]
        public IEnumerable<string> GetPopularProjectTags(int limit)
        {
            var projectTags = projectTagService.GetPopularTags((uint)limit).Select(x => x.Name).ToList();
            return projectTags;
        }

        [HttpGet]
        public string GetSkillTypeBySkillName(string skillName)
        {
            return skillService.GetSkillTypeBySkillName(skillName).ToString();
        }

        [HttpGet]
        public string GetSkillIdBySkillName(string skillName)
        {
            return skillService.GetIdBySkillName(skillName).ToString();
        }

        [HttpGet]
        public IActionResult GetPositionSkillPartialForm(string skillName)
        {
            var skill = skillService.GetByName(skillName);
            var dto = new AddPositionSkillPagePartialFormDTO
            {
                Skill = skill
            };
            return PartialView("../Recruter/Positions/PositionSkillAddPartialForm", dto);
        }

        [HttpGet]
        public IActionResult GetAddingAccessRuleForm(string skillName)
        {
            var dto = new AddPositionAccessRulePagePartialFormDTO
            {
                Skill = skillService.GetByName(skillName)
            };
            return PartialView("../Recruter/Positions/PositionAccessRuleAddPartialForm", dto);
        }

        [HttpGet]
        public IActionResult GetEditingAccessRuleForm(string accessRuleId, string eventsHandlers)
        {
            var deserializedEventsHandlers = JsonSerializer.Deserialize<Dictionary<string, string>>(eventsHandlers);
            var dto = new EditPositionAccessRulePagePartialFormDTO
            {
                AccessRule = accessRuleService.GetById(Guid.Parse(accessRuleId)),
                EventHandlers = deserializedEventsHandlers
            };
            return PartialView("../Recruter/Positions/PositionAccessRuleEditPartialForm", dto);
        }

        [HttpPost]
        public async Task<string> UploadImage([FromForm] string oldImageName, [FromForm] IFormFile image)
        {
            if (oldImageName == configuration["ImageServices:EmptyImage"])
            {
                return await imageService.UploadImageAsync(image);
            }
            else return await imageService.ReplaceImageAsync(oldImageName, image);
        }

        private IEnumerable<EditProjectDTO> GetEditDTOsFromJSON(string buffer)
        {
            IEnumerable<ProjectRecordDTO> projectRecordDTOs = JsonSerializer.Deserialize<IEnumerable<ProjectRecordDTO>>(buffer);
            IEnumerable<EditProjectDTO> editProjectDTOs = projectService.DeserializeEditDTOs(projectRecordDTOs);
            return editProjectDTOs;
        }

        private async Task TryUpdateItems(Guid ownerId, string skillsBuffer, string projectsBuffer)
        {
            IEnumerable<EditValuedSkillDTO> updatedSkillDTOs = JsonSerializer.Deserialize<IEnumerable<EditValuedSkillDTO>>(skillsBuffer);
            IEnumerable<EditProjectDTO> updatedProjectDTOs = GetEditDTOsFromJSON(projectsBuffer);
            await candidateSkillService.ChangeCurrentSkillsAsync(updatedSkillDTOs, ownerId);
            projectService.ChangeCurrentProjects(updatedProjectDTOs, ownerId);
        }

        [HttpPost]
        public async Task<IActionResult> EditCV(EditCVDTO dto)
        {
            return await ValidationDecoratorAsync(async () =>
            {
                await TryUpdateItems(dto.OwnerId, dto.BufferForUpdatingSkills, dto.BufferForUpdatingProjects);
                return RedirectToAction("ViewCVs");
            }, nameof(ViewCVs));
        }

        [HttpGet]
        public IActionResult ViewPositions()
        {
            return ValidationDecorator(() =>
            {
                var positons = positionService.GetAll();
                var dto = new ViewPositionsAdministratorPageDTO
                {
                    Positions = positons,
                    ActionForDeletePositions = nameof(DeletePositions),
                    ControllerForDeletePositions = ControllerContext.ActionDescriptor.ControllerName,
                    ActionForViewPosition = nameof(ViewPosition),
                    ControllerForViewPosition = ControllerContext.ActionDescriptor.ControllerName
                };
                return View("Positions/PositionsView", dto);
            }, nameof(Home));
        }
        [HttpGet]
        public IActionResult ViewPosition(Guid positionId)
        {
            return ValidationDecorator(() =>
            {
                var position = positionService.GetById(positionId);
                var dto = new ViewPositionPageDTO
                {
                    Position = position,
                    ActionForEditPosition = nameof(EditPosition),
                    ControllerForEditPosition = ControllerContext.ActionDescriptor.ControllerName,
                };
                return View("../Recruter/Positions/PositionView", dto);
            }, nameof(ViewPositions));
        }
        [HttpGet]
        public IActionResult DeletePositions(IEnumerable<Guid> positions)
        {
            return ValidationDecorator(() =>
            {
                positionService.DeleteRange(positions);
                return RedirectToAction("ViewPositions");
            }, nameof(ViewPositions));
        }

        [HttpGet]
        public IActionResult EditPosition(Guid positionId)
        {
            return ValidationDecorator(() =>
            {
                var position = positionService.GetById(positionId);
                var dto = new EditPositionPageDTO
                {
                    Position = position,
                    ActionForGetSkillsNames = nameof(GetSkillsNames),
                    ControllerForGetSkillsNames = ControllerContext.ActionDescriptor.ControllerName,
                    ActionForGetSkillForm = "GetPositionSkillPartialForm",
                    ControllerForGetSkillForm = ControllerContext.ActionDescriptor.ControllerName,
                    ActionForGetAddingAccessRuleForm = "GetAddingAccessRuleForm",
                    ControllerForGetAddingAccessRuleForm = ControllerContext.ActionDescriptor.ControllerName,
                    ActionForGetProjectTags = "GetProjectTags",
                    ControllerForGetProjectTags = ControllerContext.ActionDescriptor.ControllerName,
                    ActionForGetSkillTypeBySkillName = "GetSkillTypeBySkillName",
                    ControllerForGetSkillTypeBySkillName = ControllerContext.ActionDescriptor.ControllerName,
                    ActionForGetSkillIdBySkillName = "GetSkillIdBySkillName",
                    ControllerForGetSkillIdBySkillName = ControllerContext.ActionDescriptor.ControllerName,
                    ActionForGetPopularSkillNames = "GetPopularSkillsNames",
                    ControllerForGetPopularSkillNames = ControllerContext.ActionDescriptor.ControllerName,
                    ActionForGetEditingAccessRuleForm = "GetEditingAccessRuleForm",
                    ActionForGetPopularProjectTags = "GetPopularProjectTags",
                    ControllerForGetPopularProjectTags = ControllerContext.ActionDescriptor.ControllerName,
                    ControllerForGetEditingAccessRuleForm = ControllerContext.ActionDescriptor.ControllerName,
                    ActionForSubmit = ControllerContext.ActionDescriptor.ActionName,
                    ControllerForSubmit = ControllerContext.ActionDescriptor.ControllerName
                };
                return View("../Recruter/Positions/PositionEditMainForm", dto);
            }, nameof(ViewPositions));
        }

        [HttpPost]
        public IActionResult EditPosition(EditPositionDTO dto)
        {
            return ValidationDecorator(() =>
            {
                positionService.UpdatePosition(dto);
                return RedirectToAction("ViewPosition", new { positionId = dto.Id });
            }, nameof(ViewPositions));
        }
    }
}
