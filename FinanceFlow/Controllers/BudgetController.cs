using FinanceFlow.DTOs;
using FinanceFlow.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinanceFlow.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BudgetController : ControllerBase
    {
        private readonly IBudgetService _service;

        public BudgetController(IBudgetService service)
        {
            _service = service;
        }

        // ADD / UPDATE
        [HttpPost]
        public async Task<IActionResult> AddOrUpdate([FromBody] DepartmentBudgetRequestDto dto)
        {
            var result = await _service.AddOrUpdateBudgetAsync(dto);
            return Ok(result);
        }

        // GET BY ID 
        [HttpGet("{departmentId}/{month}/{year}")]
        public async Task<IActionResult> Get(int departmentId, int month, int year)
        {
            var result = await _service.GetBudgetAsync(departmentId, month, year);

            if (result == null)
                return NotFound("Budget not found");

            return Ok(result);
        }

        // GET ALL
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteBudgetAsync(id);
            return Ok(result);
        }
    }
}