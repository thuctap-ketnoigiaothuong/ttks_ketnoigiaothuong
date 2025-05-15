using FluentValidation;
using static dotnet9_ketnoigiaothuong.Domain.Contracts.CategoryContract;

namespace dotnet9_ketnoigiaothuong.Domain.Validators
{
    public class CreateCategoryModelValidator : AbstractValidator<CreateCategoryModel>
    {
        public CreateCategoryModelValidator()
        {
            RuleFor(x => x.CategoryName).NotEmpty().WithMessage("Tên danh mục không được để trống")
                .MaximumLength(100).WithMessage("Tên danh mục không được vượt quá 100 ký tự");
        }
    }

    public class UpdateCategoryModelValidator : AbstractValidator<UpdateCategoryModel>
    {
        public UpdateCategoryModelValidator()
        {
            RuleFor(x => x.CategoryName).NotEmpty().WithMessage("Tên danh mục không được để trống")
                .MaximumLength(100).WithMessage("Tên danh mục không được vượt quá 100 ký tự");
        }
    }
} 