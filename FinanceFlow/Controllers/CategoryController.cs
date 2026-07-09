using FinanceFlow.DTOs;
using FinanceFlow.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinanceFlow.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IExpenseCategoryService _service;

        public CategoryController(IExpenseCategoryService service)
        {
            _service = service;
        }

        // All categories -> list page
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllCategoriesAsync();
            return Ok(result);
        }

        // Only active -> dropdowns
        [HttpGet("active")]
        public async Task<IActionResult> GetActive()
        {
            var result = await _service.GetActiveCategoriesAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetCategoryByIdAsync(id);

            if (result == null)
                return NotFound("Category not found");

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ExpenseCategoryRequestDto dto)
        {
            var result = await _service.CreateCategoryAsync(dto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ExpenseCategoryRequestDto dto)
        {
            var result = await _service.UpdateCategoryAsync(id, dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteCategoryAsync(id);
            return Ok(result);
        }
    }
}