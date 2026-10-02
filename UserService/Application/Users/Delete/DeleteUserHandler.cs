using UserService.Domain;

namespace UserService.Application.Users.Delete;

public sealed class DeleteUserHandler(IUserRepository userRepository)
{
    public async Task<DeleteUserResult> ExecuteAsync(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        var deleted = await userRepository.DeleteAsync(command.Id, cancellationToken);

        return deleted
            ? new DeleteUserResult.Success()
            : new DeleteUserResult.NotFound();
    }
}
