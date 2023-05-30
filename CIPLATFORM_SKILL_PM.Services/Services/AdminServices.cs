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
         int IAdminService.AddEdit(int id, string skillName, bool status)
        {
            var obj = _repository.GetFirstordefault(t => t.SkillName == skillName);
            if (obj!=null && obj.Status==status)
            {
                return 1;
            }
            if (id == 0 && obj==null && skillName != null)
            {
                _repository.Add(new Skill { SkillName = skillName, Status = status, CreatedAt = DateTime.Now });
                return 2;
            }
            if(id!=0 && skillName !=null)
            {
                Skill skill=_repository.GetFirstordefault(t=>t.SkillId== id);
                skill.Status = status; skill.SkillName = skillName;skill.UpdatedAt = DateTime.Now;

                _repository.Edit(skill);
                return 3;
            }
            if (skillName == null)
            {
                return -1;
            }
            return 0;
        }
    }
}
