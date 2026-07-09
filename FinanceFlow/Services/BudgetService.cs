using FinanceFlow.DTOs;
using FinanceFlow.Models;
using FinanceFlow.Repositories;

namespace FinanceFlow.Services
{
    public class BudgetService : IBudgetService
    {
        private readonly IDepartmentBudgetRepository _budgetRepo;

        public BudgetService(IDepartmentBudgetRepository budgetRepo)
        {
            _budgetRepo = budgetRepo;
        }

        public async Task<string> AddOrUpdateBudgetAsync(DepartmentBudgetRequestDto dto)
        {
            var existing = await _budgetRepo.GetBudgetAsync(dto.DepartmentId, dto.Month, dto.Year);

            if (existing != null)
            {
                if (!existing.IsActive)
                    return "Inactive budget cannot be updated";

                existing.BudgetAmount = dto.BudgetAmount;
                existing.DepartmentId = dto.DepartmentId;
                existing.Month = dto.Month;
                existing.Year = dto.Year;

                _budgetRepo.Update(existing);
                await _budgetRepo.SaveAsync();
                return "Budget updated successfully";
            }

            var budget = new DepartmentBudget
            {
                DepartmentId = dto.DepartmentId,
                Month = dto.Month,
                Year = dto.Year,
                BudgetAmount = dto.BudgetAmount,
                IsActive = true
            };

            await _budgetRepo.AddAsync(budget);
            await _budgetRepo.SaveAsync();

            return "Budget created successfully";
        }

        public async Task<DepartmentBudgetResponseDto?> GetBudgetAsync(int departmentId, int month, int year)
        {
            var budget = await _budgetRepo.GetBudgetAsync(departmentId, month, year);

            if (budget == null)
                return null;

            return new DepartmentBudgetResponseDto
            {
                Id = budget.Id,
                DepartmentId = budget.DepartmentId,
                DepartmentName = budget.Department?.DepartmentName ?? string.Empty,
                Month = budget.Month,
                Year = budget.Year,
                BudgetAmount = budget.BudgetAmount,
                IsActive = budget.IsActive
            };
        }

        public async Task<List<DepartmentBudgetResponseDto>> GetAllAsync()
        {
            var budgets = await _budgetRepo.GetAllAsync();

            return budgets.Select(b => new DepartmentBudgetResponseDto
            {
                Id = b.Id,
                DepartmentId = b.DepartmentId,
                DepartmentName = b.Department?.DepartmentName ?? string.Empty,
                Month = b.Month,
                Year = b.Year,
                BudgetAmount = b.BudgetAmount,
                IsActive = b.IsActive
            }).ToList();
        }

        public async Task<string> DeleteBudgetAsync(int id)
        {
            var budget = await _budgetRepo.GetByIdAsync(id);

            if (budget == null)
                return "Budget not found";

            if (!budget.IsActive)
                return "Budget is already inactive";

            budget.IsActive = false;

            _budgetRepo.Update(budget);
            await _budgetRepo.SaveAsync();

            return "Budget deactivated successfully";
        }
    }
}