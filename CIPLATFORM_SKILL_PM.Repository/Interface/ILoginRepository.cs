using CIPLATFORM_SKILL_PM.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPLATFORM_SKILL_PM.Repository.Interface
{
    public interface ILoginRepository : IRepository<User>
    {
        User GetById(int id);
        User GetByEmail(string email);
    }
}
