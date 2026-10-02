using Riok.Mapperly.Abstractions;
using GrpcContracts = UserService.Contracts;

namespace UserService.Application.Users.Delete;

[Mapper]
public partial class DeleteUserRequestMapper
{
    public partial DeleteUserCommand ToCommand(GrpcContracts.DeleteUserRequest request);
}
