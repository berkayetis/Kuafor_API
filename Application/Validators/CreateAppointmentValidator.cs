using KuaforRandevu.Application.Dtos;
using FluentValidation;

namespace KuaforRandevu.Application.Validators
{
    public class CreateAppointmentValidator : AbstractValidator<CreateAppointmentDto>
    {
        public CreateAppointmentValidator()
        {
            RuleFor(x => x.EndTime).GreaterThan(x => x.StartTime).WithMessage("Bitiş zamanı, başlangıçtan sonra olmalı.");
            RuleFor(x => x.ServiceType).NotEmpty().WithMessage("Hizmet tipi boş bırakılamaz.");
        }
    }
}
