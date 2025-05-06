using AutoMapper;
using dotnet9_ketnoigiaothuong.Infrastructure.Context;

namespace dotnet9_ketnoigiaothuong.Services
{
    public abstract class BaseProvider : IDisposable
    {
        AppDbContext? context;
        IMapper? mapper;
        IConfiguration? _configuration;
        IHttpContextAccessor? _httpContextAccessor;
        protected BaseProvider(IHttpContextAccessor? httpContextAccessor) => _httpContextAccessor = httpContextAccessor;

        protected AppDbContext Context => context ??= _httpContextAccessor!.HttpContext!.RequestServices.GetRequiredService<AppDbContext>();
        protected IMapper Mapper => mapper ??= _httpContextAccessor!.HttpContext!.RequestServices.GetRequiredService<IMapper>();
        protected IConfiguration Configuration => _configuration ??= _httpContextAccessor!.HttpContext!.RequestServices.GetRequiredService<IConfiguration>();

        public void Dispose()
        {
            if (context != null)
            {
                context.Dispose();
                context = null;
            }
        }
    }
}
