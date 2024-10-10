using FluentValidation;
using StockControl.API.Models;

namespace StockControl.API.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        // Constructor with rules
        public ProductValidator()
        {
            RuleFor(product => product.Description)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(255).WithMessage("Description must not exceed 255 characters");

            RuleFor(product => product.StockQuantity)
                .GreaterThanOrEqualTo(0).WithMessage("Stock quantity must be greater than or equal to 0");

            RuleFor(product => product.AverageCostPrice)
                .GreaterThanOrEqualTo(0).WithMessage("Average cost price must be greater than or equal to 0");

            RuleFor(product => product.PartNumber)
                .MaximumLength(255).WithMessage("Part number must not exceed 255 characters");
        }
    }
}
