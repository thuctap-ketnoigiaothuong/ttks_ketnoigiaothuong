using dotnet9_ketnoigiaothuong.Domain.Contracts;
using static dotnet9_ketnoigiaothuong.Domain.Contracts.AuthContract;

namespace dotnet9_ketnoigiaothuong.Services
{
    public interface IAuthService
    {
        Task<ApiResponse<ResponseUserAccount>> RegisterAsync(RegisterViewModel model);
        Task<ApiResponse<string>> LoginAsync(LoginViewModel model);
        Task<ApiResponse<FullResponseUserAccount>> Me(string email);
    }
}
