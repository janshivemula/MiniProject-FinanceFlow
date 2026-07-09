using FinanceFlow.Models;

namespace FinanceFlow.Models
{
    using global::FinanceFlow.Models;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ExpenseClaim
    {
        [Key]
        public int Id { get; set; }
        //fk
        public int EmployeeId { get; set; }
        
        public Employee Employee { get; set; }
       
        public int? ApprovedByEmployeeId { get; set; }
       
        public Employee? ApprovedByEmployee { get; set; }
      
        public DateTime? ApprovedDate { get; set; }

        // FK Department
        [Required]
        public int DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public Department? Department { get; set; }

        // FK  Category
        [Required]
        public int ExpenseCategoryId { get; set; }

        [ForeignKey("ExpenseCategoryId")]
        public ExpenseCategory? ExpenseCategory { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public decimal Amount { get; set; }

        [Required]
        public DateTime ExpenseDate { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 10)]
        public string Description { get; set; } = string.Empty;


        
        [Required]
        public ExpenseStatus Status { get; set; } = ExpenseStatus.Pending;

        public string? ReviewRemark { get; set; }
    }
}
