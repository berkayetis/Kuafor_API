using KuaforRandevu.Application.Dtos;
using AutoMapper;
using Core.Interfaces;
using KuaforRandevu.Core.Models;
using Microsoft.AspNetCore.Mvc;
using KuaforRandevu.Core.Parameters;

namespace KuaforRandevu.Application.Controllers
{
    [ApiController]
    [Route("api/appointments")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _service;

        public AppointmentsController(IAppointmentService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateAppointmentDto createAppointmentDto)
        {
            await _service.CreateAsync(createAppointmentDto);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedAppointments([FromQuery] PaginationParams paginationParams)
        {
            var result = await _service.GetPagedAppointmentsAsync(paginationParams);
            Response.Headers.Add("X-Total-Count", result.TotalCount.ToString());
            return Ok(result.Appointments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var appt = await _service.GetByIdAsync(id);
            if (appt == null) return NotFound();
            return Ok(appt);
        }
    }

}
