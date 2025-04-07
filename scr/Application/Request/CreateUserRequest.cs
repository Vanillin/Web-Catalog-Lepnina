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
                .MaximumLength(ValidationConstants.MaxNameLenght).WithMessage("Name is too long");
            RuleFor(x => x.PathIcon)
                .MaximumLength(ValidationConstants.MaxPathPictureLenght).WithMessage("PathPicture is too long");
        }
    }
}