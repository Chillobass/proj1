namespace UserService.Mapping.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMapping(this IServiceCollection services)
    {
        services.AddSingleton<UserGrpcMapper>();

        return services;
    }
}
