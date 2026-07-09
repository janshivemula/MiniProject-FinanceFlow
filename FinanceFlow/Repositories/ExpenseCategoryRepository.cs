using FinanceFlow.Data;
using FinanceFlow.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceFlow.Repositories
{
    public class ExpenseCategoryRepository : IExpenseCategoryRepository
    {
        private readonly FinanceFlowDbContext _context;

        public ExpenseCategoryRepository(FinanceFlowDbContext context)
        {
            _context = context;
        }

        // For category list page -> all
        public async Task<List<ExpenseCategory>> GetAllAsync()
        {
            return await _context.ExpenseCategories.ToListAsync();
        }

        // For dropdowns -> only active
        public async Task<List<ExpenseCategory>> GetActiveAsync()
        {
            return await _context.ExpenseCategories
                .Where(c => c.IsActive)
                .ToListAsync();
        }

        // For edit/delete -> only active category
        public async Task<ExpenseCategory?> GetByIdAsync(int id)
        {
            return await _context.ExpenseCategories
                .FirstOrDefaultAsync(c => c.Id == id && c.IsActive);
        }

        public void Update(ExpenseCategory category)
        {
            _context.ExpenseCategories.Update(category);
        }

        public async Task AddAsync(ExpenseCategory category)
        {
            await _context.ExpenseCategories.AddAsync(category);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}