using Infrastructure.Data;
using KuaforRandevu.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KuaforRandevu.Infrastructure.Implementations
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly AppointmentContext _context;
        public RepositoryBase(AppointmentContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var result = await _context.Set<T>().ToListAsync();
            return result;
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public IQueryable<T> FindByConditionAsync(Expression<Func<T, bool>> predicate, bool trackChanges = false)
        {
            if(trackChanges)
            {
                var result =  _context.Set<T>().Where(predicate).AsNoTracking();
                return result;
            }
            else
            {
                var result = _context.Set<T>().Where(predicate);
                return result;
            }
        }

    }
}
