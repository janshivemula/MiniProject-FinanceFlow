namespace FinanceFlow.DTOs
{
    public class DepartmentBudgetResponseDto
    {
        public int Id { get; set; }

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; } = string.Empty;

        public int Month { get; set; }

        public int Year { get; set; }

        public decimal BudgetAmount { get; set; }

        public bool IsActive { get; set; }
    }
}