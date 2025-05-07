using FluentValidation;
using static dotnet9_ketnoigiaothuong.Domain.Contracts.QuotationResponseContract;

namespace dotnet9_ketnoigiaothuong.Domain.Validators
{
    public class UpdateQuotationResponseValidator : AbstractValidator<UpdateQuotationResponse>
    {
        public UpdateQuotationResponseValidator()
        {
            RuleFor(x => x.ProposedPrice)
                .NotEmpty()
                .WithMessage("Proposed price is required.")
                .GreaterThan(0)
                .WithMessage("Proposed price must be greater than 0.");
            RuleFor(x => x.DeliveryTime)
                .NotEmpty()
                .WithMessage("Delivery time is required.");
            RuleFor(x => x.ShippingMethod)
                .NotEmpty()
                .WithMessage("Shipping method is required.");
            RuleFor(x => x.Terms)
                .NotEmpty()
                .WithMessage("Terms are required.");
        }
    }
}
