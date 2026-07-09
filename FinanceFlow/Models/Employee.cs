using System.ComponentModel.DataAnnotations;

namespace FinanceFlow.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Employee name is required")]
        [StringLength(50, MinimumLength = 3)]
        public string EmployeeName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Phone number must contain exactly 10 digits")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d).{8,15}$",
            ErrorMessage = "Password must be 8-15 characters with uppercase, lowercase and a number")]
        public string Password { get; set; } = string.Empty;

        [Required]
        public int DepartmentId { get; set; }

        public Department? Department { get; set; }

        [Required]
        public UserRole Role { get; set; } = UserRole.Employee;

        public bool IsActive { get; set; } = true;

        public ICollection<ExpenseClaim> ExpenseClaims { get; set; } = new List<ExpenseClaim>();
    }
}