using FluentValidation;

namespace Application.Request
{
    public class UpdateProductRequest
    {
        public int Id { get; set; }
        public double Length { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public string PathPicture { get; set; } = null!;
        public int IdSection { get; set; }
    }
    public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
    {
        public UpdateProductRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty()
                 .InclusiveBetween(1, int.MaxValue);
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
                .MaximumLength(ValidationConstants.MaxPathPictureLenght).WithMessage("PathPicture is too long");
            RuleFor(x => x.IdSection).NotEmpty()
                 .InclusiveBetween(1, int.MaxValue);
        }
    }
}
