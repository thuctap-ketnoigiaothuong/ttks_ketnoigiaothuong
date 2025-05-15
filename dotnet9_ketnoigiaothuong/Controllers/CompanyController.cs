using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using dotnet9_ketnoigiaothuong.Domain.Contracts;
using static dotnet9_ketnoigiaothuong.Domain.Contracts.CompanyContract;

namespace dotnet9_ketnoigiaothuong.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : BaseController
    {
        private readonly IValidator<CompanyViewModel> _companyValidator;
        private readonly IValidator<CreateCompanyModel> _createCompanyValidator;
        private readonly IValidator<UpdateCompanyModel> _updateCompanyValidator;

        public CompanyController(
            IValidator<CompanyViewModel> companyValidator,
            IValidator<CreateCompanyModel> createCompanyValidator,
            IValidator<UpdateCompanyModel> updateCompanyValidator)
        {
            _companyValidator = companyValidator;
            _createCompanyValidator = createCompanyValidator;
            _updateCompanyValidator = updateCompanyValidator;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<CompanyListItem>>>> GetAllCompanies()
        {
            var result = await Provider.CompanyService.GetAllCompaniesAsync();
            if (!result.IsSuccess)
                return BadRequest(result);
                
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<FullResponseCompany>>> GetCompanyById(int id)
        {
            var result = await Provider.CompanyService.GetCompanyByIdAsync(id);
            if (!result.IsSuccess)
                return NotFound(result);
                
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<FullResponseCompany>>> CreateCompany([FromBody] CreateCompanyModel model)
        {
            var validationResult = await _createCompanyValidator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var result = await Provider.CompanyService.CreateCompanyAsync(model);
            if (!result.IsSuccess)
                return BadRequest(result);
                
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Company")]
        public async Task<ActionResult<ApiResponse<FullResponseCompany>>> UpdateCompany(int id, [FromBody] UpdateCompanyModel model)
        {
            var validationResult = await _updateCompanyValidator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            // Kiểm tra quyền Company chỉ được sửa công ty của mình
            if (User.IsInRole("Company"))
            {
                // Lấy công ty của người dùng hiện tại
                var email = User.FindFirst(ClaimTypes.Email)?.Value;
                var userCompany = await Provider.CompanyService.Async_GetCompanyProfileByEmailAndPhoneNumber(email, "");
                
                if (!userCompany.IsSuccess || userCompany.Data.CompanyID != id)
                {
                    return Forbid();
                }
            }

            var result = await Provider.CompanyService.UpdateCompanyAsync(id, model);
            if (!result.IsSuccess)
                return BadRequest(result);
                
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteCompany(int id)
        {
            var result = await Provider.CompanyService.DeleteCompanyAsync(id);
            if (!result.IsSuccess)
                return BadRequest(result);
                
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CompanyViewModel companyViewModel)
        {
            var validationResult = await _companyValidator.ValidateAsync(companyViewModel);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var response = await Provider.CompanyService.CreateCompanyProfileAsync(companyViewModel);
            if (response == null)
            {
                return BadRequest("Company already exists");
            }
            return Ok(new {
                response,
                redirect_url = "/"
            });
        }
        
        [HttpPost("get-by-email-phone")]
        public async Task<IActionResult> GetCompanyProfileByEmailAndPhoneNumber([FromBody] CompanyContact companyContact)
        {
            var response = await Provider.CompanyService.Async_GetCompanyProfileByEmailAndPhoneNumber(companyContact.Email, companyContact.PhoneNumber);
            if (response == null)
            {
                return BadRequest("Company not found!");
            }
            return Ok(response);
        }
    }
}
