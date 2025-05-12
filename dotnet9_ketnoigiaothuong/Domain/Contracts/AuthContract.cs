namespace dotnet9_ketnoigiaothuong.Domain.Contracts
{
    public class AuthContract
    {
        public record LoginViewModel(string Email, string Password);
        public record RegisterViewModel(string FullName, string Email, string Password, string ConfirmPassword);
        public record ResponseUserAccount(string FullName, string Email, string PasswordHash, string Role, string Status);
        public record FullResponseUserAccount(int UserID, string FullName, string Email, string Role, string Status);
    }

}
