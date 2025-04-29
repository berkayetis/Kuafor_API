using Core.Interfaces;
using Infrastructure.Data;
using KuaforRandevu.Application.Exceptions;
using KuaforRandevu.Core.Models;
using KuaforRandevu.Core.Parameters;
using KuaforRandevu.Infrastructure.Implementations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementations
{
    public class StylistRepository : RepositoryBase<Stylist> ,IStylistRepository
    {
        public StylistRepository(AppointmentContext context) : base(context)
        {
        }

        public async Task CreateStylist(Stylist stylist)
        {
            await CreateAsync(stylist);
        }

        public async Task<(IEnumerable<Stylist> Items, int TotalCount)> GetAllPagedStylistsAsync(PaginationParams paginationParams)
        {
            return await GetPagedAsync(paginationParams);
        }

        public async Task<IEnumerable<Stylist>> GetAllStylistsAsync()
        {
            return await GetAllAsync();
        }

        public async Task<Stylist?> GetStylistByIdAsync(Guid id)
        {
            var result =  await FindByCondition((s) => s.Id.Equals(id)).FirstAsync();
            if (result == null)
            {
                throw new NotFoundException($"{id} stylist is not found");
            }
            else
            {
                return result;
            }
        }
    }
}
