using dotnet9_ketnoigiaothuong.Services;
using Microsoft.AspNetCore.Mvc;

namespace dotnet9_ketnoigiaothuong.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        SiteProvider? provider;

        protected SiteProvider Provider => provider ?? new SiteProvider(HttpContext.RequestServices.GetRequiredService<IHttpContextAccessor>());
    }
}
