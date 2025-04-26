using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuaforRandevu.Application.Dtos
{
    public record AppointmentDto
    (
        Guid Id,
        Guid CustomerId,
        Guid StylistId,
        DateTime StartTime,
        DateTime EndTime,
        string ServiceType
    );
}
