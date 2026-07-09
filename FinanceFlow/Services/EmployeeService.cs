using FinanceFlow.DTOs;
using FinanceFlow.Models;
using FinanceFlow.Repositories;

namespace FinanceFlow.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repo;

        public EmployeeService(IEmployeeRepository repo)
        {
            _repo = repo;
        }

        public async Task<string> AddAsync(EmployeeRequestDto dto)
        {
            var employee = new Employee
            {
                EmployeeName = dto.EmployeeName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Password = dto.Password,
                DepartmentId = dto.DepartmentId,
                Role = dto.Role,
                IsActive = true
            };

            await _repo.AddAsync(employee);
            await _repo.SaveAsync();

            return "Employee added successfully";
        }

        public async Task<List<EmployeeResponseDto>> GetAllAsync()
        {
            var data = await _repo.GetAllAsync();

            return data.Select(e => new EmployeeResponseDto
            {
                EmployeeId = e.EmployeeId,
                EmployeeName = e.EmployeeName,
                Email = e.Email,
                PhoneNumber = e.PhoneNumber,
                DepartmentName = e.Department?.DepartmentName ?? string.Empty,
                Role = e.Role,
                IsActive = e.IsActive
            }).ToList();
        }

        public async Task<EmployeeResponseDto?> GetByIdAsync(int id)
        {
            var employee = await _repo.GetByIdAsync(id);

            if (employee == null)
                return null;

            return new EmployeeResponseDto
            {
                EmployeeId = employee.EmployeeId,
                EmployeeName = employee.EmployeeName,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                DepartmentName = employee.Department?.DepartmentName ?? string.Empty,
                Role = employee.Role,
                IsActive = employee.IsActive
            };
        }

        public async Task<string> UpdateEmployeeAsync(int id, EmployeeRequestDto dto)
        {
            var emp = await _repo.GetByIdAsync(id);

            if (emp == null)
                return "Employee not found";

            if (!emp.IsActive)
                return "Inactive employee cannot be edited";

            emp.EmployeeName = dto.EmployeeName;
            emp.Email = dto.Email;
            emp.PhoneNumber = dto.PhoneNumber;
            emp.Password = dto.Password;
            emp.DepartmentId = dto.DepartmentId;
            emp.Role = dto.Role;

            _repo.Update(emp);
            await _repo.SaveAsync();

            return "Employee updated successfully";
        }

        public async Task<string> DeleteEmployeeAsync(int id)
        {
            var emp = await _repo.GetByIdAsync(id);

            if (emp == null)
                return "Employee not found";

            emp.IsActive = false;

            _repo.Update(emp);
            await _repo.SaveAsync();

            return "Employee deactivated successfully";
        }
    }
}