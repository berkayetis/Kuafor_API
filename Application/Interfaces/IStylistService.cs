using KuaforRandevu.Application.Dtos;
using KuaforRandevu.Core.Models;
using KuaforRandevu.Core.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IStylistService
    {
        Task<StylistDto> CreateAsync(CreateStylistDto stylistDto);
        Task<StylistDto?> GetByIdAsync(Guid id);
        Task<(IEnumerable<Stylist> Stylists, int TotalCount)> GetAllPagedAsync(PaginationParams paginationParams);
    }
}
