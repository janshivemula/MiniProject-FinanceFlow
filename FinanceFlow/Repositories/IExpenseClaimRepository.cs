using FinanceFlow.Models;

namespace FinanceFlow.Repositories
{
    public interface IExpenseClaimRepository
    {
        Task AddAsync(ExpenseClaim claim);

        Task<List<ExpenseClaim>> GetAllAsync();

        Task<ExpenseClaim?> GetByIdAsync(int id);

        void Update(ExpenseClaim claim);  

        void Delete(ExpenseClaim claim);   

        Task SaveAsync();
    }
}