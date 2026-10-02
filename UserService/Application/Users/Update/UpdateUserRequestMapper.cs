using GrpcContracts = UserService.Contracts;

namespace UserService.Application.Users.Update;

public sealed class UpdateUserRequestMapper
{
    public UpdateUserCommand ToCommand(GrpcContracts.UpdateUserRequest request)
    {
        return new UpdateUserCommand(
            request.Id,
            request.HasPassword ? request.Password : null,
            request.HasName ? request.Name : null,
            request.HasSurname ? request.Surname : null,
            request.HasAge ? request.Age : null);
    }
}
