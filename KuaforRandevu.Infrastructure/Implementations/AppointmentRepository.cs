using Core.Interfaces;
using Infrastructure.Data;
using KuaforRandevu.Core.Interfaces;
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
    public class AppointmentRepository : RepositoryBase<Appointment> ,IAppointmentRepository
    {
        private readonly AppointmentContext _context;
        public AppointmentRepository(AppointmentContext context) : base(context)
        {
            this._context = context;
        }

        public async Task CreateAppointmentAsync(Appointment appointment)
        {
            await CreateAsync(appointment);
        }
        public async Task<bool> ConflictsAsync(Guid stylistId, DateTime start, DateTime end)
        {
            return await _context.Appointments
                .AsNoTracking()
                .AnyAsync(a =>
                    a.StylistId == stylistId
                    && a.StartTime < end
                    && a.EndTime > start
                );
        }

        public async Task<IEnumerable<Appointment>> GetAllAppointmentAsync()
        {
            return await _context.Appointments
                            .Include(a => a.Customer)
                            .Include(a => a.Stylist)
                            .OrderBy(a => a.StartTime)
                            .ToListAsync();
        }

        public async Task<Appointment?> GetAppointmentByIdAsync(Guid id)
        {
            return await _context.Appointments
                           .Include(a => a.Customer)
                           .Include(a => a.Stylist)
                           .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<(IEnumerable<Appointment> Appointments, int TotalCount)> GetPagedAsync(PaginationParams paginationParams)
        {
            var result = await GetPagedAsync(paginationParams, predicate: null, trackChanges: false);
            return result;
        }
    }
}
