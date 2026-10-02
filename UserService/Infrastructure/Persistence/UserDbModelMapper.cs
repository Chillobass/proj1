using Riok.Mapperly.Abstractions;
using UserService.Domain;

namespace UserService.Infrastructure.Persistence;

[Mapper]
public partial class UserDbModelMapper
{
    public partial User ToDomain(UserDbModel dbModel);
}
