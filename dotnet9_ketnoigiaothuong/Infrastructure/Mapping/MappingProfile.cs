using AutoMapper;
using dotnet9_ketnoigiaothuong.Domain.Entities;
using static dotnet9_ketnoigiaothuong.Domain.Contracts.AuthContract;
using static dotnet9_ketnoigiaothuong.Domain.Contracts.ProductContract;

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
        }
    }
}
