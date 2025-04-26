using KuaforRandevu.Core.Models;

namespace KuaforRandevu.Application.Dtos
{
    public record CreateAppointmentDto(
    Guid CustomerId,
    Guid StylistId,
    DateTime StartTime,
    DateTime EndTime,
    string ServiceType
    );
}
