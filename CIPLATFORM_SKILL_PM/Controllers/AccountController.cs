using Microsoft.AspNetCore.Mvc;

using System.Collections;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using CIPLATFORM_SKILL_PM.Models.Data;
using CIPLATFORM_SKILL_PM.Services.Interface;
using CIPLATFORM_SKILL_PM.Models.ViewModel;

namespace CIPlatform.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILoginService _userRepository;

        public AccountController(ILoginService loginService)
        {
            _userRepository = loginService;
        }
        public IActionResult Login()
        {
           
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginModel obj)
        {
            string emailId = obj.EmailId;
            string password = obj.Password;
            var isValidEmail = _userRepository.validateEmail(emailId);
            return View();
        }

    }
}