using FluentValidation;

namespace UserService.Application.Users.Create;

public sealed class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        RuleFor(c => c.Login).NotEmpty();
        RuleFor(c => c.Password).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.Surname).NotEmpty();
        RuleFor(c => c.Age).InclusiveBetween(16, 100);
    }
}
