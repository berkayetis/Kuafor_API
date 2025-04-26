using KuaforRandevu.Application.Dtos;
using AutoMapper;
using Core.Interfaces;
using KuaforRandevu.Core.Interfaces;
using KuaforRandevu.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementations
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public AppointmentService(IRepositoryManager repository, IMapper mapper)
        {
            _repositoryManager = repository;
            _mapper = mapper;
        }

        public async Task CreateAsync(CreateAppointmentDto appointmentDto)
        {
            var appointment = _mapper.Map<Appointment>(appointmentDto);

            // 1) Çakışma kontrolü
            bool conflict = await _repositoryManager.AppointmentRepo.ConflictsAsync(appointmentDto.StylistId, appointmentDto.StartTime, appointmentDto.EndTime);

            if (conflict)
                throw new InvalidOperationException("Seçilen zaman diliminde zaten başka bir randevu var.");

            // 2) Yeni randevuyu ekle
            await _repositoryManager.AppointmentRepo.CreateAppointmentAsync(appointment);

            // Değişiklikleri veritabanına commit et
            await _repositoryManager.SaveAsync();
        }

        public Task<IEnumerable<Appointment>> GetAllAsync()
        {
            return _repositoryManager.AppointmentRepo.GetAllAppointmentAsync();
        }

        public Task<Appointment?> GetByIdAsync(Guid id)
        {
            return _repositoryManager.AppointmentRepo.GetAppointmentByIdAsync(id);
        }
    }
}
