using Dapper;
using Npgsql;
using UserService.Domain;

namespace UserService.Infrastructure.Persistence;

public sealed class UserRepository(NpgsqlDataSource dataSource, UserDbModelMapper mapper) : IUserRepository
{
    public async Task<User?> TryCreateAsync(
        string login,
        string password,
        string name,
        string surname,
        int age,
        CancellationToken cancellationToken)
    {
        var parameters = new DynamicParameters();
        parameters.Add("p_login", login);
        parameters.Add("p_password", password);
        parameters.Add("p_name", name);
        parameters.Add("p_surname", surname);
        parameters.Add("p_age", age);

        await using var connection = await dataSource.OpenConnectionAsync(cancellationToken);
        var command = new CommandDefinition(
            "SELECT * FROM fn_create_user(@p_login, @p_password, @p_name, @p_surname, @p_age)",
            parameters,
            cancellationToken: cancellationToken);

        var dbModel = await connection.QuerySingleOrDefaultAsync<UserDbModel>(command);

        return dbModel is null ? null : mapper.ToDomain(dbModel);
    }

    public async Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var parameters = new DynamicParameters();
        parameters.Add("p_id", id);

        await using var connection = await dataSource.OpenConnectionAsync(cancellationToken);
        var command = new CommandDefinition(
            "SELECT * FROM fn_get_user_by_id(@p_id)",
            parameters,
            cancellationToken: cancellationToken);

        var dbModel = await connection.QuerySingleOrDefaultAsync<UserDbModel>(command);

        return dbModel is null ? null : mapper.ToDomain(dbModel);
    }

    public async Task<User?> GetByNameAsync(string name, string surname, CancellationToken cancellationToken)
    {
        var parameters = new DynamicParameters();
        parameters.Add("p_name", name);
        parameters.Add("p_surname", surname);

        await using var connection = await dataSource.OpenConnectionAsync(cancellationToken);
        var command = new CommandDefinition(
            "SELECT * FROM fn_get_user_by_name(@p_name, @p_surname)",
            parameters,
            cancellationToken: cancellationToken);

        var dbModel = await connection.QuerySingleOrDefaultAsync<UserDbModel>(command);

        return dbModel is null ? null : mapper.ToDomain(dbModel);
    }

    public async Task<User?> UpdateAsync(
        int id,
        string? password,
        string? name,
        string? surname,
        int? age,
        CancellationToken cancellationToken)
    {
        var parameters = new DynamicParameters();
        parameters.Add("p_id", id);
        parameters.Add("p_password", password);
        parameters.Add("p_name", name);
        parameters.Add("p_surname", surname);
        parameters.Add("p_age", age);

        await using var connection = await dataSource.OpenConnectionAsync(cancellationToken);
        var command = new CommandDefinition(
            "SELECT * FROM fn_update_user(@p_id, @p_password, @p_name, @p_surname, @p_age)",
            parameters,
            cancellationToken: cancellationToken);

        var dbModel = await connection.QuerySingleOrDefaultAsync<UserDbModel>(command);

        return dbModel is null ? null : mapper.ToDomain(dbModel);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var parameters = new DynamicParameters();
        parameters.Add("p_id", id);

        await using var connection = await dataSource.OpenConnectionAsync(cancellationToken);
        var command = new CommandDefinition(
            "SELECT * FROM fn_delete_user(@p_id)",
            parameters,
            cancellationToken: cancellationToken);

        var deletedId = await connection.QuerySingleOrDefaultAsync<int?>(command);

        return deletedId is not null;
    }
}
