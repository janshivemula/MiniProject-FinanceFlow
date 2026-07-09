using FinanceFlow.DTOs;

namespace FinanceFlow.Services
{
    public interface ILoginService
    {
        Task<LoginResponseDto?> LoginAsync(LoginRequestDto dto);
    }
}