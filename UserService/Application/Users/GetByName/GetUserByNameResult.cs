using UserService.Domain;

namespace UserService.Application.Users.GetByName;

public abstract record GetUserByNameResult
{
    private GetUserByNameResult() { }

    public sealed record Success(User User) : GetUserByNameResult;
    public sealed record NotFound : GetUserByNameResult;
}
