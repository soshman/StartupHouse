using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace StartupHouse.Database.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Queryable { get; }

        T Get(Expression<Func<T, bool>> predicate);

        IEnumerable<T> Fetch(Expression<Func<T, bool>> predicate);

        IQueryable<T> Query();

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
