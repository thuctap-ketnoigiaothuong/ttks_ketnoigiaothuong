using AutoMapper;
using dotnet9_ketnoigiaothuong.Domain.Contracts;
using dotnet9_ketnoigiaothuong.Domain.Entities;
using dotnet9_ketnoigiaothuong.Infrastructure.Context;
using dotnet9_ketnoigiaothuong.Services.Company;
using Microsoft.EntityFrameworkCore;
using static dotnet9_ketnoigiaothuong.Domain.Contracts.CompanyContract;

namespace dotnet9_ketnoigiaothuong.Services.User
{
    public class CompanyService: BaseRepository, ICompanyService
    {
        public CompanyService(AppDbContext context, IMapper mapper) : base(context, mapper)
        {}

        public async Task<ApiResponse<ResponseCompany>> CreateCompanyProfileAsync(CompanyViewModel model)
        {
            var existCompany = await context.Companies
                .FirstOrDefaultAsync(x => x.Email == model.Email);
            if (existCompany != null)
            {
                return new ApiResponse<ResponseCompany>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "Email already exists"
                };
            }
            var company = new Domain.Entities.Company
            {
                CompanyName = model.CompanyName,
                TaxCode = model.TaxCode,
                BusinessSector = model.BusinessSector,
                Address = model.Address,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Representative = model.Representative,
                RegistrationDate = DateTime.Now,
                VerificationStatus= "Unverified",
                LegalDocuments="",
            };
            await context.Companies.AddAsync(company);
            await context.SaveChangesAsync();
            return new ApiResponse<ResponseCompany>
            {
                Data = mapper.Map<ResponseCompany>(company),
                IsSuccess = true,
                Message = "Added company profile successfully"
            };
        }

        public async Task<ApiResponse<FullResponseCompany>> Async_GetCompanyProfileByEmailAndPhoneNumber(string email, string phoneNumber)
        {
            var existCompany = await context.Companies
                .FirstOrDefaultAsync(x => x.Email == email && x.PhoneNumber == phoneNumber);
            if (existCompany == null)
            {
                return new ApiResponse<FullResponseCompany>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "Company profile not found!"
                };
            }
            else
            {
                return new ApiResponse<FullResponseCompany>
                {
                    Data = mapper.Map<FullResponseCompany>(existCompany),
                    IsSuccess = true,
                    Message = "Company profile received successfully"
                };
            }
        }
    }
}
