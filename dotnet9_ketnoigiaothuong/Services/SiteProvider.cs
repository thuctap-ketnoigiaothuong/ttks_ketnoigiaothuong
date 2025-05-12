using dotnet9_ketnoigiaothuong.Services.User;

namespace dotnet9_ketnoigiaothuong.Services
{
    public class SiteProvider : BaseProvider
    {
        public SiteProvider(IHttpContextAccessor? httpContextAccessor) : base(httpContextAccessor)
        {
        }

        DbInitializer? db;
        public DbInitializer Db => db ??= new DbInitializer(Context, Mapper);

        TokenService? tokenService;
        public TokenService TokenService => tokenService ??= new TokenService(Configuration);

        AuthService? authService;
        CompanyService? companyService;
        UserService? userService;
        public AuthService AuthService => authService ??= new AuthService(Context, Mapper, TokenService);
        public CompanyService CompanyService => companyService ??= new CompanyService(Context, Mapper);
        public UserService UserService => userService ??= new UserService(Context, Mapper);
    }
}
