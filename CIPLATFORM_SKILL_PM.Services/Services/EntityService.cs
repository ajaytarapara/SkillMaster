using CIPLATFORM_SKILL_PM.Models.ViewModel;
using CIPLATFORM_SKILL_PM.Repository.Interface;
using CIPLATFORM_SKILL_PM.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPLATFORM_SKILL_PM.Services.Services
{
    public abstract class EntityService<T> : IEntityService<T> where T : BaseEntity
    {

        IRepository<T> _repository;

        public EntityService(IRepository<T> repository)
        {
            _repository = repository;
        }


        public virtual void Create(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _repository.Add(entity);
        }


        public virtual void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _repository.Edit(entity);
        }

        public virtual void Delete(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _repository.Delete(entity);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _repository.GetAll();
        }
    }
}
