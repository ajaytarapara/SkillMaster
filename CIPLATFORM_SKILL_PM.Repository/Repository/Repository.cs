using CIPLATFORM_SKILL_PM.Models.Data;
using CIPLATFORM_SKILL_PM.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CIPLATFORM_SKILL_PM.Repository.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    
    {
        private readonly SkillDbContext _db;

        protected DbSet<T> dbset;

        public Repository(SkillDbContext db)
        {
            _db= db;
            this.dbset= _db.Set<T>();
        }
        public void Add(T entity)
        {
            dbset.Add(entity);
           
        }
        public virtual void Delete(T entity)
        {
            dbset.Remove(entity);
        }

        public virtual void Edit(T entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
        }
        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = dbset;
            return query.ToList();  
        }

        public T GetFirstordefault(Expression<Func<T, bool>> filter)
        {
           IQueryable<T> query = dbset;
            query= query.Where(filter);
            return query.FirstOrDefault();
        }

        public void Remove(T entity)
        {
           dbset.Remove(entity);
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
