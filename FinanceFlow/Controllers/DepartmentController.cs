using FinanceFlow.Services;
using FinanceFlow.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FinanceFlow.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _service;

        public DepartmentController(IDepartmentService service)
        {
            _service = service;
        }

        // All departments -> for list page
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllDepartmentsAsync();
            return Ok(result);
        }

        // Only active -> for dropdowns
        [HttpGet("active")]
        public async Task<IActionResult> GetActive()
        {
            var result = await _service.GetActiveDepartmentsAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DepartmentRequestDto dto)
        {
            var result = await _service.CreateDepartmentAsync(dto);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetDepartmentByIdAsync(id);

            if (result == null)
                return NotFound("Department not found");

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DepartmentRequestDto dto)
        {
            var result = await _service.UpdateDepartmentAsync(id, dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteDepartmentAsync(id);
            return Ok(result);
        }
    }
}