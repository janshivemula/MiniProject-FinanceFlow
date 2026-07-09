namespace FinanceFlow.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class DepartmentBudget
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public Department? Department { get; set; }

        [Required]
        [Range(1, 12, ErrorMessage = "Month must be between  1 and 12")]
        public int Month { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        [Range(1, double.MaxValue)]
        public decimal BudgetAmount { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
