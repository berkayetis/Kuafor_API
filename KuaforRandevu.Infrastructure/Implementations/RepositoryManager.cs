using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Implementations;
using KuaforRandevu.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuaforRandevu.Infrastructure.Implementations
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly AppointmentContext _context;

        // Repositories
        private readonly Lazy<IAppointmentRepository> _appointmentRepository;
        private readonly Lazy<IStylistRepository> _stylistRepository;
        private readonly Lazy<ICustomerRepository> _customerRepository;

        public RepositoryManager(AppointmentContext context)
        {
            _context = context;
            _appointmentRepository = new Lazy<IAppointmentRepository>(() => new AppointmentRepository(_context));
            _stylistRepository = new Lazy<IStylistRepository>(() => new StylistRepository(_context));
            _customerRepository = new Lazy<ICustomerRepository>(() => new CustomerRepository(_context));
        }

        public IAppointmentRepository AppointmentRepo => _appointmentRepository.Value;

        public IStylistRepository StylistRepo => _stylistRepository.Value;

        public ICustomerRepository CustomerRepo => _customerRepository.Value;

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
