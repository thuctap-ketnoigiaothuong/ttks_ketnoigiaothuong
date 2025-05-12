using AutoMapper;
using dotnet9_ketnoigiaothuong.Domain.Contracts;
using dotnet9_ketnoigiaothuong.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using static dotnet9_ketnoigiaothuong.Domain.Contracts.CompanyContract;
using static dotnet9_ketnoigiaothuong.Domain.Contracts.UserContract;

namespace dotnet9_ketnoigiaothuong.Services.User
{
    public class UserService : BaseRepository, IUserService
    {
        public UserService(AppDbContext context, IMapper mapper) : base(context, mapper) { }
        public async Task<ApiResponse<ResponseUser>> UpdateUserAsync(int user_id, UserUpdate model)
        {
            var existing_user = await context.UserAccounts.FirstOrDefaultAsync(x => x.UserID == user_id);
            if (existing_user == null)
            {
                return new ApiResponse<ResponseUser>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "User not found!"
                };
            }
            else
            {
                if (model.CompanyID != null || model.CompanyID != 0)
                    existing_user.CompanyID = model.CompanyID;

                if (!string.IsNullOrEmpty(model.FullName))
                    existing_user.FullName = model.FullName;

                if (!string.IsNullOrEmpty(model.Email))
                    existing_user.Email = model.Email;

                if (!string.IsNullOrEmpty(model.Role))
                    existing_user.Role = model.Role;

                if (!string.IsNullOrEmpty(model.Status))
                    existing_user.Status = model.Status;

                await context.SaveChangesAsync();
                return new ApiResponse<ResponseUser>
                {
                    Data = mapper.Map<ResponseUser>(existing_user),
                    IsSuccess = true,
                    Message = "Updated user successfully"
                };
            }
        }
    }
}
