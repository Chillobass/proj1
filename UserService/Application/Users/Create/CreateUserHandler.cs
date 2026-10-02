using UserService.Domain;

namespace UserService.Application.Users.Create;

public sealed class CreateUserHandler(IUserRepository userRepository, CreateUserValidator validator)
{
    public async Task<CreateUserResult> ExecuteAsync(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(command);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(error => error.ErrorMessage).ToArray();
            return new CreateUserResult.ValidationFailed(errors);
        }

        var user = await userRepository.TryCreateAsync(
            command.Login,
            command.Password,
            command.Name,
            command.Surname,
            command.Age,
            cancellationToken);

        return user is null
            ? new CreateUserResult.LoginCollision()
            : new CreateUserResult.Success(user);
    }
}
