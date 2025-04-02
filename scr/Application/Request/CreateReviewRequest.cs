using FluentValidation;

namespace Application.Request
{
    public class CreateReviewRequest
    {
        public string Message { get; set; }
        public string PathPicture { get; set; }
        public int IdUser { get; set; }
        public int? IdProduct { get; set; }
    }
    public class CreateReviewRequestValidator : AbstractValidator<CreateReviewRequest>
    {
        public CreateReviewRequestValidator()
        {
            RuleFor(x => x.Message).NotEmpty().MaximumLength(255).WithMessage("Message has 255 max length");
            RuleFor(x => x.PathPicture).MaximumLength(255).WithMessage("PathPicture has 255 max length");
            RuleFor(x => x.IdUser).NotEmpty().GreaterThan(0).WithMessage("IdUser must be positive").LessThan(int.MaxValue).WithMessage("IdUser is too long");
            RuleFor(x => x.IdProduct).GreaterThan(0).WithMessage("IdProduct must be positive").LessThan(int.MaxValue).WithMessage("IdProduct is too long");
        }
    }
}
