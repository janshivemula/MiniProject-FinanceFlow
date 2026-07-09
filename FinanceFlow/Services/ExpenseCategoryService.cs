using FinanceFlow.DTOs;
using FinanceFlow.Models;
using FinanceFlow.Repositories;

namespace FinanceFlow.Services
{
    public class ExpenseCategoryService : IExpenseCategoryService
    {
        private readonly IExpenseCategoryRepository _repo;

        public ExpenseCategoryService(IExpenseCategoryRepository repo)
        {
            _repo = repo;
        }

        // Category LIST -> all categories
        public async Task<List<ExpenseCategoryResponseDto>> GetAllCategoriesAsync()
        {
            var categories = await _repo.GetAllAsync();

            return categories.Select(c => new ExpenseCategoryResponseDto
            {
                Id = c.Id,
                CategoryName = c.CategoryName,
                IsActive = c.IsActive
            }).ToList();
        }

        // Dropdown -> only active categories
        public async Task<List<ExpenseCategoryResponseDto>> GetActiveCategoriesAsync()
        {
            var categories = await _repo.GetActiveAsync();

            return categories.Select(c => new ExpenseCategoryResponseDto
            {
                Id = c.Id,
                CategoryName = c.CategoryName,
                IsActive = c.IsActive
            }).ToList();
        }

        // Edit -> active only
        public async Task<ExpenseCategoryResponseDto?> GetCategoryByIdAsync(int id)
        {
            var category = await _repo.GetByIdAsync(id);

            if (category == null)
                return null;

            return new ExpenseCategoryResponseDto
            {
                Id = category.Id,
                CategoryName = category.CategoryName,
                IsActive = category.IsActive
            };
        }

        public async Task<string> CreateCategoryAsync(ExpenseCategoryRequestDto dto)
        {
            var category = new ExpenseCategory
            {
                CategoryName = dto.CategoryName,
                IsActive = true
            };

            await _repo.AddAsync(category);
            await _repo.SaveAsync();

            return "Category created successfully";
        }

        public async Task<string> UpdateCategoryAsync(int id, ExpenseCategoryRequestDto dto)
        {
            var category = await _repo.GetByIdAsync(id);

            if (category == null)
                return "Category not found or inactive";

            category.CategoryName = dto.CategoryName;

            _repo.Update(category);
            await _repo.SaveAsync();

            return "Category updated successfully";
        }

        public async Task<string> DeleteCategoryAsync(int id)
        {
            var category = await _repo.GetByIdAsync(id);

            if (category == null)
                return "Category not found or already inactive";

            category.IsActive = false;

            _repo.Update(category);
            await _repo.SaveAsync();

            return "Category deactivated successfully";
        }
    }
}