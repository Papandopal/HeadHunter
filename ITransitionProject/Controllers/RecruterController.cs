using System.Text.Json;
using Domain;
using Domain.Entities;
using Domain.Enums;
using ITransitionProject.PagesDTOs.Recruter.Categories;
using ITransitionProject.PagesDTOs.Recruter.Positions;
using ITransitionProject.PagesDTOs.Recruter.Profile;
using ITransitionProject.PagesDTOs.Recruter.Skills;
using Microsoft.AspNetCore.Mvc;
using UseCases.Services;
using UseCases.Services.AccessRuleServices.Interfaces;
using UseCases.Services.AuthServices.Interfaces;
using UseCases.Services.CategoryServices.DTOs;
using UseCases.Services.CategoryServices.Interfaces;
using UseCases.Services.Entities.PositionServices.DTOs;
using UseCases.Services.PositionServices.DTOs;
using UseCases.Services.PositionServices.Interfaces;
using UseCases.Services.RecruterServices.DTOs;
using UseCases.Services.RecruterServices.Interfaces;
using UseCases.Services.SkillServices.DTOs;
using UseCases.Services.SkillServices.Interfaces;

namespace ITransitionProject.Controllers
{
    [EnumAuthorize(UserRoles.Recruter)]
    public class RecruterController(ICategoryService categoryService, ISkillService skillService, IAuthService authService,
        IRecruterService recruterService, AlertService alertService, IPositionService positionService,
        IAccessRuleService accessRuleService) : Controller
    {

        private Recruter? Recruter()
        {
            return recruterService.GetItemOrDefaultByOwnerId(authService.User().Id);
        }

        [HttpGet]
        public IActionResult Home()
        {
            authService.Validate();
            if (recruterService.GetItemOrDefaultByOwnerId(authService.User().Id) is null)
            {
                var createRecruterDTO = new CreateRecruterProfilePageDTO
                {
                    ControllerForSubmit = ControllerContext.ActionDescriptor.ControllerName,
                    ActionForSubmit = "CreateProfile",
                };
                return View("Profile/CreateProfile", createRecruterDTO);
            }

            var positions = positionService.GetAll();
            var homeDTO = new RecruterHomePageDTO
            {
                Recruter = Recruter(),
                Positions = positions,
                ActionForDeletePositions = "DeletePositions",
                ControllerForDeletePositions = ControllerContext.ActionDescriptor.ControllerName,
                ActionForViewPosition = "ViewPosition",
                ControllerForViewPosition = ControllerContext.ActionDescriptor.ControllerName
            };
            return View("Profile/Home", homeDTO);
        }

        [HttpPost]
        public IActionResult CreateProfile(CreateRecruterProfileDTO dto)
        {
            var newRecruter = new Recruter
            {
                OwnerId = authService.User().Id,
                Name = dto.Name,
            };
            recruterService.Add(newRecruter);
            return RedirectToAction("Profile");
        }

        [HttpGet]
        public IActionResult Profile()
        {
            var dto = new RecruterProfilePageDTO
            {
                Recruter = Recruter()
            };
            return View("Profile/Profile", dto);
        }

        [HttpGet]
        public IActionResult AddSkill()
        {
            var dto = new AddSkillPageDTO
            {
                ActionToSubmit = ControllerContext.ActionDescriptor.ActionName,
                ControllerToSubmit = ControllerContext.ActionDescriptor.ControllerName,
                Categories = categoryService.GetAll(),
                CountOfRequiredProperties = 4
            };
            return View("Skills/SkillAdd", dto);
        }

        [HttpPost]
        public async Task<IActionResult> AddSkill(AddSkillDTO addSkillDTO)
        {
            var category = await categoryService.GetByNameAsync(addSkillDTO.CategoryName);

            var newSkill = new Skill
            {
                Category = category,
                Name = addSkillDTO.Name,
                PotentialValue = addSkillDTO.PotencialValue,
                Type = Enum.Parse<SkillTypes>(addSkillDTO.TypeName),
            };

            await skillService.AddAsync(newSkill);
            return RedirectToAction("AddSkill");
        }

        [HttpGet]

        public IActionResult EditSkill()
        {
            var dto = new EditSkillsPageDTO
            {
                Skills = skillService.GetAll(),
                Categories = categoryService.GetAll(),
                ActionToSubmit = ControllerContext.ActionDescriptor.ActionName,
                ControllerToSubmit = ControllerContext.ActionDescriptor.ControllerName
            };
            return View("Skills/SkillsEdit", dto);
        }

        [HttpPost]
        public IActionResult EditSkill(string json)
        {
            List<UpdateSkillDTO> dtos = JsonSerializer.Deserialize<List<UpdateSkillDTO>>(json);
            skillService.UpdateByDTOs(dtos);
            return RedirectToAction("Profile");
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            var dto = new AddCategoryPageDTO
            {
                ActionForSubmit = ControllerContext.ActionDescriptor.ActionName,
                ControllerForSubmit = ControllerContext.ActionDescriptor.ControllerName
            };

            return View("Categories/CategoryAdd", dto);
        }

        [HttpPost]
        public IActionResult AddCategory(AddCategoryDTO dto)
        {
            var newCategory = new Category { Name = dto.Name };
            categoryService.Add(newCategory);
            return RedirectToAction("AddCategory");
        }

        [HttpGet]
        public IActionResult AddPosition()
        {
            var dto = new AddPositionPageMainFormDTO
            {
                ActionForGetSkillsNames = "GetSkillsNames",
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
                ActionForSubmit = ControllerContext.ActionDescriptor.ActionName,
                ControllerForSubmit = ControllerContext.ActionDescriptor.ControllerName
            };
            return View("Positions/PositionAddMainForm", dto);
        }

        [HttpGet]
        public IEnumerable<string> GetSkillsNames(string prefix)
        {
            authService.Validate();
            var skills = skillService.GetAllSkillsNamesByPrefix(prefix);
            return skills;
        }

        [HttpGet]
        public IEnumerable<string> GetPopularSkillsNames(int count)
        {
            authService.Validate();
            var skills = skillService.GetPopularSkillsNames(count);
            return skills;
        }

        [HttpGet]
        public IEnumerable<string> GetProjectTags(string prefix)
        {
            authService.Validate();
            var tags = new List<string> { "aboba1", "aboba2" };
            return tags;
        }

        [HttpGet]
        public string GetSkillTypeBySkillName(string skillName)
        {
            authService.Validate();
            return skillService.GetSkillTypeBySkillName(skillName).ToString();
        }

        [HttpGet]
        public string GetSkillIdBySkillName(string skillName)
        {
            authService.Validate();
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
            return PartialView("Positions/PositionSkillAddPartialForm", dto);
        }

        [HttpGet]
        public IActionResult GetAddingAccessRuleForm(string skillName)
        {
            var dto = new AddPositionAccessRulePagePartialFormDTO
            {
                Skill = skillService.GetByName(skillName)
            };
            return PartialView("Positions/PositionAccessRuleAddPartialForm", dto);
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
            return PartialView("Positions/PositionAccessRuleEditPartialForm", dto);
        }

        [HttpPost]
        public IActionResult AddPosition(AddPositionDTO addPositionDTO)
        {
            positionService.AddPosition(addPositionDTO, Recruter().Id);
            return RedirectToAction("Profile");
        }

        [HttpGet]
        public IActionResult ViewPositions()
        {
            var positons = positionService.GetAll();
            var dto = new ViewPositionsPageDTO
            {
                Positions = positons,
                ActionForDeletePositions = "DeletePositions",
                ControllerForDeletePositions = ControllerContext.ActionDescriptor.ControllerName,
                ActionForViewPosition = "ViewPosition",
                ControllerForViewPosition = ControllerContext.ActionDescriptor.ControllerName
            };
            return View("Positions/PositionsView", dto);
        }
        [HttpGet]
        public IActionResult ViewPosition(Guid positionId)
        {
            var position = positionService.GetById(positionId);
            var dto = new ViewPositionPageDTO
            {
                Position = position,
                ActionForEditPosition = "EditPosition",
                ControllerForEditPosition = ControllerContext.ActionDescriptor.ControllerName,
            };
            return View("Positions/PositionView", dto);
        }
        [HttpGet]
        public IActionResult DeletePositions(IEnumerable<Guid> positions)
        {
            positionService.DeleteRange(positions);
            return RedirectToAction("ViewPositions");
        }

        [HttpGet]
        public IActionResult EditPosition(Guid positionId)
        {
            var position = positionService.GetById(positionId);
            var dto = new EditPositionPageDTO
            {
                Position = position,
                ActionForGetSkillsNames = "GetSkillsNames",
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
                ControllerForGetEditingAccessRuleForm = ControllerContext.ActionDescriptor.ControllerName,
                ActionForSubmit = ControllerContext.ActionDescriptor.ActionName,
                ControllerForSubmit = ControllerContext.ActionDescriptor.ControllerName
            };
            return View("Positions/PositionEditMainForm", dto);
        }

        [HttpPost]
        public IActionResult EditPosition(EditPositionDTO dto)
        {
            positionService.UpdatePosition(dto);
            return RedirectToAction("ViewPosition", new { positionId = dto.Id });
        }
    }
}
