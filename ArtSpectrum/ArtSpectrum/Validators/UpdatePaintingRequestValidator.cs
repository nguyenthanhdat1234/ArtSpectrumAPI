using ArtSpectrum.Contracts.Request;
using FluentValidation;

namespace ArtSpectrum.Validators
{
    public class UpdatePaintingRequestValidator : AbstractValidator<UpdatePaintingRequest>
    {
        public UpdatePaintingRequestValidator() 
        {
            RuleLevelCascadeMode = CascadeMode.Stop;
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Painting length must not exceed 100 characters.");
            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Price cannot be left blank.")
                .GreaterThan(0).WithMessage("Price must be greater than zero.");
            RuleFor(x => x.StockQuantity)
                .NotEmpty().WithMessage("Quantity cannot be left blank.")
                .GreaterThanOrEqualTo(0).WithMessage("Stock Quantity must be equal or greater than zero.");
            RuleFor(x => x.SalesPrice)
                .InclusiveBetween(0, 100).WithMessage("Sales Price must be between 0 and 100.");

        } 
    }
}
