using Core.Interfaces;
using Infrastructure.Data;
using KuaforRandevu.Application.Exceptions;
using KuaforRandevu.Core.Models;
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

        public async Task<IEnumerable<Stylist>> GetAllStylistAsync()
        {
            return await GetAllAsync();
        }

        public async Task<Stylist?> GetStylistByIdAsync(Guid id)
        {
            var result =  await FindByConditionAsync((s) => s.Id.Equals(id)).FirstAsync();
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
