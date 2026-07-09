using FinanceFlow.Models;

namespace FinanceFlow.Repositories
{
    public interface IDepartmentBudgetRepository
    {
        Task<DepartmentBudget?> GetBudgetAsync(int deptId, int month, int year);
        Task<DepartmentBudget?> GetByIdAsync(int id);
        void Update(DepartmentBudget budget);
        Task AddAsync(DepartmentBudget budget);
        Task<List<DepartmentBudget>> GetAllAsync();
        Task SaveAsync();
    }
}
