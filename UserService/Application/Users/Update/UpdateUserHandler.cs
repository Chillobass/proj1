using UserService.Domain;

namespace UserService.Application.Users.Update;

public sealed class UpdateUserHandler(IUserRepository userRepository, UpdateUserValidator validator)
{
    public async Task<UpdateUserResult> ExecuteAsync(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(command);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(error => error.ErrorMessage).ToArray();
            return new UpdateUserResult.ValidationFailed(errors);
        }

        var user = await userRepository.UpdateAsync(
            command.Id, command.Password, command.Name, command.Surname, command.Age, cancellationToken);

        return user is null
            ? new UpdateUserResult.NotFound()
            : new UpdateUserResult.Success(user);
    }
}
