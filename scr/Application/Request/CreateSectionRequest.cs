using FluentValidation;

namespace Application.Request
{
    public class CreateSectionRequest
    {
        public string Name { get; set; } = null!;
    }
    public class CreateSectionRequestValidator : AbstractValidator<CreateSectionRequest>
    {
        public CreateSectionRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty()
                .MaximumLength(ValidationConstants.MaxNameLenght).WithMessage("Name is too long");
        }
    }
}