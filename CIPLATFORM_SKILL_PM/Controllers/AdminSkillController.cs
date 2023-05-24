
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
        public AdminSkillController(IAdminService adminService, IMapper mapper, INotyfService notyf)
        {
            _adminService = adminService; _mapper = mapper; _notyf = notyf;
        }
        public IActionResult Admin_Skill_List()
        {
            string userSessionEmailId = HttpContext.Session.GetString("useremail");
            if (userSessionEmailId == null) { return RedirectToAction("Login", "Account"); }
            return View();
        }
        [HttpPost]
        public IActionResult AdminSkillAdd(string skillname, bool status)
        {
            if (ModelState.IsValid)
            {
                Skill skillexist = _adminService.GetFirstordefault(t => t.SkillName.Contains(skillname));
                if (skillexist == null)
                {
                    SkillModel skill = new SkillModel(); skill.Status = status; skill.SkillName = skillname; Skill skill1 = _mapper.Map<Skill>(skill); skill1.CreatedAt = DateTime.Now;
                    _adminService.Create(skill1);
                    _adminService.Save();
                    ModelState.Clear();
                    _notyf.Success("skill added successfully ", 3); return Ok(new { data = 1 });
                }
                else
                {
                    _notyf.Error("Skill Already Exists", 3); return BadRequest("Skill Already Exists");
                }
            }
            else
            {
                _notyf.Error("data is not valid", 3); return BadRequest("data is not valid");
            }
        }
        [HttpPost]
        public IActionResult GetSkillData(int? page, string searchText, int orderBy = 0)
        {
            int pageNumber = page ?? 1; int pageSize = 10;
            IPagedList<SkillModel?>? skillViewModels = _adminService.PageList(page, x => searchText != null ? (x.SkillName.ToLower().Contains(searchText.ToLower()) && x.DeletedAt == null) : (x.DeletedAt == null),
            x => new SkillModel { skillid = x.SkillId, SkillName = x.SkillName, Status = x.Status }, q => orderBy == 0 ? q.OrderBy(x => x.SkillName) : q.OrderByDescending(x => x.SkillName));
            return PartialView("_SkillTable", skillViewModels);
        }
        public IActionResult getSkillforedit(int id)
        {
            Skill skill = _adminService.GetFirstordefault(t => t.SkillId==id);
            return Json(skill);
        }
        [HttpPost]
        public IActionResult EditSkill(int id, string skillName, bool status)
        {
            if (ModelState.IsValid)
            {
                Skill skillexist = _adminService.GetFirstordefault(t => t.SkillName.Contains(skillName));
                if (skillexist == null)
                {
                    Skill skill = _adminService.GetFirstordefault(t=>t.SkillId==id); skill.SkillName = skillName; skill.Status = status; skill.UpdatedAt = DateTime.Now;
                    _adminService.Update(skill); _adminService.Save();
                    _notyf.Success("skill edited successfully", 3);
                    return Ok(new { data = 1 });
                }
                return BadRequest("Skill Already Exists");
            }
            else
            {
                return BadRequest("your data can not be Empty");
            }
        }
        [HttpPost]
        public void DeleteSkill(int id)
        {
            Skill skill = _adminService.GetFirstordefault(t => t.SkillId == id);
            _adminService.Delete(skill); _adminService.Save();
        }
        public IActionResult logout()
        {
            HttpContext.Session.Remove("useremail"); HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }

    }
}
