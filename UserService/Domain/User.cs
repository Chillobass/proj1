namespace UserService.Domain;

public sealed record User
{
    public required int Id { get; init; }
    public required string Login { get; init; }
    public required string Password { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required int Age { get; set; }
}
