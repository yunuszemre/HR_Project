using HR.Project.BL.Abstract;
using HR.Project.DAL.Context;
using HR.Project.Entities.Abstract;
using HR.Project.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using System.Text;
using System.Threading.Tasks;

namespace HR.Project.BL.Services
{
    public class BaseService<T> : IGenericService<T> where T : BaseEntity
    {
        private readonly HRProjectContext _context;

        public BaseService(HRProjectContext context)
        {
            _context = context;
        }

        public bool Add(T item)
        {
            try
            {
                _context.Set<T>().Add(item);
                return Save() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<T> GetAll() => _context.Set<T>().ToList();

        public IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes)
        {
            var query = _context.Set<T>().ToList().AsQueryable();
            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            return query;
        }

        public T GetById(int id)
        {
            var item = _context.Set<T>().Find(id);
            return item;
        }

        

        public int Save()
        {
            return _context.SaveChanges();
        }

        public bool Update(T item)
        {
            try
            {
                _context.Set<T>().Update(item);
                return Save() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Any(Expression<Func<T, bool>> predicate) => _context.Set<T>().Any(predicate);

        public T GetByDefault(Expression<Func<T, bool>> predicate) => _context.Set<T>().FirstOrDefault(predicate);
    }
}
