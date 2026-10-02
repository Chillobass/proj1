using UserService.Domain;

namespace UserService.Application.Users.GetById;

public sealed class GetUserByIdHandler(IUserRepository userRepository)
{
    public async Task<GetUserByIdResult> ExecuteAsync(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(query.Id, cancellationToken);

        return user is null
            ? new GetUserByIdResult.NotFound()
            : new GetUserByIdResult.Success(user);
    }
}
