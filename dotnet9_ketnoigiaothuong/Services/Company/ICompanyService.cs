using dotnet9_ketnoigiaothuong.Domain.Contracts;
using static dotnet9_ketnoigiaothuong.Domain.Contracts.CompanyContract;

namespace dotnet9_ketnoigiaothuong.Services.Company
{
    public interface ICompanyService
    {
        Task<ApiResponse<ResponseCompany>> CreateCompanyProfileAsync(CompanyViewModel model);
        Task<ApiResponse<FullResponseCompany>> Async_GetCompanyProfileByEmailAndPhoneNumber(string email, string phoneNumber);
    }
}
