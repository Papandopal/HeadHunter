using System.Text.Json;
using AspNetCoreGeneratedDocument;
using Domain;
using Domain.Entities;
using Domain.Enums;
using ITransitionProject.PagesDTOs.Recruter;
using ITransitionProject.PagesDTOs.Recruter.AccessRules;
using ITransitionProject.PagesDTOs.Recruter.Categories;
using ITransitionProject.PagesDTOs.Recruter.Positions;
using ITransitionProject.PagesDTOs.Recruter.Profile;
using ITransitionProject.PagesDTOs.Recruter.Skills;
using Microsoft.AspNetCore.Mvc;
using UseCases.Services;
using UseCases.Services.AuthServices.Interfaces;
using UseCases.Services.CategoryServices.DTOs;
using UseCases.Services.CategoryServices.Interfaces;
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
        IRecruterService recruterService, AlertService alertService, IPositionService positionService) : Controller
    {

        public Recruter? Recruter()
        {
            return recruterService.GetItemOrDefaultByOwnerId(authService.User().Id);
        }

        [HttpGet]
        public IActionResult Home()
        {
            authService.Validate();
            if (recruterService.GetItemOrDefaultByOwnerId(authService.User().Id) is null)
            {
                var dto = new CreateRecruterProfilePageDTO
                {
                    ControllerForSubmit = ControllerContext.ActionDescriptor.ControllerName,
                    ActionForSubmit = "CreateProfile",
                };
                return View("Profile/CreateProfile", dto);
            }
            return View("/Profile/Home");
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
            var skills = skillService.GetAll();
            var dto = new RecruterProfileDTO
            {
                Name = recruterService.GetItemOrDefaultByOwnerId(authService.User().Id).Name,
                Skills = skills,
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
                ActionForGetAccessRuleForm = "GetAccessRuleForm",
                ControllerForGetAccessRuleForm = ControllerContext.ActionDescriptor.ControllerName,
                ActionForGetProjectTags = "GetProjectTags",
                ControllerForGetProjectTags = ControllerContext.ActionDescriptor.ControllerName,
                ActionForGetSkillTypeBySkillName = "GetSkillTypeBySkillName",
                ControllerForGetSkillTypeBySkillName = ControllerContext.ActionDescriptor.ControllerName,
                ActionForGetSkillIdBySkillName = "GetSkillIdBySkillName",
                ControllerForGetSkillIdBySkillName = ControllerContext.ActionDescriptor.ControllerName,
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
            var dto = new AddPositionPagePartialFormDTO
            {
                Skill = skill
            };
            return PartialView("Positions/PositionSkillAddPartialForm", dto);
        }

        [HttpGet]
        public IActionResult GetAccessRuleForm(string skillName)
        {
            var dto = new AddAccessRulePageDTO
            {
                Skill = skillService.GetByName(skillName)
            };
            return PartialView("AccessRules/AccessRuleAddPartialForm", dto);
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
                ControllerForDeletePositions = ControllerContext.ActionDescriptor.ControllerName
            };
            return View("Positions/PositionsView", dto);
        }
        [HttpGet]
        public IActionResult ViewPosition(Guid id)
        {
            var position = positionService.GetById(id);
            var dto = new ViewPositionPageDTO { Position = position };
            return View("Positions/PositionView", dto);
        }
        [HttpGet]
        public IActionResult DeletePositions(IEnumerable<Guid> positions)
        {
            positionService.DeleteRange(positions);
            return RedirectToAction("ViewPositions");
        }
    }
}
