namespace FinanceFlow.DTOs
{
    public class ExpenseCategoryResponseDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}