using CIPLATFORM_SKILL_PM.Models.Data;
using CIPLATFORM_SKILL_PM.Repository.Interface;
using CIPLATFORM_SKILL_PM.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPLATFORM_SKILL_PM.Services.Services
{

    public class LoginService : EntityService<User>, ILoginService
    {
        ILoginRepository _loginRepository;

        public LoginService(ILoginRepository loginRepository)
            : base(loginRepository)
        {

            _loginRepository = loginRepository;
        }
        public User GetById(int Id)
        {
            return _loginRepository.GetById(Id);
        }
        public User ValidateEmail(string email)
        {
            return _loginRepository.GetByEmail(email);
        }
        public User ValidateUser(string email, string password)
        {
            User user= _loginRepository.GetByEmail(email);
            if (user == null && user.Password == password)
            {
                return user;
            }
            else
            {
                return null;
            }
        }
    }

}
