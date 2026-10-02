using Moq;
using UserService.Application.Users.Create;
using UserService.Domain;

namespace UserService.Tests.Application.Users.Create;

public sealed class CreateUserHandlerTests
{
    private readonly Mock<IUserRepository> _userRepository = new();
    private readonly CreateUserHandler _handler;

    public CreateUserHandlerTests()
    {
        _handler = new CreateUserHandler(_userRepository.Object, new CreateUserValidator());
    }

    [Fact]
    public async Task ExecutionRepositoryCreatesUserTest()
    {
        var user = new User { Id = 1, Login = "login", Password = "password", Name = "name", Surname = "surname", Age = 25 };
        _userRepository
            .Setup(r => r.TryCreateAsync("login", "password", "name", "surname", 25, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        var command = new CreateUserCommand("login", "password", "name", "surname", 25);
        var result = await _handler.ExecuteAsync(command, CancellationToken.None);

        var success = Assert.IsType<CreateUserResult.Success>(result);
        Assert.Same(user, success.User);
    }

    [Fact]
    public async Task ExecutionRepositoryLoginCollisionTest()
    {
        _userRepository
            .Setup(r => r.TryCreateAsync(
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((User?)null);

        var command = new CreateUserCommand("login", "password", "name", "surname", 25);
        var result = await _handler.ExecuteAsync(command, CancellationToken.None);

        Assert.IsType<CreateUserResult.LoginCollision>(result);
    }

    [Fact]
    public async Task ExecutionInvalidAgeValidationErrorTest()
    {
        var command = new CreateUserCommand("login", "password", "name", "surname", 5);
        var result = await _handler.ExecuteAsync(command, CancellationToken.None);

        Assert.IsType<CreateUserResult.ValidationFailed>(result);
        _userRepository.VerifyNoOtherCalls();
    }
}
