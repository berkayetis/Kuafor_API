using Infrastructure.Data;
using KuaforRandevu.Core.Interfaces;
using KuaforRandevu.Core.Parameters;
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
        protected readonly DbSet<T> _dbSet;

        public RepositoryBase(AppointmentContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public async Task CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var result = await _dbSet.ToListAsync();
            return result;
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> predicate, bool trackChanges = false)
        {
            if(trackChanges)
            {
                var result =  _dbSet.Where(predicate).AsNoTracking();
                return result;
            }
            else
            {
                var result = _dbSet.Where(predicate);
                return result;
            }
        }

        public async Task<(IEnumerable<T> Items, int TotalCount)> GetPagedAsync(PaginationParams paginationParams, 
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>,IOrderedQueryable<T>>? orderBy = null,
            bool trackChanges = false)
        {            
            // 1) Temel sorgu:  filtre var ise ona göre, yoksa tüm tablo
            IQueryable<T> query = predicate != null ? FindByCondition(predicate, trackChanges) : (trackChanges? _dbSet : _dbSet.AsNoTracking());

            // 2) Ordering: paginate etmeden önce sıralayın
            if (orderBy != null)
                query = orderBy(query);

            // 3) Toplam kayıt sayısını çek
            var totalCount = await query.CountAsync();

            // 4) Skip/Take ile sayfayı al
            var items = await query
                .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                .Take(paginationParams.PageSize)
                .ToListAsync();

            return (items, totalCount);
        }
    }
}
