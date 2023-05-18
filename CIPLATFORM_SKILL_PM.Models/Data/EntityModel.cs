using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPLATFORM_SKILL_PM.Models.ViewModel
{

    public class EntityModel<T> : IEntityModel<T>
    {
        public DateTime? CreatedAt { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime? UpdatedAt { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime? DeletedAt { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        bool? IEntityModel<T>.Status { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}

