namespace UserService.Infrastructure.Persistence;

public sealed class UserDbModel
{
    public int Id { get; init; }
    public string Login { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string Surname { get; init; } = string.Empty;
    public int Age { get; init; }
}
