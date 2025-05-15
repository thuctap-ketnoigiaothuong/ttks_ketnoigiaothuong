using AutoMapper;
using dotnet9_ketnoigiaothuong.Domain.Entities;
using static dotnet9_ketnoigiaothuong.Domain.Contracts.AuthContract;
using static dotnet9_ketnoigiaothuong.Domain.Contracts.ProductContract;
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


            // Product mappings
            CreateMap<Product, ResponseProductModel>()
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.CompanyName))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName));

            CreateMap<Product, ProductDetailModel>()
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.CompanyName))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName))
                .ForMember(dest => dest.ApprovedByUserName, opt => opt.MapFrom(src => src.ApprovedByUser.FullName));
                
            // Mapping từ DTO tới entity
            CreateMap<CreateProductModel, Product>()
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now));
                
            CreateMap<UpdateProductModel, Product>();
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
