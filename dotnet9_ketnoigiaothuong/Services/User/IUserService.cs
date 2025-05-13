using dotnet9_ketnoigiaothuong.Domain.Contracts;
using static dotnet9_ketnoigiaothuong.Domain.Contracts.UserContract;

namespace dotnet9_ketnoigiaothuong.Services.User
{
    public interface IUserService
    {
        Task<ApiResponse<ResponseUser>> UpdateUserAsync(int user_id, UserUpdate model);
    }
}
