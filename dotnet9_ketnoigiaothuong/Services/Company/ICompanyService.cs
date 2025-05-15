using dotnet9_ketnoigiaothuong.Domain.Contracts;
using static dotnet9_ketnoigiaothuong.Domain.Contracts.CompanyContract;

namespace dotnet9_ketnoigiaothuong.Services.Company
{
    public interface ICompanyService
    {
        Task<ApiResponse<ResponseCompany>> CreateCompanyProfileAsync(CompanyViewModel model);
        Task<ApiResponse<FullResponseCompany>> Async_GetCompanyProfileByEmailAndPhoneNumber(string email, string phoneNumber);
        Task<ApiResponse<List<CompanyListItem>>> GetAllCompaniesAsync();
        Task<ApiResponse<FullResponseCompany>> GetCompanyByIdAsync(int id);
        Task<ApiResponse<FullResponseCompany>> CreateCompanyAsync(CreateCompanyModel model);
        Task<ApiResponse<FullResponseCompany>> UpdateCompanyAsync(int id, UpdateCompanyModel model);
        Task<ApiResponse<bool>> DeleteCompanyAsync(int id);
    }
}
