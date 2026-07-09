using FinanceFlow.Data;
using FinanceFlow.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceFlow.Repositories
{
    public class ExpenseClaimRepository : IExpenseClaimRepository
    {
        private readonly FinanceFlowDbContext _context;

        public ExpenseClaimRepository(FinanceFlowDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ExpenseClaim claim)
        {
            await _context.ExpenseClaims.AddAsync(claim);
        }

        public async Task<List<ExpenseClaim>> GetAllAsync()
        {
            return await _context.ExpenseClaims
                .Include(e => e.Employee)
                .Include(e => e.Department)
                .Include(e => e.ExpenseCategory)
                .Include(e => e.ApprovedByEmployee)
                .ToListAsync();
        }

        public async Task<ExpenseClaim?> GetByIdAsync(int id)
        {
            return await _context.ExpenseClaims
                .Include(e => e.Employee)
                .Include(e => e.Department)
                .Include(e => e.ExpenseCategory)
                .Include(e => e.ApprovedByEmployee)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public void Update(ExpenseClaim claim)
        {
            _context.ExpenseClaims.Update(claim);
        }

        public void Delete(ExpenseClaim claim)
        {
            _context.ExpenseClaims.Remove(claim);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}