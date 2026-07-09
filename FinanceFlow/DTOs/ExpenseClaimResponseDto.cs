namespace FinanceFlow.DTOs
{
    public class ExpenseClaimResponseDto
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;

        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;

        public int ExpenseCategoryId { get; set; }
        public string ExpenseCategoryName { get; set; } = string.Empty;

        public decimal Amount { get; set; }

        public DateTime ExpenseDate { get; set; }

        public string Description { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public string? ReviewRemark { get; set; }

        public string? ApprovedBy { get; set; }

        public DateTime? ApprovedDate { get; set; }
    }
}