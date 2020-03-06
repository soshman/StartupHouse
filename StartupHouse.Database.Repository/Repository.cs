using StartupHouse.Database.Entities;
using StartupHouse.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace StartupHouse.Database.Repository
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private readonly ShDbContext _context;

        public Repository(ShDbContext context)
        {
            _context = context;
        }

        public IQueryable<T> Queryable => _context.Set<T>();

        public T Get(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
         
            return Queryable.FirstOrDefault(predicate);
        }

        public IEnumerable<T> Fetch(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return Queryable.Where(predicate).ToList();
        }

        public IEnumerable<T> Fetch()
        {
            return Queryable.ToList();
        }

        public IQueryable<T> Query()
        {
            return Queryable;
        }

        public void Add(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _context.Add(entity);
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _context.Update(entity);
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _context.Remove(entity);
        }
    }
}
