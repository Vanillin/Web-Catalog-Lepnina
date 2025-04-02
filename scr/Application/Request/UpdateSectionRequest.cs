using FluentValidation;

namespace Application.Request
{
    public class UpdateSectionRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class UpdateSectionRequestValidator : AbstractValidator<UpdateSectionRequest>
    {
        public UpdateSectionRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty().GreaterThan(0).WithMessage("Id must be positive").LessThan(int.MaxValue).WithMessage("Id is too long");
            RuleFor(x => x.Name).NotEmpty().MaximumLength(255).WithMessage("Name has 255 max length");
        }
    }
}
