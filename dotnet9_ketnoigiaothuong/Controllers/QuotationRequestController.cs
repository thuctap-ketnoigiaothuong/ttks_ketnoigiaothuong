using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static dotnet9_ketnoigiaothuong.Domain.Contracts.QuotationRequestContract;

namespace dotnet9_ketnoigiaothuong.Controllers
{
    [ApiController]
    public class QuotationRequestController : BaseController
    {
        private readonly IValidator<CreateQuotationRequest> _validatorCreateQuotationRequest;

        public QuotationRequestController(IValidator<CreateQuotationRequest> validatorCreateQuotationRequest)
        {
            _validatorCreateQuotationRequest = validatorCreateQuotationRequest;
        }

        [HttpPost("api/quotation-request")]
        public async Task<IActionResult> CreateQuotationRequest([FromBody] CreateQuotationRequest quotationRequest)
        {
            var validationResult = _validatorCreateQuotationRequest.Validate(quotationRequest);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var response = await Provider.QuotationRequestService.CreateQuotationRequest(quotationRequest);
            return Ok(response);
        }
    }
}
