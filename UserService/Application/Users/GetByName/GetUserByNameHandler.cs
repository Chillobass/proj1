using UserService.Domain;

namespace UserService.Application.Users.GetByName;

public sealed class GetUserByNameHandler(IUserRepository userRepository)
{
    public async Task<GetUserByNameResult> ExecuteAsync(GetUserByNameQuery query, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByNameAsync(query.Name, query.Surname, cancellationToken);

        return user is null
            ? new GetUserByNameResult.NotFound()
            : new GetUserByNameResult.Success(user);
    }
}
