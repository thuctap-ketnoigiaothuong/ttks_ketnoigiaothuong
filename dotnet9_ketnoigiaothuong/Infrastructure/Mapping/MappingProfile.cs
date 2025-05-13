using AutoMapper;
using dotnet9_ketnoigiaothuong.Domain.Entities;
using static dotnet9_ketnoigiaothuong.Domain.Contracts.AuthContract;
using static dotnet9_ketnoigiaothuong.Domain.Contracts.CompanyContract;
using static dotnet9_ketnoigiaothuong.Domain.Contracts.UserContract;
using static dotnet9_ketnoigiaothuong.Domain.Contracts.QuotationRequestContract;
using static dotnet9_ketnoigiaothuong.Domain.Contracts.QuotationResponseContract;
using static dotnet9_ketnoigiaothuong.Domain.Contracts.CategoryContract;

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
            
            // Category mappings
            CreateMap<Category, CategoryListItem>()
                .ForMember(dest => dest.ParentCategoryName, opt => opt.MapFrom(src => 
                    src.ParentCategory != null ? src.ParentCategory.CategoryName : null));
                
            CreateMap<Category, CategoryDetailModel>()
                .ForMember(dest => dest.ParentCategoryName, opt => opt.MapFrom(src => 
                    src.ParentCategory != null ? src.ParentCategory.CategoryName : null))
                .ForMember(dest => dest.SubCategories, opt => opt.MapFrom(src => src.SubCategories));
                
            CreateMap<CreateCategoryModel, Category>();
            CreateMap<UpdateCategoryModel, Category>();
            
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
