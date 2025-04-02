using FluentValidation;

namespace Application.Request
{
    public class CreateUserRequest
    {
        public string Name { get; set; }
        public string PathIcon { get; set; }
    }
    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(255).WithMessage("Name has 255 max length");
            RuleFor(x => x.PathIcon).MaximumLength(255).WithMessage("PathIcon has 255 max length");
        }
    }
}