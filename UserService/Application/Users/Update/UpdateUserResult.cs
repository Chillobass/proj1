using UserService.Domain;

namespace UserService.Application.Users.Update;

public abstract record UpdateUserResult
{
    private UpdateUserResult() { }

    public sealed record Success(User User) : UpdateUserResult;
    public sealed record NotFound : UpdateUserResult;
    public sealed record ValidationFailed(IReadOnlyList<string> Errors) : UpdateUserResult;
}
