using AutoMapper;
using Core.Interfaces;
using Infrastructure.Cache.Contracts;
using KuaforRandevu.Application.Dtos;
using KuaforRandevu.Application.Exceptions;
using KuaforRandevu.Core.Interfaces;
using KuaforRandevu.Core.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly IRedisCacheService _cache;

        private const string AllKey = "customers:all";

        public CustomerService(IRepositoryManager repositoryManager, IMapper mapper, IRedisCacheService cache)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto createCustomerDto)
        {
            var customer = _mapper.Map<Customer>(createCustomerDto);
            await _repositoryManager.CustomerRepo.CreateCustomerAsync(customer);

            // Değişiklikleri veritabanına commit et
            await _repositoryManager.SaveAsync();

            try
            {
                // Yeni eklendiği için önbelleği temizleyelim
                await _cache.RemoveAsync(AllKey);
            }
            catch (Exception ex)
            {
                throw new Exception(AllKey + " cache is not available", ex);
            }

            // convert to DTO
            var customerDto = _mapper.Map<CustomerDto>(customer);
            return customerDto;
        }

        public async Task<CustomerDto?> GetCustomerByIdAsync(Guid id)
        {
            var resultCustomer = await _repositoryManager.CustomerRepo.GetCustomerByIdAsync(id);

            if (resultCustomer is null)
            {
                throw new NotFoundException($"Customer '{id}' not found");
            }

            var customerDto = _mapper.Map<CustomerDto>(resultCustomer);
            return customerDto;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            // Cache’den oku
            try
            {
                var cached = await _cache.GetAsync<IEnumerable<Customer>>(AllKey);
                if (cached is not null)
                {
                    return cached;
                }
            }
            catch (Exception ex)
            {
                Log.Warning(ex, "Redis cache unavailable, falling back to DB");

                throw new Exception(AllKey + " cache is not available", ex);
            }

            // get all from database
            var customerList = await _repositoryManager.CustomerRepo.GetAllCustomersAsync();

            // set cache
            await _cache.SetAsync(AllKey, customerList, TimeSpan.FromMinutes(5));

            return customerList;
        }
    }
}
