using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
namespace CIPLATFORM_SKILL_PM.Repository.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        public void Add(T entity);
        void Delete(T entity);
        void Edit(T entity);
        public T GetFirstordefault(Expression<Func<T, bool>> filter);
        void Save();
        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter,string searchText);
        public IQueryable<TResult> PageList<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
    }
}
