using CIPLATFORM_SKILL_PM.Models.ViewModel;
using System;
using System.Collections.Generic;

namespace CIPLATFORM_SKILL_PM.Models.Data
{
    public class Skill:AuditableEntityModel<Skill>
    {
        public int SkillId { get; set; }
        public string? SkillName { get; set; }
    }
}
