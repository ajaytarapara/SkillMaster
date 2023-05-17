using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPLATFORM_SKILL_PM.Models.ViewModel
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}
