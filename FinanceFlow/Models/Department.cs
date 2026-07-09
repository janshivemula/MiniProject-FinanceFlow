namespace FinanceFlow.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Department name is required")]
        [StringLength(50, MinimumLength = 2)]
        public string DepartmentName { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;



        // Navigation
        public List<ExpenseClaim>? ExpenseClaims { get; set; }
        public List<DepartmentBudget>? Budgets { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
