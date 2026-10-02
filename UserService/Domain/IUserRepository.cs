namespace UserService.Domain;

public interface IUserRepository
{
    Task<User?> TryCreateAsync(
        string login,
        string password,
        string name,
        string surname,
        int age,
        CancellationToken cancellationToken);

    Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken);

    Task<User?> GetByNameAsync(string name, string surname, CancellationToken cancellationToken);

    Task<User?> UpdateAsync(
        int id,
        string? password,
        string? name,
        string? surname,
        int? age,
        CancellationToken cancellationToken);

    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
}
