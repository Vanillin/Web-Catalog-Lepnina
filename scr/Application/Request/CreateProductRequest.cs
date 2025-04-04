using FluentValidation;

namespace Application.Request
{
    public class CreateProductRequest
    {
        public double Length { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public string PathPicture { get; set; } = null!;
        public int IdSection { get; set; }
    }
    public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
    {
        public CreateProductRequestValidator()
        {
            RuleFor(x => x.Length).NotEmpty()
                 .InclusiveBetween(1, double.MaxValue);
            RuleFor(x => x.Height).NotEmpty()
                 .InclusiveBetween(1, double.MaxValue);
            RuleFor(x => x.Width).NotEmpty()
                 .InclusiveBetween(1, double.MaxValue);
            RuleFor(x => x.Price)
                 .InclusiveBetween(0, double.MaxValue);
            RuleFor(x => x.Discount)
                .InclusiveBetween(0, 1).WithMessage("Discount must be more/equals 0 and less/equals 1");
            RuleFor(x => x.PathPicture).NotEmpty()
                .MaximumLength(ValidationConstants.MaxPathPictLen).WithMessage("PathPicture soo length");
            RuleFor(x => x.IdSection).NotEmpty()
                 .InclusiveBetween(1, int.MaxValue);
        }
    }
}
