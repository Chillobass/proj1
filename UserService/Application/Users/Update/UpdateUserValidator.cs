using FluentValidation;

namespace UserService.Application.Users.Update;

public sealed class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserValidator()
    {
        RuleFor(c => c.Password).NotEmpty().When(c => c.Password is not null);
        RuleFor(c => c.Name).NotEmpty().When(c => c.Name is not null);
        RuleFor(c => c.Surname).NotEmpty().When(c => c.Surname is not null);
        RuleFor(c => c.Age.Value).InclusiveBetween(16, 100).WithName("Age").When(c => c.Age is not null);
    }
}
