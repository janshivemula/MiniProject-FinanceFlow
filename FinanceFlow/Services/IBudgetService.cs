using FinanceFlow.DTOs;

namespace FinanceFlow.Services
{
    public interface IBudgetService
    {
        Task<string> AddOrUpdateBudgetAsync(DepartmentBudgetRequestDto dto);

        Task<DepartmentBudgetResponseDto?> GetBudgetAsync(int departmentId, int month, int year);

        Task<List<DepartmentBudgetResponseDto>> GetAllAsync();

        Task<string> DeleteBudgetAsync(int id);
    }
}