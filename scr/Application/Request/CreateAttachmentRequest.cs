using FluentValidation;

namespace Application.Request
{
    public class CreateAttachmentRequest
    {
        public string Message { get; set; }
        public string PathPicture { get; set; }
        public int IdProduct { get; set; }
    }
    public class CreateAttachmentRequestValidator : AbstractValidator<CreateAttachmentRequest>
    {
        public CreateAttachmentRequestValidator()
        {
            RuleFor(x => x.Message).MaximumLength(255).WithMessage("Message has 255 max length");
            RuleFor(x => x.PathPicture).NotEmpty().MaximumLength(255).WithMessage("PathPicture has 255 max length");
            RuleFor(x => x.IdProduct).NotEmpty().GreaterThan(0).WithMessage("IdProduct must be positive").LessThan(int.MaxValue).WithMessage("IdProduct is too long");
        }
    }
}
