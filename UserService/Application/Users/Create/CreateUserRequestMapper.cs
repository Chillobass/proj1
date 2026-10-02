using Riok.Mapperly.Abstractions;
using GrpcContracts = UserService.Contracts;

namespace UserService.Application.Users.Create;

[Mapper]
public partial class CreateUserRequestMapper
{
    public partial CreateUserCommand ToCommand(GrpcContracts.CreateUserRequest request);
}
