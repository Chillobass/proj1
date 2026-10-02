using UserService.Domain;

namespace UserService.Application.Users.GetById;

public abstract record GetUserByIdResult
{
    private GetUserByIdResult() { }

    public sealed record Success(User User) : GetUserByIdResult;
    public sealed record NotFound : GetUserByIdResult;
}
