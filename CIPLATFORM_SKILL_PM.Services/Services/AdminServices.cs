using CIPLATFORM_SKILL_PM.Models.Data;
using CIPLATFORM_SKILL_PM.Models.ViewModel;
using CIPLATFORM_SKILL_PM.Repository.Interface;
using CIPLATFORM_SKILL_PM.Repository.Repository;
using CIPLATFORM_SKILL_PM.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPLATFORM_SKILL_PM.Services.Services
{
    public class AdminServices:EntityService<Skill>,IAdminService
    {
       private readonly IGenericRepository<Skill> _repository;
        public AdminServices(AdminSkillCrudRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public Skill GetSkillById(int id)
        {
          return _repository.GetFirstordefault(T=>T.SkillId==id);
        }
    }
}
