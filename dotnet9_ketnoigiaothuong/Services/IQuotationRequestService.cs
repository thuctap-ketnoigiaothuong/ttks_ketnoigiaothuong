using dotnet9_ketnoigiaothuong.Domain.Contracts;
using static dotnet9_ketnoigiaothuong.Domain.Contracts.QuotationRequestContract;

namespace dotnet9_ketnoigiaothuong.Services
{
    public interface IQuotationRequestService
    {
        Task<ApiResponse<ReponseQuotationRequest>> CreateQuotationRequest(CreateQuotationRequest request);
    }
}
