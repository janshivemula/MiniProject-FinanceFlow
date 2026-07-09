using FinanceFlow.DTOs;
using global::FinanceFlow.DTOs;

namespace FinanceFlow.Services
{
    public interface IEmployeeService
    {
            Task<List<EmployeeResponseDto>> GetAllAsync();
            Task<EmployeeResponseDto?> GetByIdAsync(int id);
            Task<string> AddAsync(EmployeeRequestDto dto);
            Task<string> UpdateEmployeeAsync(int id, EmployeeRequestDto dto);
            Task<string> DeleteEmployeeAsync(int id);



    }
}

