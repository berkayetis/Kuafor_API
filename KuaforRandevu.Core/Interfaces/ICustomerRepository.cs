using KuaforRandevu.Core.Models;
using KuaforRandevu.Core.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICustomerRepository
    {
        Task CreateCustomerAsync(Customer customer);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer?> GetCustomerByIdAsync(Guid id);
        Task<(IEnumerable<Customer> Items, int TotalCount)> GetAllPagedCustomersAsync(PaginationParams paginationParams);

    }
}
