﻿using CIPLATFORM_SKILL_PM.Models.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CIPLATFORM_SKILL_PM.Services.Interface
{
    public interface IEntityService<T> where T : class
    {
        void Delete(int id);
        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter,string searchText);
        public T GetFirstordefault(Expression<Func<T, bool>> filter);
        void Save();
        public IPagedList<TResult> PageList<TResult>(int? page,int?size, Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
    }
}
