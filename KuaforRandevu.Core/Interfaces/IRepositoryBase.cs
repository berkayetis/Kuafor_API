using KuaforRandevu.Core.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KuaforRandevu.Core.Interfaces
{
    public interface IRepositoryBase<T> where T : class
    {
        /// <summary>
        /// Sayfalı olarak T varlıklarını getirir. 
        /// </summary>
        Task<(IEnumerable<T> Items, int TotalCount)> GetPagedAsync(
            PaginationParams paginationParams,
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            bool trackChanges = false);

        Task<IEnumerable<T>> GetAllAsync();     

        void Update(T entity);

        void Delete(T identity);

        /// <summary>
        /// Verilen koşula uyan tüm T varlıklarını döner.
        /// </summary>
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> predicate, bool trackChanges = false); 

        Task CreateAsync(T entity);
    }
}
