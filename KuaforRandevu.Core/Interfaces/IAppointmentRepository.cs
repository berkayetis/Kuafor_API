using KuaforRandevu.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuaforRandevu.Core.Interfaces
{
    public interface IAppointmentRepository
    {
        /// <summary>
        /// Yeni bir randevu ekler.
        /// </summary>
        Task CreateAppointmentAsync(Appointment appointment);

        /// <summary>
        /// Verilen stylist için, [start,end) zaman aralığında çakışan başka bir randevu var mı?
        /// </summary>
        Task<bool> ConflictsAsync(Guid stylistId, DateTime start, DateTime end);

        /// <summary>
        /// Id’si verilen randevuyu getirir. Bulamazsa null döner.
        /// </summary>
        Task<Appointment?> GetAppointmentByIdAsync(Guid id);

        /// <summary>
        /// Tüm randevuları liste olarak döner.
        /// </summary>
        Task<IEnumerable<Appointment>> GetAllAppointmentAsync();
    }

}
