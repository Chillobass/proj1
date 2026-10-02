using UserService.Application.Users.Create;

namespace UserService.Tests.Application.Users.Create;

public sealed class CreateUserValidatorTests
{
    private readonly CreateUserValidator _validator = new();

    [Fact]
    public void ValidationValidCommandTest()
    {
        var command = new CreateUserCommand("login", "password", "name", "surname", 25);

        var result = _validator.Validate(command);

        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("", "password", "name", "surname", 25)]
    [InlineData("login", "", "name", "surname", 25)]
    [InlineData("login", "password", "", "surname", 25)]
    [InlineData("login", "password", "name", "", 25)]
    public void ValidationEmptyRequiredFieldErrorTest(
        string login, string password, string name, string surname, int age)
    {
        var command = new CreateUserCommand(login, password, name, surname, age);

        var result = _validator.Validate(command);

        Assert.False(result.IsValid);
    }

    [Theory]
    [InlineData(15)]
    [InlineData(101)]
    public void ValidationAgeOutsideRangeErrorTest(int age)
    {
        var command = new CreateUserCommand("login", "password", "name", "surname", age);

        var result = _validator.Validate(command);

        Assert.False(result.IsValid);
    }

    [Theory]
    [InlineData(16)]
    [InlineData(100)]
    public void ValidationAgeAtBoundaryTest(int age)
    {
        var command = new CreateUserCommand("login", "password", "name", "surname", age);

        var result = _validator.Validate(command);

        Assert.True(result.IsValid);
    }
}
