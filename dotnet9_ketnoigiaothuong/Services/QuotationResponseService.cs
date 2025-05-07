using AutoMapper;
using dotnet9_ketnoigiaothuong.Domain.Contracts;
using dotnet9_ketnoigiaothuong.Domain.Entities;
using dotnet9_ketnoigiaothuong.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using static dotnet9_ketnoigiaothuong.Domain.Contracts.QuotationResponseContract;

namespace dotnet9_ketnoigiaothuong.Services
{
    public class QuotationResponseService : BaseRepository, IQuotationResponseService
    {
        public QuotationResponseService(AppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<ApiResponse<ReponseQuotationResponse>> CreateQuotationResponse(CreateQuotationResponse request)
        {
            var quotationResponse = mapper.Map<QuotationResponse>(request);
            var quotationRequest = await context.QuotationRequests.FirstOrDefaultAsync(x => x.RequestID == request.RequestID);
            if (quotationRequest == null)
            {
                return new ApiResponse<ReponseQuotationResponse>
                {
                    Data = null,
                    Message = "Quotation request not found",
                    IsSuccess = false
                };
            }
            quotationResponse.BuyerCompanyID = quotationRequest.BuyerCompanyID;
            quotationResponse.SellerCompanyID = quotationRequest.SellerCompanyID;
            quotationResponse.CreatedDate = DateTime.Now;

            await context.QuotationResponses.AddAsync(quotationResponse);
            await context.SaveChangesAsync();

            return new ApiResponse<ReponseQuotationResponse>
            {
                Data = mapper.Map<ReponseQuotationResponse>(quotationResponse),
                Message = "Quotation response created successfully",
                IsSuccess = true
            };
        }

        public async Task<ApiResponse<ReponseQuotationResponse>> UpdateQuotationResponse(int responseId, UpdateQuotationResponse request)
        {
            var quotationResponse = await context.QuotationResponses.FirstOrDefaultAsync(x => x.ResponseID == responseId);
            if (quotationResponse == null)
            {
                return new ApiResponse<ReponseQuotationResponse>
                {
                    Data = null,
                    Message = "Quotation response not found",
                    IsSuccess = false
                };
            }

            if (request.ProposedPrice > 0)
                quotationResponse.ProposedPrice = request.ProposedPrice;
            if (!string.IsNullOrEmpty(request.DeliveryTime))
                quotationResponse.DeliveryTime = request.DeliveryTime;
            if (!string.IsNullOrEmpty(request.ShippingMethod))
                quotationResponse.ShippingMethod = request.ShippingMethod;
            if (!string.IsNullOrEmpty(request.Terms))
                quotationResponse.Terms = request.Terms;

            await context.SaveChangesAsync();

            return new ApiResponse<ReponseQuotationResponse>
            {
                Data = mapper.Map<ReponseQuotationResponse>(quotationResponse),
                Message = "Quotation response updated successfully",
                IsSuccess = true
            };
        }
    }
}
