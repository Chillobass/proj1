namespace UserService.Application.Users.Create;

public sealed record CreateUserCommand(string Login, string Password, string Name, string Surname, int Age);
