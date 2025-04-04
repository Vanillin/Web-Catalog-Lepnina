using FluentValidation;

namespace Application.Request
{
    public class UpdateUserRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? PathIcon { get; set; }
    }
    public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty()
                .InclusiveBetween(1, int.MaxValue);
            RuleFor(x => x.Name).NotEmpty()
                .MaximumLength(ValidationConstants.MaxNameLen).WithMessage("Name soo length");
            RuleFor(x => x.PathIcon)
                .MaximumLength(ValidationConstants.MaxPathPictLen).WithMessage("PathPicture soo length");
        }
    }
}
