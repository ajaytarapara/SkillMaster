using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPLATFORM_SKILL_PM.Models.ViewModel
{

    public class Entity<T> : IEntity<T>
    {
        public T Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
