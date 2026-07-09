namespace FinanceFlow.DTOs
{
    public class LoginResponseDto
    {
        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public string DepartmentName { get; set; } = string.Empty;
    }
}