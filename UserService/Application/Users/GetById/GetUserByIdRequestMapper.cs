using Riok.Mapperly.Abstractions;
using GrpcContracts = UserService.Contracts;

namespace UserService.Application.Users.GetById;

[Mapper]
public partial class GetUserByIdRequestMapper
{
    public partial GetUserByIdQuery ToQuery(GrpcContracts.GetUserByIdRequest request);
}
