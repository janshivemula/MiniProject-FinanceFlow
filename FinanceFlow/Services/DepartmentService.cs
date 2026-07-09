using FinanceFlow.DTOs;
using FinanceFlow.Models;
using FinanceFlow.Repositories;

namespace FinanceFlow.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _repo;

        public DepartmentService(IDepartmentRepository repo)
        {
            _repo = repo;
        }

        // Department LIST -> all departments
        public async Task<List<DepartmentResponseDto>> GetAllDepartmentsAsync()
        {
            var departments = await _repo.GetAllAsync();

            return departments.Select(d => new DepartmentResponseDto
            {
                Id = d.Id,
                DepartmentName = d.DepartmentName,
                IsActive = d.IsActive
            }).ToList();
        }

        // Dropdown -> only active
        public async Task<List<DepartmentResponseDto>> GetActiveDepartmentsAsync()
        {
            var departments = await _repo.GetActiveAsync();

            return departments.Select(d => new DepartmentResponseDto
            {
                Id = d.Id,
                DepartmentName = d.DepartmentName,
                IsActive = d.IsActive
            }).ToList();
        }

        // Edit -> active only
        public async Task<DepartmentResponseDto?> GetDepartmentByIdAsync(int id)
        {
            var dept = await _repo.GetByIdAsync(id);

            if (dept == null)
                return null;

            return new DepartmentResponseDto
            {
                Id = dept.Id,
                DepartmentName = dept.DepartmentName,
                IsActive = dept.IsActive
            };
        }

        public async Task<string> CreateDepartmentAsync(DepartmentRequestDto dto)
        {
            var dept = new Department
            {
                DepartmentName = dto.DepartmentName,
                IsActive = true
            };

            await _repo.AddAsync(dept);
            await _repo.SaveAsync();

            return "Department created successfully";
        }

        public async Task<string> UpdateDepartmentAsync(int id, DepartmentRequestDto dto)
        {
            var dept = await _repo.GetByIdAsync(id);

            if (dept == null)
                return "Department not found or inactive";

            dept.DepartmentName = dto.DepartmentName;

            _repo.Update(dept);
            await _repo.SaveAsync();

            return "Department updated successfully";
        }

        public async Task<string> DeleteDepartmentAsync(int id)
        {
            var dept = await _repo.GetByIdAsync(id);

            if (dept == null)
                return "Department not found or already inactive";

            dept.IsActive = false;

            _repo.Update(dept);
            await _repo.SaveAsync();

            return "Department deactivated successfully";
        }
    }
}