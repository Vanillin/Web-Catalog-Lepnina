using FluentValidation;

namespace Application.Request
{
    public class UpdateUserRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? IdPictureIcon { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
    public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty()
                .InclusiveBetween(1, int.MaxValue);
            RuleFor(x => x.Name).NotEmpty()
                .MaximumLength(ValidationConstants.MaxNameLenght).WithMessage("Name is too long");
            RuleFor(x => x.IdPictureIcon)
                .InclusiveBetween(1, int.MaxValue);
            RuleFor(x => x.Email).NotEmpty()
                .EmailAddress();
            RuleFor(x => x.Password).NotEmpty()
                .MinimumLength(ValidationConstants.MinPasswordLenght);
        }
    }
}
