
namespace dotnet9_ketnoigiaothuong.Domain.Contracts
{
    public class UserContract
    {
        public record ResponseUser(int? UserID, int? CompanyID, string? FullName, string? Email, string? Role, string? Status);
        public record UserUpdate(int? CompanyID, string? FullName, string? Email, string? Role, string? Status);
    }
}