namespace FinanceFlow.DTOs
{
    using System.ComponentModel.DataAnnotations;

    public class ExpenseCategoryRequestDto
    {
        [Required(ErrorMessage = "Category name is required")]
        public string CategoryName { get; set; } = string.Empty;
    }
}
