using KuaforRandevu.Application.Dtos;
using Core.Interfaces;
using KuaforRandevu.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using KuaforRandevu.Core.Parameters;

namespace KuaforRandevu.Application.Controllers
{
    [ApiController]
    [Route("api/stylists")]
    public class StylistsController : ControllerBase
    {
        private readonly IStylistService _stylistService;

        public StylistsController(IStylistService stylistService)
        {
            _stylistService = stylistService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStylists([FromQuery] PaginationParams paginationParams)
        {
            var stylists = await _stylistService.GetAllPagedAsync(paginationParams);
            Response.Headers.Add("X-Total-Count", stylists.TotalCount.ToString());
            return Ok(stylists.Stylists);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStylistById(Guid id)
        {
            var stylist = await _stylistService.GetByIdAsync(id);
            if (stylist == null)
            {
                return NotFound();
            }
            return Ok(stylist);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStylist([FromBody] CreateStylistDto createStylistDto)
        {
            if (createStylistDto == null)
            {
                return BadRequest();
            }
            StylistDto stylistDto = await _stylistService.CreateAsync(createStylistDto);
            return StatusCode(201, stylistDto);
        }
    }
}
