using Moq;
using UserService.Application.Users.Update;
using UserService.Domain;

namespace UserService.Tests.Application.Users.Update;

public sealed class UpdateUserHandlerTests
{
    private readonly Mock<IUserRepository> _userRepository = new();
    private readonly UpdateUserHandler _handler;

    public UpdateUserHandlerTests()
    {
        _handler = new UpdateUserHandler(_userRepository.Object, new UpdateUserValidator());
    }

    [Fact]
    public async Task ExecutionRepositoryUpdatesUserTest()
    {
        var user = new User { Id = 1, Login = "login", Password = "password", Name = "name", Surname = "surname", Age = 30 };
        _userRepository
            .Setup(r => r.UpdateAsync(1, null, null, null, 30, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        var command = new UpdateUserCommand(1, null, null, null, 30);
        var result = await _handler.ExecuteAsync(command, CancellationToken.None);

        var success = Assert.IsType<UpdateUserResult.Success>(result);
        Assert.Same(user, success.User);
    }

    [Fact]
    public async Task ExecutionRepositoryNotFoundTest()
    {
        _userRepository
            .Setup(r => r.UpdateAsync(
                It.IsAny<int>(), It.IsAny<string?>(), It.IsAny<string?>(), It.IsAny<string?>(), It.IsAny<int?>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((User?)null);

        var command = new UpdateUserCommand(1, null, null, null, 30);
        var result = await _handler.ExecuteAsync(command, CancellationToken.None);

        Assert.IsType<UpdateUserResult.NotFound>(result);
    }

    [Fact]
    public async Task ExecutionInvalidAgeValidationErrorTest()
    {
        var command = new UpdateUserCommand(1, null, null, null, 5);
        var result = await _handler.ExecuteAsync(command, CancellationToken.None);

        Assert.IsType<UpdateUserResult.ValidationFailed>(result);
        _userRepository.VerifyNoOtherCalls();
    }
}
