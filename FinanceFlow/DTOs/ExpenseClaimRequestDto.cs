using System.ComponentModel.DataAnnotations;

namespace FinanceFlow.DTOs
{
    public class ExpenseClaimRequestDto
    {
        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public int ExpenseCategoryId { get; set; }

        [Range(1, double.MaxValue)]
        public decimal Amount { get; set; }

        [Required]
        public DateTime ExpenseDate { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 10)]
        public string Description { get; set; } = string.Empty;
    }
}