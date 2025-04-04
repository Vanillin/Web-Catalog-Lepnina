using FluentValidation;

namespace Application.Request
{
    public class UpdateReviewRequest
    {
        public int Id { get; set; }
        public string Message { get; set; } = null!;
        public string? PathPicture { get; set; }
        public int IdUser { get; set; }
        public int? IdProduct { get; set; }
    }
    public class UpdateReviewRequestValidator : AbstractValidator<UpdateReviewRequest>
    {
        public UpdateReviewRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty()
                .InclusiveBetween(1, int.MaxValue);
            RuleFor(x => x.Message).NotEmpty()
                .MaximumLength(ValidationConstants.MaxMessageLen).WithMessage("Message soo length");
            RuleFor(x => x.PathPicture)
                .MaximumLength(ValidationConstants.MaxPathPictLen).WithMessage("PathPicture soo length");
            RuleFor(x => x.IdUser).NotEmpty()
                .InclusiveBetween(1, int.MaxValue);
            RuleFor(x => x.IdProduct)
                .InclusiveBetween(1, int.MaxValue);
        }
    }
}
