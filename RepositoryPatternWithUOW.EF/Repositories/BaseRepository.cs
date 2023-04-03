using Microsoft.EntityFrameworkCore;
using RepositoryPatternWithUOW.Core.Consts;
using RepositoryPatternWithUOW.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.EF.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public T Find(Expression<Func<T, bool>> match, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if(includes != null)
                foreach(var include in includes)
                {
                    query = query.Include(include);
                }
           return query.SingleOrDefault(match);
        }
        public IEnumerable<T> FindAll(Expression<Func<T, bool>> match, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            return query.Where(match).ToList();
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, int? take, int? skip, Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending)
        {
            IQueryable<T> query = _context.Set<T>().Where(criteria);
            if(take.HasValue)
                query = query.Take(take.Value);
            if(skip.HasValue)
                query = query.Take(skip.Value);
            if (orderBy != null)
              {
                  if (orderByDirection == OrderBy.Ascending)
                        query = query.OrderBy(orderBy);
                  else
                        query = query.OrderByDescending(orderBy);
               }

            return query.ToList();
        }

        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return entity;
        }
    }
}
