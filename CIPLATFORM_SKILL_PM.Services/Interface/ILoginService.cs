using CIPLATFORM_SKILL_PM.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPLATFORM_SKILL_PM.Services.Interface
{
    public interface ILoginService : IEntityService<User>
    {
        User GetById(int Id);
        User validateEmail(string email);
    }
}
