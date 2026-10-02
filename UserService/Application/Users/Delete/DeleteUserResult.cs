namespace UserService.Application.Users.Delete;

public abstract record DeleteUserResult
{
    private DeleteUserResult() { }

    public sealed record Success : DeleteUserResult;
    public sealed record NotFound : DeleteUserResult;
}
