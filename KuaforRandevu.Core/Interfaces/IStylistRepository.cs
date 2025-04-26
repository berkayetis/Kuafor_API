using KuaforRandevu.Core.Models;
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

        Task<IEnumerable<Stylist>> GetAllStylistAsync();
    }
}
