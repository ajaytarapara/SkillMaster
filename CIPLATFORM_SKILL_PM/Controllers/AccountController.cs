using Microsoft.AspNetCore.Mvc;

using System.Collections;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using CIPLATFORM_SKILL_PM.Models.Data;
using CIPLATFORM_SKILL_PM.Services.Interface;
using CIPLATFORM_SKILL_PM.Models.ViewModel;
using System.Configuration;
using CIPLATFORM_SKILL_PM.Models.Auth;
using CIPLATFORM_SKILL_PM.Auth;
using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
namespace CIPlatform.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILoginService _loginservice;
        private readonly IConfiguration configuration;
        private readonly INotyfService _notyf;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        public AccountController(ILoginService loginService, IConfiguration _configuration, INotyfService notyf, IHttpContextAccessor httpContextAccessor,IMapper mapper)
        {
            _loginservice = loginService;configuration = _configuration; _notyf = notyf;  _httpContextAccessor = httpContextAccessor;_mapper = mapper;
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult PrivacyPolicy()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginModel obj)
        {
            User isValidEmail = _loginservice.ValidateEmail(obj.EmailId);
            if (isValidEmail == null)
            {
                ModelState.AddModelError("EmailId", "Email not found");
            }
            else
            {
                User isValidUser = _loginservice.ValidateUser(obj.EmailId, obj.Password);
                if (isValidUser == null)
                {
                    ModelState.AddModelError("Password", "Password does not match");
                    return View();
                }
                SessionDetailsViewModel session = _mapper.Map<SessionDetailsViewModel>(isValidUser);
                var jwtSetting = configuration.GetSection(nameof(JwtSetting)).Get<JwtSetting>();
                var token = JwtTokenHelper.GenerateToken(jwtSetting, session);
                if (string.IsNullOrWhiteSpace(token))
                {
                    ModelState.AddModelError("email", "Pls Enter correct email");
                    return View("Login");
                }
                HttpContext.Session.SetString("Token", token);
                HttpContext.Session.SetString("useremail", obj.EmailId);
                if (isValidUser.Role.ToLower() == "admin")
                {
                    HttpContext.Session.SetString("useremail", isValidUser.Email);
                    _notyf.Success("Login Success Fully admin", 3);
                    return RedirectToAction("Admin_Skill_List", "AdminSkill");
                }
                if (isValidUser.Role.ToLower() == "volunteer" || isValidUser.Role==null)
                {
                    HttpContext.Session.SetString("useremail", isValidUser.Email);
                    _notyf.Success("Login Success Fully vol", 3);
                }
            }    
            return View();
        }

    }

}
