﻿using CIPLATFORM_SKILL_PM.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPLATFORM_SKILL_PM.Models.ViewModel
{
    public class SkillModel
    {
        [Required]
        public string? SkillName { get; set; }
        public bool? Status { get; set; }
        public IEnumerable<Skill>? skills { get; set; }
        public int? skillid { get; set; }
        public int TotalCount { get; set; }
        public string? userName { get; set; }
        public DateTime? createdAt { get; set; }
    }
}
