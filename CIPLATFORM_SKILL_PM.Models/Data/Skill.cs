using System;
using System.Collections.Generic;

namespace CIPLATFORM_SKILL_PM.Models.Data
{
    public partial class Skill
    {
        public int SkillId { get; set; }
        public string? SkillName { get; set; }
        public bool? Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
