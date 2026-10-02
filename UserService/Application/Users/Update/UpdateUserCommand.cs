namespace UserService.Application.Users.Update;

public sealed record UpdateUserCommand(int Id, string? Password, string? Name, string? Surname, int? Age);
