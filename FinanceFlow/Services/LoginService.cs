using FinanceFlow.DTOs;
using FinanceFlow.Repositories;

namespace FinanceFlow.Services
{
    public class LoginService : ILoginService
    {
        private readonly IEmployeeRepository _employeeRepo;

        public LoginService(IEmployeeRepository employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto dto)
        {
            var employee = await _employeeRepo.LoginAsync(dto.Email, dto.Password);

            if (employee == null)
                return null;

            return new LoginResponseDto
            {
                EmployeeId = employee.EmployeeId,
                EmployeeName = employee.EmployeeName,
                Email = employee.Email,
                Role = employee.Role.ToString(),
                DepartmentName = employee.Department?.DepartmentName ?? string.Empty
            };
        }
    }
}