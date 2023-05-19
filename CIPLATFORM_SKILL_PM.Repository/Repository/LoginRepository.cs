using CIPLATFORM_SKILL_PM.Models.Data;
using CIPLATFORM_SKILL_PM.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPLATFORM_SKILL_PM.Repository.Repository
{
    public class LoginRepository : GenericRepository<User>, ILoginRepository
    {
        private readonly protected SkillMasterDbContext _entities;

        public LoginRepository(SkillMasterDbContext context)
              : base(context)
        {
            _entities = context;
        }
        public User GetById(int id)
        {
            return GetFirstordefault(x => x.UserId == id);
        }
        public User GetByEmail(string email)
        {
            return GetFirstordefault(x => x.Email == email);
        }

    }
}
