using FinanceFlow.DTOs;
using FinanceFlow.Models;
using FinanceFlow.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinanceFlow.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _service;

        public ExpenseController(IExpenseService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> AddExpense([FromBody] ExpenseClaimRequestDto dto)
        {
            return Ok(await _service.AddExpenseAsync(dto));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ExpenseClaimRequestDto dto)
        {
            return Ok(await _service.UpdateExpenseAsync(id, dto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _service.DeleteExpenseAsync(id));
        }

        
        [HttpPut("approve/{id}")]
        public async Task<IActionResult> Approve( int id, [FromQuery] int employeeId,
        [FromQuery] string remark)
        {
            return Ok(await _service.ApproveClaimAsync(id, employeeId, remark));
        }

        [HttpPut("reject/{id}")]
        public async Task<IActionResult> Reject(int id,
        [FromQuery] int employeeId,
        [FromQuery] string remark)
        {
            return Ok(await _service.RejectClaimAsync(id, employeeId, remark));
        }
    }
}