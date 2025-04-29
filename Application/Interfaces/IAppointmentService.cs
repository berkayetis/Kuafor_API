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
    public interface IAppointmentService
    {
        Task CreateAsync(CreateAppointmentDto appt);
        Task<Appointment?> GetByIdAsync(Guid id);
        Task<IEnumerable<Appointment>> GetAllAsync();
        Task<(IEnumerable<AppointmentDto> Appointments ,int TotalCount)> GetPagedAppointmentsAsync(PaginationParams paginationParams);
    }
}
