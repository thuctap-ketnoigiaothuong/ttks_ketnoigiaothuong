using AutoMapper;
using dotnet9_ketnoigiaothuong.Domain.Entities;
using static dotnet9_ketnoigiaothuong.Domain.Contracts.AuthContract;
using static dotnet9_ketnoigiaothuong.Domain.Contracts.CompanyContract;
using static dotnet9_ketnoigiaothuong.Domain.Contracts.UserContract;

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

        }
    }
}
