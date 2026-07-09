namespace FinanceFlow.DTOs
{
    using System.ComponentModel.DataAnnotations;

    public class DepartmentBudgetRequestDto
    {
        [Required]
        public int DepartmentId { get; set; }

        [Required]
        [Range(1, 12, ErrorMessage = "Month must be between 1 and 12")]
        public int Month { get; set; }

        [Required]
        public int Year { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Budget must be greater than 0")]
        public decimal BudgetAmount { get; set; }
    }
}
