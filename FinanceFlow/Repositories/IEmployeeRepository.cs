using FinanceFlow.Models;

namespace FinanceFlow.Repositories
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllAsync();

        Task<Employee?> GetByIdAsync(int id);
        Task<Employee?> LoginAsync(string email, string password);

        Task AddAsync(Employee employee);

        void Update(Employee employee);

        Task SaveAsync();
    }
}

