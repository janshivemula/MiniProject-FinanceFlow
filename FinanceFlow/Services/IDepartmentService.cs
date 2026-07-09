using FinanceFlow.DTOs;

namespace FinanceFlow.Services
{
    public interface IDepartmentService
    {
        Task<List<DepartmentResponseDto>> GetAllDepartmentsAsync();      
        Task<List<DepartmentResponseDto>> GetActiveDepartmentsAsync();   
        Task<DepartmentResponseDto?> GetDepartmentByIdAsync(int id);     
        Task<string> CreateDepartmentAsync(DepartmentRequestDto dto);
        Task<string> UpdateDepartmentAsync(int id, DepartmentRequestDto dto);
        Task<string> DeleteDepartmentAsync(int id);
    }
}