using AutoMapper;
using dotnet9_ketnoigiaothuong.Domain.Entities;
using static dotnet9_ketnoigiaothuong.Domain.Contracts.AuthContract;
using static dotnet9_ketnoigiaothuong.Domain.Contracts.QuotationRequestContract;
using static dotnet9_ketnoigiaothuong.Domain.Contracts.QuotationResponseContract;

namespace dotnet9_ketnoigiaothuong.Infrastructure.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LoginViewModel, ResponseUserAccount>();
            CreateMap<RegisterViewModel, ResponseUserAccount>();
            CreateMap<UserAccount, ResponseUserAccount>();
            #region QuotationRequest
            CreateMap<CreateQuotationRequest, QuotationRequest>();
            CreateMap<QuotationRequest, ReponseQuotationRequest>();
            #endregion
            #region QuotationResponse
            CreateMap<CreateQuotationResponse, QuotationResponse>();
            CreateMap<UpdateQuotationResponse, QuotationResponse>();
            CreateMap<QuotationResponse, ReponseQuotationResponse>();
            #endregion

        }
    }
}
