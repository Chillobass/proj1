using UserService.Domain;

namespace UserService.Application.Users.Create;

public abstract record CreateUserResult
{
    private CreateUserResult()
    {
    }

    public sealed record Success(User User) : CreateUserResult;

    public sealed record LoginCollision : CreateUserResult;

    public sealed record ValidationFailed(IReadOnlyList<string> Errors) : CreateUserResult;
}
