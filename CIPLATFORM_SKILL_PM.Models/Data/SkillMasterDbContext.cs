using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPLATFORM_SKILL_PM.Models.Data
{
    public partial class  SkillMasterDbContext:DbContext
    {
        public SkillMasterDbContext(DbContextOptions<SkillMasterDbContext> options)
           : base(options)
        {
        }
        public virtual DbSet<Skill> Skills { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
    }
}
