using FluentValidation;
using KuaforRandevu.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuaforRandevu.Application.Validators
{
    public class CreateCustomerValidator : AbstractValidator<CreateCustomerDto>
    {
        public CreateCustomerValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().WithMessage("İsim boş bırakılamaz.");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Telefon numarası boş bırakılamaz.");
        }
    }
}
