using FinanceFlow.Models;

namespace FinanceFlow.DTOs
{
    public class EmployeeResponseDto
    {
        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string DepartmentName { get; set; } = string.Empty;

        public UserRole Role { get; set; }

        public bool IsActive { get; set; }
    }
}