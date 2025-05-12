using FluentValidation;
using static dotnet9_ketnoigiaothuong.Domain.Contracts.CompanyContract;

namespace dotnet9_ketnoigiaothuong.Domain.Validators
{
    public class CompanyViewModelValidator: AbstractValidator<CompanyViewModel>
    {
        public CompanyViewModelValidator() 
        {
            RuleFor(x => x.CompanyName).NotEmpty().WithMessage("Company name is required");
            RuleFor(x => x.TaxCode).NotEmpty().WithMessage("Tax code is required");
        }
    }
}
