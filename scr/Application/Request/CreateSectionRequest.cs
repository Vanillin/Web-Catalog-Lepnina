using FluentValidation;

namespace Application.Request
{
    public class CreateSectionRequest
    {
        public string Name { get; set; }
    }
    public class CreateSectionRequestValidator : AbstractValidator<CreateSectionRequest>
    {
        public CreateSectionRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(255).WithMessage("Name has 255 max length");
        }
    }
}