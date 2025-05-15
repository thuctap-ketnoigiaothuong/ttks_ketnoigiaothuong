using dotnet9_ketnoigiaothuong.Domain.Contracts;
using static dotnet9_ketnoigiaothuong.Domain.Contracts.QuotationResponseContract;

namespace dotnet9_ketnoigiaothuong.Services
{
    public interface IQuotationResponseService
    {
        Task<ApiResponse<ReponseQuotationResponse>> CreateQuotationResponse(CreateQuotationResponse request);
        Task<ApiResponse<ReponseQuotationResponse>> UpdateQuotationResponse(int responseId, UpdateQuotationResponse request);
    }
}
