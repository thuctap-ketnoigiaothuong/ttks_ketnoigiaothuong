using AutoMapper;
using dotnet9_ketnoigiaothuong.Domain.Contracts;
using dotnet9_ketnoigiaothuong.Domain.Entities;
using dotnet9_ketnoigiaothuong.Infrastructure.Context;
using static dotnet9_ketnoigiaothuong.Domain.Contracts.QuotationRequestContract;

namespace dotnet9_ketnoigiaothuong.Services
{
    public class QuotationRequestService : BaseRepository, IQuotationRequestService
    {
        public QuotationRequestService(AppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<ApiResponse<ReponseQuotationRequest>> CreateQuotationRequest(CreateQuotationRequest request)
        {
            var quotationRequest = mapper.Map<QuotationRequest>(request);
            quotationRequest.CreatedDate = DateTime.Now;

            await context.QuotationRequests.AddAsync(quotationRequest);
            await context.SaveChangesAsync();

            return new ApiResponse<ReponseQuotationRequest>
            {
                Data = mapper.Map<ReponseQuotationRequest>(quotationRequest),
                Message = "Quotation request created successfully",
                IsSuccess = true
            };
        }
    }
}
