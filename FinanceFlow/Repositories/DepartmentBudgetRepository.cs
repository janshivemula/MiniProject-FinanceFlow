using FinanceFlow.Data;
using FinanceFlow.Models;
using Microsoft.EntityFrameworkCore;


namespace FinanceFlow.Repositories
{
    

    public class DepartmentBudgetRepository : IDepartmentBudgetRepository
    {
        private readonly FinanceFlowDbContext _context;

        public DepartmentBudgetRepository(FinanceFlowDbContext context)
        {
            _context = context;
        }

        public async Task<DepartmentBudget?> GetBudgetAsync(int departmentId, int month, int year)
        {
            return await _context.DepartmentBudgets
                .Include(b => b.Department)
                .FirstOrDefaultAsync(b =>
                    b.DepartmentId == departmentId &&
                    b.Month == month &&
                    b.Year == year);
        }
        public async Task<List<DepartmentBudget>> GetAllAsync()
        {
            return await _context.DepartmentBudgets
                .Include(b => b.Department)
                .ToListAsync();
        }

        public async Task AddAsync(DepartmentBudget budget)
        {
            await _context.DepartmentBudgets.AddAsync(budget);
        }
        public async Task<DepartmentBudget?> GetByIdAsync(int id)
        {
            return await _context.DepartmentBudgets
                .Include(b => b.Department)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public void Update(DepartmentBudget budget)
        {
            _context.DepartmentBudgets.Update(budget);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
