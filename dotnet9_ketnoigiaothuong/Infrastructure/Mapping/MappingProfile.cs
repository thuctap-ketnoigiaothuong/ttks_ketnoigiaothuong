using AutoMapper;
using dotnet9_ketnoigiaothuong.Domain.Entities;
using static dotnet9_ketnoigiaothuong.Domain.Contracts.AuthContract;
using static dotnet9_ketnoigiaothuong.Domain.Contracts.CompanyContract;
using static dotnet9_ketnoigiaothuong.Domain.Contracts.UserContract;
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
            CreateMap<UserAccount, FullResponseUserAccount>();

            CreateMap<UserAccount, ResponseUser>();

            CreateMap<Company, ResponseCompany>(); 
            CreateMap<Company, FullResponseCompany>();
            
            CreateMap<CreateCompanyModel, Company>()
                .ForMember(dest => dest.RegistrationDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.VerificationStatus, opt => opt.MapFrom(src => "Pending"));
            
            CreateMap<UpdateCompanyModel, Company>();
            
            CreateMap<Company, CompanyListItem>();
            
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
