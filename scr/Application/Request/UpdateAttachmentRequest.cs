using FluentValidation;

namespace Application.Request
{
    public class UpdateAttachmentRequest
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string PathPicture { get; set; }
        public int IdProduct { get; set; }
    }
    public class UpdateAttachmentRequestValidator : AbstractValidator<UpdateAttachmentRequest>
    {
        public UpdateAttachmentRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty().GreaterThan(0).WithMessage("Id must be positive").LessThan(int.MaxValue).WithMessage("Id is too long");
            RuleFor(x => x.Message).MaximumLength(255).WithMessage("Message has 255 max length");
            RuleFor(x => x.PathPicture).NotEmpty().MaximumLength(255).WithMessage("PathPicture has 255 max length");
            RuleFor(x => x.IdProduct).NotEmpty().GreaterThan(0).WithMessage("IdProduct must be positive").LessThan(int.MaxValue).WithMessage("IdProduct is too long");

        }
    }
}
