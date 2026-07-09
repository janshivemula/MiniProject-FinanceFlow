using FinanceFlow.DTOs;
using FinanceFlow.Models;
using FinanceFlow.Repositories;
using System.Data;

namespace FinanceFlow.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseClaimRepository _expenseRepo;
        private readonly IDepartmentBudgetRepository _budgetRepo;
        private readonly IEmployeeRepository _employeeRepo;

        public ExpenseService(
                IExpenseClaimRepository expenseRepo,
                IDepartmentBudgetRepository budgetRepo,
                IEmployeeRepository employeeRepo)
        {
            _expenseRepo = expenseRepo;
            _budgetRepo = budgetRepo;
            _employeeRepo = employeeRepo;
        }

        // CREATE
        public async Task<string> AddExpenseAsync(ExpenseClaimRequestDto dto)
        {
            var employee = await _employeeRepo.GetByIdAsync(dto.EmployeeId);

            if (employee == null)
                return "Employee not found";

            var claim = new ExpenseClaim
            {
                EmployeeId = dto.EmployeeId,
                DepartmentId = employee.DepartmentId,
                ExpenseCategoryId = dto.ExpenseCategoryId,
                Amount = dto.Amount,
                ExpenseDate = dto.ExpenseDate,
                Description = dto.Description,
                Status = ExpenseStatus.Pending
            };

            await _expenseRepo.AddAsync(claim);
            await _expenseRepo.SaveAsync();

            return "Expense claim submitted successfully";
        }

        // GET ALL
        public async Task<List<ExpenseClaimResponseDto>> GetAllAsync()
        {
            var claims = await _expenseRepo.GetAllAsync();

            return claims.Select(x => new ExpenseClaimResponseDto
            {
                Id = x.Id,

                EmployeeId = x.EmployeeId,
                EmployeeName = x.Employee?.EmployeeName ?? string.Empty,

                DepartmentId = x.DepartmentId,
                DepartmentName = x.Department?.DepartmentName ?? string.Empty,

                ExpenseCategoryId = x.ExpenseCategoryId,
                ExpenseCategoryName = x.ExpenseCategory?.CategoryName ?? string.Empty,

                Amount = x.Amount,
                ExpenseDate = x.ExpenseDate,
                Description = x.Description,
                Status = x.Status.ToString(),
                ReviewRemark = x.ReviewRemark,
                ApprovedBy = x.ApprovedByEmployee?.EmployeeName,
                ApprovedDate = x.ApprovedDate
            }).ToList();
        }
        // GET BY ID
        public async Task<ExpenseClaimResponseDto?> GetByIdAsync(int id)
        {
            var x = await _expenseRepo.GetByIdAsync(id);

            if (x == null)
                return null;

            return new ExpenseClaimResponseDto
            {
                Id = x.Id,

                EmployeeId = x.EmployeeId,
                EmployeeName = x.Employee?.EmployeeName ?? string.Empty,

                DepartmentId = x.DepartmentId,
                DepartmentName = x.Department?.DepartmentName ?? string.Empty,

                ExpenseCategoryId = x.ExpenseCategoryId,
                ExpenseCategoryName = x.ExpenseCategory?.CategoryName ?? string.Empty,

                Amount = x.Amount,
                ExpenseDate = x.ExpenseDate,
                Description = x.Description,
                Status = x.Status.ToString(),
                ReviewRemark = x.ReviewRemark,
                ApprovedBy = x.ApprovedByEmployee?.EmployeeName,
                ApprovedDate = x.ApprovedDate
            };
        }

        public async Task<string> UpdateExpenseAsync(int id, ExpenseClaimRequestDto dto)
        {
            var claim = await _expenseRepo.GetByIdAsync(id);

            if (claim == null)
                return "Expense not found";

            if (claim.Status != ExpenseStatus.Pending)
                return "Only pending expenses can be updated";

            var employee = await _employeeRepo.GetByIdAsync(dto.EmployeeId);

            if (employee == null)
                return "Employee not found";

            claim.EmployeeId = dto.EmployeeId;
            claim.DepartmentId = employee.DepartmentId;
            claim.ExpenseCategoryId = dto.ExpenseCategoryId;
            claim.Amount = dto.Amount;
            claim.ExpenseDate = dto.ExpenseDate;
            claim.Description = dto.Description;

            _expenseRepo.Update(claim);
            await _expenseRepo.SaveAsync();

            return "Expense updated successfully";
        }

        // DELETE (ONLY PENDING)
        public async Task<string> DeleteExpenseAsync(int id)
        {
            var claim = await _expenseRepo.GetByIdAsync(id);

            if (claim == null)
                return "Expense not found";

            if (claim.Status != ExpenseStatus.Pending)
                return "Only pending expenses can be deleted";

            _expenseRepo.Delete(claim);
            await _expenseRepo.SaveAsync();

            return "Expense deleted successfully";
        }

        // APPROVE
        public async Task<string> ApproveClaimAsync(int claimId, int employeeId, string remark)
        {
            var claim = await _expenseRepo.GetByIdAsync(claimId);

            var employee = await _employeeRepo.GetByIdAsync(employeeId);

            if (employee == null)
                return "Employee not found";

            if (employee.Role != UserRole.FinanceManager)
                return "Only Finance Managers can approve claims";

            if (claim == null)
                return "Claim not found";

            if (claim.Status != ExpenseStatus.Pending)
                return "Only pending claims can be approved";

            var budget = await _budgetRepo.GetBudgetAsync(
                claim.DepartmentId,
                claim.ExpenseDate.Month,
                claim.ExpenseDate.Year
            );

            if (budget == null)
                return "Budget not defined";

            var allClaims = await _expenseRepo.GetAllAsync();

            var approvedTotal = allClaims
                .Where(x =>
                    x.DepartmentId == claim.DepartmentId &&
                    x.ExpenseDate.Month == claim.ExpenseDate.Month &&
                    x.ExpenseDate.Year == claim.ExpenseDate.Year &&
                    x.Status == ExpenseStatus.Approved)
                .Sum(x => x.Amount);

            if (approvedTotal + claim.Amount > budget.BudgetAmount)
                return "Budget exceeded";

            claim.Status = ExpenseStatus.Approved;
            claim.ReviewRemark = remark;

            claim.ApprovedByEmployeeId = employeeId;

            claim.ApprovedDate = DateTime.Now;

            _expenseRepo.Update(claim);

            await _expenseRepo.SaveAsync();
            

            return "Claim approved successfully";
        }

        // REJECT
        public async Task<string> RejectClaimAsync(int claimId, int employeeId, string remark)
        {
            var claim = await _expenseRepo.GetByIdAsync(claimId);

            var employee = await _employeeRepo.GetByIdAsync(employeeId);

            if (employee == null)
                return "Employee not found";

            if (employee.Role != UserRole.FinanceManager)
                return "Only Finance Managers can reject claims";

            if (claim == null)
                return "Claim not found";

            if (claim.Status != ExpenseStatus.Pending)
                return "Only pending claims can be rejected";

            
            claim.Status = ExpenseStatus.Rejected;

            claim.ReviewRemark = remark;

            claim.ApprovedByEmployeeId = employeeId;

            claim.ApprovedDate = DateTime.Now;

            _expenseRepo.Update(claim);

            await _expenseRepo.SaveAsync();

            

            return "Claim rejected successfully";
        }
    }
}