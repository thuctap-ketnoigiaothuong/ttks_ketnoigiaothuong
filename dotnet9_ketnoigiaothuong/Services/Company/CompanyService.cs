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

        // New CRUD methods
        public async Task<ApiResponse<List<CompanyListItem>>> GetAllCompaniesAsync()
        {
            try
            {
                var companies = await context.Companies.ToListAsync();
                var companyList = mapper.Map<List<CompanyListItem>>(companies);
                
                return new ApiResponse<List<CompanyListItem>>
                {
                    Data = companyList,
                    IsSuccess = true,
                    Message = "Lấy danh sách công ty thành công"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<CompanyListItem>>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = $"Lỗi khi lấy danh sách công ty: {ex.Message}"
                };
            }
        }

        public async Task<ApiResponse<FullResponseCompany>> GetCompanyByIdAsync(int id)
        {
            try
            {
                var company = await context.Companies.FindAsync(id);
                if (company == null)
                {
                    return new ApiResponse<FullResponseCompany>
                    {
                        Data = null,
                        IsSuccess = false,
                        Message = "Không tìm thấy công ty"
                    };
                }

                return new ApiResponse<FullResponseCompany>
                {
                    Data = mapper.Map<FullResponseCompany>(company),
                    IsSuccess = true,
                    Message = "Lấy thông tin công ty thành công"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<FullResponseCompany>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = $"Lỗi khi lấy thông tin công ty: {ex.Message}"
                };
            }
        }

        public async Task<ApiResponse<FullResponseCompany>> CreateCompanyAsync(CreateCompanyModel model)
        {
            try
            {
                // Kiểm tra email đã tồn tại chưa
                var existingCompany = await context.Companies.FirstOrDefaultAsync(c => c.Email == model.Email);
                if (existingCompany != null)
                {
                    return new ApiResponse<FullResponseCompany>
                    {
                        Data = null,
                        IsSuccess = false,
                        Message = "Email công ty đã tồn tại"
                    };
                }

                // Kiểm tra tax code đã tồn tại chưa
                existingCompany = await context.Companies.FirstOrDefaultAsync(c => c.TaxCode == model.TaxCode);
                if (existingCompany != null)
                {
                    return new ApiResponse<FullResponseCompany>
                    {
                        Data = null,
                        IsSuccess = false,
                        Message = "Mã số thuế công ty đã tồn tại"
                    };
                }

                var company = mapper.Map<Domain.Entities.Company>(model);
                
                await context.Companies.AddAsync(company);
                await context.SaveChangesAsync();

                return new ApiResponse<FullResponseCompany>
                {
                    Data = mapper.Map<FullResponseCompany>(company),
                    IsSuccess = true,
                    Message = "Tạo công ty thành công"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<FullResponseCompany>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = $"Lỗi khi tạo công ty: {ex.Message}"
                };
            }
        }

        public async Task<ApiResponse<FullResponseCompany>> UpdateCompanyAsync(int id, UpdateCompanyModel model)
        {
            try
            {
                var company = await context.Companies.FindAsync(id);
                if (company == null)
                {
                    return new ApiResponse<FullResponseCompany>
                    {
                        Data = null,
                        IsSuccess = false,
                        Message = "Không tìm thấy công ty"
                    };
                }

                // Kiểm tra email đã tồn tại ở công ty khác chưa
                var existingCompany = await context.Companies.FirstOrDefaultAsync(c => c.Email == model.Email && c.CompanyID != id);
                if (existingCompany != null)
                {
                    return new ApiResponse<FullResponseCompany>
                    {
                        Data = null,
                        IsSuccess = false,
                        Message = "Email công ty đã tồn tại"
                    };
                }

                // Kiểm tra tax code đã tồn tại ở công ty khác chưa
                existingCompany = await context.Companies.FirstOrDefaultAsync(c => c.TaxCode == model.TaxCode && c.CompanyID != id);
                if (existingCompany != null)
                {
                    return new ApiResponse<FullResponseCompany>
                    {
                        Data = null,
                        IsSuccess = false,
                        Message = "Mã số thuế công ty đã tồn tại"
                    };
                }

                mapper.Map(model, company);
                
                context.Companies.Update(company);
                await context.SaveChangesAsync();

                return new ApiResponse<FullResponseCompany>
                {
                    Data = mapper.Map<FullResponseCompany>(company),
                    IsSuccess = true,
                    Message = "Cập nhật công ty thành công"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<FullResponseCompany>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = $"Lỗi khi cập nhật công ty: {ex.Message}"
                };
            }
        }

        public async Task<ApiResponse<bool>> DeleteCompanyAsync(int id)
        {
            try
            {
                var company = await context.Companies.FindAsync(id);
                if (company == null)
                {
                    return new ApiResponse<bool>
                    {
                        Data = false,
                        IsSuccess = false,
                        Message = "Không tìm thấy công ty"
                    };
                }

                // Kiểm tra liên kết với các bảng khác
                bool hasUsers = await context.UserAccounts.AnyAsync(u => u.CompanyID == id);
                if (hasUsers)
                {
                    return new ApiResponse<bool>
                    {
                        Data = false,
                        IsSuccess = false,
                        Message = "Không thể xóa công ty vì có người dùng liên kết"
                    };
                }

                bool hasProducts = await context.Products.AnyAsync(p => p.CompanyID == id);
                if (hasProducts)
                {
                    return new ApiResponse<bool>
                    {
                        Data = false,
                        IsSuccess = false,
                        Message = "Không thể xóa công ty vì có sản phẩm liên kết"
                    };
                }

                context.Companies.Remove(company);
                await context.SaveChangesAsync();

                return new ApiResponse<bool>
                {
                    Data = true,
                    IsSuccess = true,
                    Message = "Xóa công ty thành công"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>
                {
                    Data = false,
                    IsSuccess = false,
                    Message = $"Lỗi khi xóa công ty: {ex.Message}"
                };
            }
        }
    }
}
