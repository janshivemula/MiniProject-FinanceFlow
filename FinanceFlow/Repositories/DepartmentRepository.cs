using FinanceFlow.Data;
using FinanceFlow.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceFlow.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly FinanceFlowDbContext _context;

        public DepartmentRepository(FinanceFlowDbContext context)
        {
            _context = context;
        }

        // For department list page -> show all
        public async Task<List<Department>> GetAllAsync()
        {
            return await _context.Departments.ToListAsync();
        }

        // For dropdowns -> only active
        public async Task<List<Department>> GetActiveAsync()
        {
            return await _context.Departments
                .Where(d => d.IsActive)
                .ToListAsync();
        }

        // For edit/delete -> only active department can be edited/deleted
        public async Task<Department?> GetByIdAsync(int id)
        {
            return await _context.Departments
                .FirstOrDefaultAsync(d => d.Id == id && d.IsActive);
        }

        public async Task AddAsync(Department department)
        {
            await _context.Departments.AddAsync(department);
        }

        public void Update(Department department)
        {
            _context.Departments.Update(department);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}