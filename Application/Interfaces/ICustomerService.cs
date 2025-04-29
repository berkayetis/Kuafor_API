using KuaforRandevu.Application.Dtos;
using KuaforRandevu.Core.Models;
using KuaforRandevu.Core.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto customer);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<CustomerDto> GetCustomerByIdAsync(Guid id);
        Task<(IEnumerable<Customer> Customers, int TotalCount)> GetAllPagedCustomersAsync(PaginationParams paginationParams);
    }
}
