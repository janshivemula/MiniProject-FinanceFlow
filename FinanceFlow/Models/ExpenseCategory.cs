namespace FinanceFlow.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ExpenseCategory
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        [StringLength(50, MinimumLength = 3)]
        public string CategoryName { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true; 
        // Navigation
        public List<ExpenseClaim>? ExpenseClaims { get; set; }
    }
}
