
using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using CIPLATFORM_SKILL_PM.Models.Data;
using CIPLATFORM_SKILL_PM.Models.ViewModel;
using CIPLATFORM_SKILL_PM.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PagedList;
namespace CIPLATFORM_SKILL_PM.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminSkillController : Controller
    {
        private readonly Services.Interface.IAdminService _adminService;
        private readonly IMapper _mapper;
        private readonly INotyfService _notyf;
        private readonly ILoginService _loginService;
        public AdminSkillController(IAdminService adminService, IMapper mapper, INotyfService notyf,ILoginService loginService)
        {
            _adminService = adminService; _mapper = mapper; _notyf = notyf;_loginService=loginService; 
        }
        public IActionResult Admin_Skill_List()
        {
            string userSessionEmailId = HttpContext.Session.GetString("useremail");
            if (userSessionEmailId == null) { return RedirectToAction("Login", "Account"); }
            User user = _loginService.ValidateEmail(userSessionEmailId);
            SkillModel skillModel = new SkillModel();
            skillModel.userName=user.FirstName+" "+user.LastName;
            return View(skillModel);
        }
        //================================
        //Skill Add Edit
        //================================
        [HttpPost]
        public void AddEditSkill(int id, string skillName, bool status)
        {
            var response = _adminService.AddEdit(id, skillName, status);
            _adminService.Save();
            if (response == 1)
            {
                _notyf.Error("skill already Exists", 3);
            }
            if (response == 2)
            {
                _notyf.Success("skill added successfully", 3);
            }
            if (response == 3)
            {
                _notyf.Success("skill edited successfully", 3);
            }
            if (response == -1)
            {
                _notyf.Error("your skill name must be not null", 3);
            }
        }
        //================================
        //Skill data get with pagination
        //================================
        [HttpPost]
        public IActionResult GetSkillData(int? page, string searchText, int orderBy,int? size)
        {
            int pageNumber = page ?? 1; int pageSize = size ?? 10;
            IPagedList<SkillModel?>? skillViewModels = _adminService.PageList(page,size, x => searchText != null ? (x.SkillName.ToLower().Contains(searchText.ToLower()) && 
            x.DeletedAt == null) : (x.DeletedAt == null),
            x => new SkillModel { skillid = x.SkillId, SkillName = x.SkillName, Status = x.Status }, q => orderBy == 0 ? q.OrderBy(x => x.SkillName) : q.OrderByDescending
            (x => x.SkillName));
            return PartialView("_SkillTable", skillViewModels);
        }
        //====================================
        //Skill delete
        //====================================
        [HttpPost]
        public void DeleteSkill(int id)
        {
            _adminService.Delete(id);
            _adminService.Save();
        }
        //======================================
        //logout admin
        //=====================================
        public IActionResult logout()
        {
            HttpContext.Session.Remove("useremail"); HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
        public IActionResult getSkillforedit(int id)
        {
            Skill skill = _adminService.GetFirstordefault(t => t.SkillId == id);
            return Json(skill);
        }
    }
}
