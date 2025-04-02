using FluentValidation;

namespace Application.Request
{
    public class UpdateUserRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PathIcon { get; set; }
    }
    public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty().GreaterThan(0).WithMessage("Id must be positive").LessThan(int.MaxValue).WithMessage("Id is too long");
            RuleFor(x => x.Name).NotEmpty().MaximumLength(255).WithMessage("Name has 255 max length");
            RuleFor(x => x.PathIcon).MaximumLength(255).WithMessage("PathIcon has 255 max length");
        }
    }
}
