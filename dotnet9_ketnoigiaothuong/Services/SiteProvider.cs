
﻿using dotnet9_ketnoigiaothuong.Infrastructure.Services;
﻿using dotnet9_ketnoigiaothuong.Services.User;

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


        ProductService? productService;
        public ProductService ProductService => productService ??= new ProductService(Context, Mapper);
        public CompanyService CompanyService => companyService ??= new CompanyService(Context, Mapper);
        public UserService UserService => userService ??= new UserService(Context, Mapper);

        QuotationRequestService? quotationRequestService;
        public QuotationRequestService QuotationRequestService => quotationRequestService ??= new QuotationRequestService(Context, Mapper);

        QuotationResponseService? quotationResponseService;
        public QuotationResponseService QuotationResponseService => quotationResponseService ??= new QuotationResponseService(Context, Mapper);

    }
}
