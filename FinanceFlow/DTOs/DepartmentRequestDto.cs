namespace FinanceFlow.DTOs
{
    using System.ComponentModel.DataAnnotations;

    public class DepartmentRequestDto
    {
        [Required(ErrorMessage = "Department name is required")]
        public string DepartmentName { get; set; } = string.Empty;
    }
}
