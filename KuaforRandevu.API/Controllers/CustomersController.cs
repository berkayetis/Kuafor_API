using Core.Interfaces;
using KuaforRandevu.Application.Dtos;
using KuaforRandevu.Core.Interfaces;
using KuaforRandevu.Core.Models;
using KuaforRandevu.Core.Parameters;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace KuaforRandevu.Application.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomersController(ICustomerService customerRepository)
        {
            _customerService = customerRepository;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedCustomers([FromQuery] PaginationParams paginationParams)
        {
            var customers = await _customerService.GetAllPagedCustomersAsync(paginationParams);
            Response.Headers.Add("X-Total-Count", customers.TotalCount.ToString());
            return Ok(customers.Customers);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerDto createCustomerDto)
        {
            if (createCustomerDto == null)
            {
                return BadRequest("customerDto cannot be null");
            }

            CustomerDto customerDto = await _customerService.CreateCustomerAsync(createCustomerDto);
            return StatusCode(201, customerDto);
        }
    }
}
