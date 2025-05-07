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

        QuotationRequestService? quotationRequestService;
        public QuotationRequestService QuotationRequestService => quotationRequestService ??= new QuotationRequestService(Context, Mapper);

        QuotationResponseService? quotationResponseService;
        public QuotationResponseService QuotationResponseService => quotationResponseService ??= new QuotationResponseService(Context, Mapper);
    }
}
