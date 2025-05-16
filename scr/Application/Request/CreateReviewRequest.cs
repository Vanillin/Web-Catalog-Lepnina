using FluentValidation;

namespace Application.Request
{
    public class CreateReviewRequest
    {
        public string Message { get; set; } = null!;
        public int? IdPicture { get; set; }
        public int IdUser { get; set; }
        public int? IdProduct { get; set; }
    }
    public class CreateReviewRequestValidator : AbstractValidator<CreateReviewRequest>
    {
        public CreateReviewRequestValidator()
        {
            RuleFor(x => x.Message).NotEmpty()
                .MaximumLength(ValidationConstants.MaxMessageLenght).WithMessage("Message is too long");
            RuleFor(x => x.IdPicture)
                .InclusiveBetween(1, int.MaxValue);
            RuleFor(x => x.IdUser).NotEmpty()
                .InclusiveBetween(1, int.MaxValue);
            RuleFor(x => x.IdProduct)
                .InclusiveBetween(1, int.MaxValue);
        }
    }
}
