using FluentValidation;
using static dotnet9_ketnoigiaothuong.Domain.Contracts.QuotationResponseContract;

namespace dotnet9_ketnoigiaothuong.Domain.Validators
{
    public class CreateQuotationResponseValidator : AbstractValidator<CreateQuotationResponse>
    {
        public CreateQuotationResponseValidator()
        {
            RuleFor(x => x.RequestID)
                .NotEmpty()
                .WithMessage("Request ID is required.");
            RuleFor(x => x.BuyerCompanyID)
                .NotEmpty()
                .WithMessage("Buyer Company ID is required.");
            RuleFor(x => x.SellerCompanyID)
                .NotEmpty()
                .WithMessage("Seller Company ID is required.");
            RuleFor(x => x.ProposedPrice)
                .GreaterThan(0)
                .WithMessage("Proposed Price must be greater than 0.");
            RuleFor(x => x.DeliveryTime)
                .NotEmpty()
                .WithMessage("Delivery Time is required.");
            RuleFor(x => x.ShippingMethod)
                .NotEmpty()
                .WithMessage("Shipping Method is required.");
            RuleFor(x => x.Terms)
                .NotEmpty()
                .WithMessage("Terms are required.");
        }
    }
}
