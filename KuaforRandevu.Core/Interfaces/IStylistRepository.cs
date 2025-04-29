using KuaforRandevu.Core.Models;
using KuaforRandevu.Core.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IStylistRepository
    {
        Task CreateStylist(Stylist stylist);

        Task<Stylist?> GetStylistByIdAsync(Guid id);

        Task<(IEnumerable<Stylist> Items, int TotalCount)> GetAllPagedStylistsAsync(PaginationParams paginationParams);

        Task<IEnumerable<Stylist>> GetAllStylistsAsync();
    }
}
