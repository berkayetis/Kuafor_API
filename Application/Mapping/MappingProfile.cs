using KuaforRandevu.Application.Dtos;
using AutoMapper;
using KuaforRandevu.Core.Models;

namespace KuaforRandevu.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //appointment
            CreateMap<CreateAppointmentDto, Appointment>();
            CreateMap<Appointment, AppointmentDto>();

            //stylist
            CreateMap<Stylist, StylistDto>();
            CreateMap<CreateStylistDto, Stylist>();

            //customer
            CreateMap<CreateCustomerDto, Customer>(); 
            CreateMap<Customer, CustomerDto>();
        }
    }
}
