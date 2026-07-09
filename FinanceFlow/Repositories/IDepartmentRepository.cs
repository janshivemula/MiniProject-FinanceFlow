using FinanceFlow.Models;

namespace FinanceFlow.Repositories
{
    public interface IDepartmentRepository
    {
        Task<List<Department>> GetAllAsync();          
        Task<List<Department>> GetActiveAsync();       
        Task<Department?> GetByIdAsync(int id);        
        Task AddAsync(Department department);
        void Update(Department department);
        Task SaveAsync();
    }
}