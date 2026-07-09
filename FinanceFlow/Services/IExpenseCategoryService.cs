
using FinanceFlow.DTOs;

namespace FinanceFlow.Services
{
    public interface IExpenseCategoryService
    {
        Task<List<ExpenseCategoryResponseDto>> GetAllCategoriesAsync();      
        Task<List<ExpenseCategoryResponseDto>> GetActiveCategoriesAsync();   
        Task<ExpenseCategoryResponseDto?> GetCategoryByIdAsync(int id);      
        Task<string> CreateCategoryAsync(ExpenseCategoryRequestDto dto);
        Task<string> UpdateCategoryAsync(int id, ExpenseCategoryRequestDto dto);
        Task<string> DeleteCategoryAsync(int id);
    }
}