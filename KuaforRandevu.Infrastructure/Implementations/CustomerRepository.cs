using Core.Interfaces;
using Infrastructure.Data;
using KuaforRandevu.Core.Models;
using KuaforRandevu.Core.Parameters;
using KuaforRandevu.Infrastructure.Implementations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementations
{
    public class CustomerRepository : RepositoryBase<Customer>,ICustomerRepository
    {
        public CustomerRepository(AppointmentContext context) : base(context)
        {
        }

        public async Task CreateCustomerAsync(Customer customer)
        {
            await CreateAsync(customer);
        }
        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await GetAllAsync();
        }

        public async Task<(IEnumerable<Customer> Items, int TotalCount)> GetAllPagedCustomersAsync(PaginationParams paginationParams)
        {
            var result = await GetPagedAsync(paginationParams);
            return result;
        }

        public async Task<Customer?> GetCustomerByIdAsync(Guid id)
        {
            return await FindByCondition(c => c.Id == id).FirstAsync();  
        }
    }
}
