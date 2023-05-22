
using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using CIPLATFORM_SKILL_PM.Models.Data;
using CIPLATFORM_SKILL_PM.Models.ViewModel;
using CIPLATFORM_SKILL_PM.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using PagedList;

namespace CIPLATFORM_SKILL_PM.Controllers
{
    public class AdminController : Controller
    {
        private readonly Services.Interface.IAdminService _adminService;
        private readonly IMapper _mapper;
        private readonly INotyfService _notyf;
        public AdminController(IAdminService adminService, IMapper mapper, INotyfService notyf)
        {
            _adminService = adminService;
            _mapper = mapper;
            _notyf = notyf;
        }
        public IActionResult Admin_Skill_List()
        {

            return View();
        }
        [HttpPost]
        public void AdminSkillAdd(string skillname, bool status)
        {
            if (ModelState.IsValid)
            {
                SkillModel skill = new SkillModel();
                skill.Status = status;
                skill.SkillName = skillname;
                Skill skill1 = _mapper.Map<Skill>(skill);
                skill1.CreatedAt = DateTime.Now;
                _adminService.Create(skill1);
                _adminService.Save();
            }
        }
        public IActionResult GetSkillData(int? page, string searchSkill = "a")
        {
            int pageNumber = page ?? 1;
            int pageSize = 10;

            IEnumerable<Skill?> skills = _adminService.GetAll().ToList();
            var pagedItems = skills.ToPagedList(pageNumber, pageSize);


            return PartialView("_SkillTable", pagedItems);
        }
        public IActionResult getSkillforedit(int id)
        {
            Skill skill = _adminService.GetSkillById(id);
            return Json(skill);
        }
        [HttpPost]
        public void EditSkill(int id,string skillName,bool status)
        {
            Skill skill = _adminService.GetSkillById(id);
            skill.SkillName = skillName;
            skill.Status = status;
            skill.UpdatedAt = DateTime.Now;
            _adminService.Update(skill);
            _adminService.Save();
        }
        [HttpPost]
        public void DeleteSkill(int id)
        {
            Skill skill = _adminService.GetSkillById(id);
            _adminService.Delete(skill);
            _adminService.Save();
        }
    }
}
