using Riok.Mapperly.Abstractions;
using UserService.Domain;
using GrpcContracts = UserService.Contracts;

namespace UserService.Mapping;

[Mapper]
public partial class UserGrpcMapper
{
    public partial GrpcContracts.User ToProto(User user);
}
