
using CIPLATFORM_SKILL_PM.Models.ViewModel;
using CIPLATFORM_SKILL_PM.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CIPLATFORM_SKILL_PM.Controllers
{
    public class AdminController : Controller
    {
        private readonly Services.Interface.IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService= adminService;

        }
        public IActionResult Admin_Skill_List()
        {
            return View();
        }
        [HttpPost]
        public void AdminSkillAdd(SkillModel skillModel)
        {
            if (ModelState.IsValid)
            {
                skill skill=new skill();
                _adminService.Create(skillModel);
            }
        }

    }
}
