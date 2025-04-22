using FluentValidation;

namespace Application.Request
{
    public class UpdateSectionRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
    public class UpdateSectionRequestValidator : AbstractValidator<UpdateSectionRequest>
    {
        public UpdateSectionRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty()
                .InclusiveBetween(1, int.MaxValue);
            RuleFor(x => x.Name).NotEmpty()
                .MaximumLength(ValidationConstants.MaxNameLenght).WithMessage("Name is too long");
        }
    }
}
