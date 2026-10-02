using UserService.Application.Users.Create;
using UserService.Application.Users.Delete;
using UserService.Application.Users.GetById;
using UserService.Application.Users.GetByName;
using UserService.Application.Users.Update;

namespace UserService.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        AddCreateUser(services);
        AddGetUserById(services);
        AddGetUserByName(services);
        AddUpdateUser(services);
        AddDeleteUser(services);

        return services;
    }

    private static void AddCreateUser(IServiceCollection services)
    {
        services.AddSingleton<CreateUserRequestMapper>();
        services.AddSingleton<CreateUserValidator>();
        services.AddSingleton<CreateUserHandler>();
    }

    private static void AddGetUserById(IServiceCollection services)
    {
        services.AddSingleton<GetUserByIdRequestMapper>();
        services.AddSingleton<GetUserByIdHandler>();
    }

    private static void AddGetUserByName(IServiceCollection services)
    {
        services.AddSingleton<GetUserByNameRequestMapper>();
        services.AddSingleton<GetUserByNameHandler>();
    }

    private static void AddUpdateUser(IServiceCollection services)
    {
        services.AddSingleton<UpdateUserRequestMapper>();
        services.AddSingleton<UpdateUserValidator>();
        services.AddSingleton<UpdateUserHandler>();
    }

    private static void AddDeleteUser(IServiceCollection services)
    {
        services.AddSingleton<DeleteUserRequestMapper>();
        services.AddSingleton<DeleteUserHandler>();
    }
}
