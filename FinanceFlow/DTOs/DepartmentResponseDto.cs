namespace FinanceFlow.DTOs
{
    public class DepartmentResponseDto
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}