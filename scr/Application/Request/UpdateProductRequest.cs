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
        public string PathPicture { get; set; }
        public int IdSection { get; set; }
    }
    public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
    {
        public UpdateProductRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty().GreaterThan(0).WithMessage("Id must be positive").LessThan(int.MaxValue).WithMessage("Id is too long");
            RuleFor(x => x.Length).NotEmpty().GreaterThan(0).WithMessage("Length must be positive").LessThan(double.MaxValue).WithMessage("Length is too long");
            RuleFor(x => x.Height).NotEmpty().GreaterThan(0).WithMessage("Height must be positive").LessThan(double.MaxValue).WithMessage("Height is too long");
            RuleFor(x => x.Width).NotEmpty().GreaterThan(0).WithMessage("Width must be positive").LessThan(double.MaxValue).WithMessage("Width is too long");
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0).WithMessage("Price must be positive").LessThan(double.MaxValue).WithMessage("Price is too long");
            RuleFor(x => x.Discount).GreaterThanOrEqualTo(0).WithMessage("Discount must be positive").LessThanOrEqualTo(1).WithMessage("Discount must be less 1");
            RuleFor(x => x.PathPicture).NotEmpty().MaximumLength(255).WithMessage("PathPicture has 255 max length");
            RuleFor(x => x.IdSection).NotEmpty().GreaterThan(0).WithMessage("IdSection must be positive").LessThan(int.MaxValue).WithMessage("IdSection is too long");
        }
    }
}
