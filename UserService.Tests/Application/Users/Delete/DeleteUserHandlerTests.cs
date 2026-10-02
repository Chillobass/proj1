using Moq;
using UserService.Application.Users.Delete;
using UserService.Domain;

namespace UserService.Tests.Application.Users.Delete;

public sealed class DeleteUserHandlerTests
{
    private readonly Mock<IUserRepository> _userRepository = new();
    private readonly DeleteUserHandler _handler;

    public DeleteUserHandlerTests()
    {
        _handler = new DeleteUserHandler(_userRepository.Object);
    }

    [Fact]
    public async Task ExecutionRepositoryDeletesUserTest()
    {
        _userRepository
            .Setup(r => r.DeleteAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var result = await _handler.ExecuteAsync(new DeleteUserCommand(1), CancellationToken.None);

        Assert.IsType<DeleteUserResult.Success>(result);
    }

    [Fact]
    public async Task ExecutionRepositoryNotFoundTest()
    {
        _userRepository
            .Setup(r => r.DeleteAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var result = await _handler.ExecuteAsync(new DeleteUserCommand(1), CancellationToken.None);

        Assert.IsType<DeleteUserResult.NotFound>(result);
    }
}
