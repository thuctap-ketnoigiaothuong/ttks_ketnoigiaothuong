using dotnet9_ketnoigiaothuong.Infrastructure.Services;

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
        public AuthService AuthService => authService ??= new AuthService(Context, Mapper, TokenService);

        ProductService? productService;
        public ProductService ProductService => productService ??= new ProductService(Context, Mapper);
    }
}
