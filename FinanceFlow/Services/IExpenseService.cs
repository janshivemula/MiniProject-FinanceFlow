using FinanceFlow.DTOs;

namespace FinanceFlow.Services
{
    public interface IExpenseService
    {
        Task<string> AddExpenseAsync(ExpenseClaimRequestDto dto);

        Task<List<ExpenseClaimResponseDto>> GetAllAsync();

        Task<ExpenseClaimResponseDto?> GetByIdAsync(int id);

        Task<string> UpdateExpenseAsync(int id, ExpenseClaimRequestDto dto);   

        Task<string> DeleteExpenseAsync(int id);

        Task<string> ApproveClaimAsync(int claimId, int employeeId, string remark);

        Task<string> RejectClaimAsync(int claimId, int employeeId, string remark);
    }
}