using System.ComponentModel.DataAnnotations;
using FinanceFlow.Models;

namespace FinanceFlow.DTOs
{
    public class EmployeeRequestDto
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string EmployeeName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^[0-9]{10}$")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d).{8,15}$")]
        public string Password { get; set; } = string.Empty;

        [Required]
        public int DepartmentId { get; set; }

        [Required]
        public UserRole Role { get; set; }
    }
}