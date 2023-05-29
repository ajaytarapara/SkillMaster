using CIPLATFORM_SKILL_PM.Models.Data;
using CIPLATFORM_SKILL_PM.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CIPLATFORM_SKILL_PM.Repository.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class

    {
        private readonly SkillMasterDbContext _db;

        protected DbSet<T> dbset;

        public GenericRepository(SkillMasterDbContext db)
        {
            _db = db;
            this.dbset = _db.Set<T>();
        }
        public void Add(T entity)
        {
            dbset.Add(entity);

        }
        public void Delete(int id)
        {
            T entity =dbset.Find(id);
            dbset.Remove(entity);
        }

        public void Edit(T entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter,string searchText)
        {
            IQueryable<T> query = dbset;
            if (searchText != null)
            {
                query = query.Where(filter);
            }
            return query.AsEnumerable();
        }
        public T GetFirstordefault(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbset;
            query = query.Where(filter);
            return query.FirstOrDefault();
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public IPagedList<TResult> PageList<TResult>(Expression<Func<T, bool>> predicate, int? page, int? Size, Expression<Func<T, TResult>> selector, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy)
        {
            int pageNumber = page ?? 1;
            int pageSize = Size??10;
            IQueryable<T> query = dbset;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            IQueryable<TResult> result = query.Select(selector);
            return result.ToPagedList(pageNumber, pageSize); ;
        }
    }
}
