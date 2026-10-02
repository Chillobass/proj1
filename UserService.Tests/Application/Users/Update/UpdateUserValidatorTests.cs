using UserService.Application.Users.Update;

namespace UserService.Tests.Application.Users.Update;

public sealed class UpdateUserValidatorTests
{
    private readonly UpdateUserValidator _validator = new();

    [Fact]
    public void ValidationNoFieldsProvidedTest()
    {
        var command = new UpdateUserCommand(1, null, null, null, null);

        var result = _validator.Validate(command);

        Assert.True(result.IsValid);
    }

    [Fact]
    public void ValidationEmptyPasswordErrorTest()
    {
        var command = new UpdateUserCommand(1, "", null, null, null);

        var result = _validator.Validate(command);

        Assert.False(result.IsValid);
    }

    [Theory]
    [InlineData(15)]
    [InlineData(101)]
    public void ValidationAgeOutsideRangeErrorTest(int age)
    {
        var command = new UpdateUserCommand(1, null, null, null, age);

        var result = _validator.Validate(command);

        Assert.False(result.IsValid);
    }

    [Fact]
    public void ValidationAgeInsideRangeTest()
    {
        var command = new UpdateUserCommand(1, null, null, null, 30);

        var result = _validator.Validate(command);

        Assert.True(result.IsValid);
    }
}
