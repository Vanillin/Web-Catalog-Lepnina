using FluentValidation;

namespace Application.Request
{
    public class CreateAttachmentRequest
    {
        public string? Message { get; set; }
        public string PathPicture { get; set; } = null!;
        public int IdProduct { get; set; }
    }
    public class CreateAttachmentRequestValidator : AbstractValidator<CreateAttachmentRequest>
    {
        public CreateAttachmentRequestValidator()
        {
            RuleFor(x => x.Message)
                .MaximumLength(ValidationConstants.MaxMessageLen).WithMessage("Message soo length");
            RuleFor(x => x.PathPicture).NotEmpty()
                .MaximumLength(ValidationConstants.MaxPathPictLen).WithMessage("PathPicture soo length");
            RuleFor(x => x.IdProduct).NotEmpty()
                .InclusiveBetween(1, int.MaxValue);
        }
    }
}
