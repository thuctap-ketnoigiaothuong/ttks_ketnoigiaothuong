using AutoMapper;
using dotnet9_ketnoigiaothuong.Domain.Contracts;
using dotnet9_ketnoigiaothuong.Domain.Entities;
using dotnet9_ketnoigiaothuong.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using static dotnet9_ketnoigiaothuong.Domain.Contracts.AuthContract;

namespace dotnet9_ketnoigiaothuong.Services
{
    public class AuthService : BaseRepository, IAuthService
    {
        private readonly ITokenService _tokenService;

        public AuthService(AppDbContext context, IMapper mapper, ITokenService tokenService) : base(context, mapper)
        {
            _tokenService = tokenService;
        }

        public async Task<ApiResponse<ResponseUserAccount>> RegisterAsync(RegisterViewModel model)
        {
            var existUser = await context.UserAccounts
                .FirstOrDefaultAsync(x => x.Email == model.Email);
            if (existUser != null)
            {
                return new ApiResponse<ResponseUserAccount>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "Email already exists"
                };
            }
            var user = new UserAccount
            {
                FullName = model.FullName,
                Email = model.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
                Role = "Company",
                Status = null
            };
            await context.UserAccounts.AddAsync(user);
            await context.SaveChangesAsync();
            return new ApiResponse<ResponseUserAccount>
            {
                Data = mapper.Map<ResponseUserAccount>(user),
                IsSuccess = true,
                Message = "User registered successfully"
            };
        }

        public async Task<ApiResponse<string>> LoginAsync(LoginViewModel model)
        {
            var user = await context.UserAccounts
                .FirstOrDefaultAsync(x => x.Email == model.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            {
                return new ApiResponse<string>
                {
                    Data = string.Empty,
                    IsSuccess = false,
                    Message = "Invalid credentials"
                };
            }
            var token = _tokenService.GenerateJwtToken(user);
            return new ApiResponse<string> {
                Data = token,
                IsSuccess = true,
                Message = "Login successful"
            };
        }

        public async Task<ApiResponse<ResponseUserAccount>> Me(string email)
        {
            var user = await context.UserAccounts
                .FirstOrDefaultAsync(x => x.Email == email);
            if (user == null)
            {
                return new ApiResponse<ResponseUserAccount>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "User not found"
                };
            }
            return new ApiResponse<ResponseUserAccount>
            {
                Data = mapper.Map<ResponseUserAccount>(user),
                IsSuccess = true,
                Message = "User found"
            };
        }
    }
}
