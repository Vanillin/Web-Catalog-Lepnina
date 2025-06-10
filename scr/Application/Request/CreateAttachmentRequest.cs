using FluentValidation;

namespace Application.Request
{
    public class CreateAttachmentRequest
    {
        public string? Message { get; set; }
        public int IdPicture { get; set; }
        public int IdProduct { get; set; }
    }
    public class CreateAttachmentRequestValidator : AbstractValidator<CreateAttachmentRequest>
    {
        public CreateAttachmentRequestValidator()
        {
            RuleFor(x => x.Message)
                .MaximumLength(ValidationConstants.MaxMessageLenght).WithMessage("Message is too long");
            RuleFor(x => x.IdPicture).NotEmpty()
                .InclusiveBetween(1, int.MaxValue);
            RuleFor(x => x.IdProduct).NotEmpty()
                .InclusiveBetween(1, int.MaxValue);
        }
    }
}
