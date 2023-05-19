using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPLATFORM_SKILL_PM.Models.ViewModel
{
    public interface IAutitableEntityModel<T>
    {
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Boolean? Status { get; set; }
    }
}
