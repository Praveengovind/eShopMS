namespace CatalogAPI.Products.CreateProduct
{
    public class CreateProductCommandValidator: AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product name is required")
                .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters");
            RuleFor(x => x.Category)
                .NotEmpty().WithMessage("At least one category is required");
            RuleFor(x => x.ImageFile)
                .NotEmpty().WithMessage("Image file URL is required")
                .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute))
                .WithMessage("Image file must be a valid URL");
            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }
}
