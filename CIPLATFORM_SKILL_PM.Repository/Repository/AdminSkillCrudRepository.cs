using CIPLATFORM_SKILL_PM.Models.Data;
using CIPLATFORM_SKILL_PM.Models.ViewModel;
using CIPLATFORM_SKILL_PM.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPLATFORM_SKILL_PM.Repository.Repository
{
    public class AdminSkillCrudRepository:GenericRepository<Skill>,IAdminSkillCrud
    {
        private readonly protected SkillMasterDbContext _entities;
        public AdminSkillCrudRepository(SkillMasterDbContext context)
            : base(context)
        {
            _entities = context;
        }
        
    }
}
