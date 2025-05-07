using FluentValidation;
using static dotnet9_ketnoigiaothuong.Domain.Contracts.QuotationRequestContract;

namespace dotnet9_ketnoigiaothuong.Domain.Validators
{
    public class CreateQuotationRequestValidator : AbstractValidator<CreateQuotationRequest>
    {
        public CreateQuotationRequestValidator() 
        {
            RuleFor(x => x.BuyerCompanyID)
                .NotEmpty()
                .WithMessage("Buyer company ID is required.");
            RuleFor(x => x.SellerCompanyID)
                .NotEmpty()
                .WithMessage("Seller company ID is required.");
            RuleFor(x => x.ProductID)
                .NotEmpty()
                .WithMessage("Product ID is required.");
            RuleFor(x => x.Quantity)
                .NotEmpty()
                .WithMessage("Quantity is required.")
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than 0.");
            RuleFor(x => x.DeliveryTime)
                .NotEmpty()
                .WithMessage("Delivery time is required.");
            RuleFor(x => x.AdditionalRequest)
                .MaximumLength(500)
                .WithMessage("Additional request cannot exceed 500 characters.");
        }
    }
}
