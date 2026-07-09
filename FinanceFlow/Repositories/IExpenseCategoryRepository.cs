using FinanceFlow.Models;

namespace FinanceFlow.Repositories
{
    public interface IExpenseCategoryRepository
    {
        Task<List<ExpenseCategory>> GetAllAsync();       
        Task<List<ExpenseCategory>> GetActiveAsync();    
        Task<ExpenseCategory?> GetByIdAsync(int id);     
        void Update(ExpenseCategory category);
        Task AddAsync(ExpenseCategory category);
        Task SaveAsync();
    }
}