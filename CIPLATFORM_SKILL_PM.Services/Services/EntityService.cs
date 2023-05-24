using CIPLATFORM_SKILL_PM.Models.ViewModel;
using CIPLATFORM_SKILL_PM.Repository.Interface;
using CIPLATFORM_SKILL_PM.Repository.Repository;
using CIPLATFORM_SKILL_PM.Services.Interface;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CIPLATFORM_SKILL_PM.Services.Services
{
    public class EntityService<T> : IEntityService<T> where T : class
    {
        IGenericRepository<T> _repository;

        public EntityService(IGenericRepository<T> repository)
        {
            _repository = repository;
        }
        public void Create(T entity)
        {
            if (entity == null)
            {
                throw new Exception("entity");
            }
            _repository.Add(entity);
        }
        public T GetFirstordefault(Expression<Func<T, bool>> filter)
        {
            return _repository.GetFirstordefault(filter);
        }

        public void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _repository.Edit(entity);
        }

        public void Delete(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _repository.Delete(entity);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter,string searchText)
        {
                return _repository.GetAll(filter, searchText);
        }
        public void Save()
        {
            _repository.Save();
        }
        public IPagedList<TResult> PageList<TResult>(int? page, Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            int pageNumber = page??1;
            int pageSize = 10;
            return _repository.PageList(predicate, selector, orderBy).ToPagedList(pageNumber, pageSize);
        }
    }
}
