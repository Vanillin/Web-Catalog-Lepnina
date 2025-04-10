using FluentValidation;

namespace Application.Request
{
    public class UpdateAttachmentRequest
    {
        public int Id { get; set; }
        public string? Message { get; set; }
        public string PathPicture { get; set; } = null!;
        public int IdProduct { get; set; }
    }
    public class UpdateAttachmentRequestValidator : AbstractValidator<UpdateAttachmentRequest>
    {
        public UpdateAttachmentRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty()
                .InclusiveBetween(1, int.MaxValue);
            RuleFor(x => x.Message)
                .MaximumLength(ValidationConstants.MaxMessageLenght).WithMessage("Message is too long");
            RuleFor(x => x.PathPicture).NotEmpty()
                .MaximumLength(ValidationConstants.MaxPathPictureLenght).WithMessage("PathPicture is too long");
            RuleFor(x => x.IdProduct).NotEmpty()
                .InclusiveBetween(1, int.MaxValue);

        }
    }
}
