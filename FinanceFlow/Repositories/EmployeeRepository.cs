using FinanceFlow.Data;
using FinanceFlow.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceFlow.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly FinanceFlowDbContext _context;

        public EmployeeRepository(FinanceFlowDbContext context)
        {
            _context = context;
        }

        //public async Task<List<Employee>> GetAllAsync()
        //{
        //    return await _context.Employees
        //        .Include(e => e.Department)
        //        .Where(e => e.IsActive)
        //        .ToListAsync();
        //}

        //public async Task<Employee?> GetByIdAsync(int id)
        //{
        //    return await _context.Employees
        //        .Include(e => e.Department)
        //        .FirstOrDefaultAsync(e => e.EmployeeId == id && e.IsActive);
        //}
        public async Task<List<Employee>> GetAllAsync()
        {
            return await _context.Employees
                .Include(e => e.Department)
                .ToListAsync();
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _context.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.EmployeeId == id);
        }

        public async Task<Employee?> LoginAsync(string email, string password)
        {
            return await _context.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(e =>
                    e.Email == email &&
                    e.Password == password &&
                    e.IsActive);
        }

        public void Update(Employee employee)
        {
            _context.Employees.Update(employee);
        }

        public async Task AddAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}