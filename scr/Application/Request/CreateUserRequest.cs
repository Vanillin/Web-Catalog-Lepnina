using FluentValidation;

namespace Application.Request
{
    public class CreateUserRequest
    {
        public string Name { get; set; } = null!;
        public string? PathIcon { get; set; }
    }
    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty()
                .MaximumLength(ValidationConstants.MaxNameLen).WithMessage("Name soo length");
            RuleFor(x => x.PathIcon)
                .MaximumLength(ValidationConstants.MaxPathPictLen).WithMessage("PathPicture soo length");
        }
    }
}