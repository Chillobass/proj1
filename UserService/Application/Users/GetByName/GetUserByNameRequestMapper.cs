using Riok.Mapperly.Abstractions;
using GrpcContracts = UserService.Contracts;

namespace UserService.Application.Users.GetByName;

[Mapper]
public partial class GetUserByNameRequestMapper
{
    public partial GetUserByNameQuery ToQuery(GrpcContracts.GetUserByNameRequest request);
}
