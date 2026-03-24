using Microsoft.Extensions.DependencyInjection;
using static AndRod.StronglyTypedIds.StronglyTypedIdFactory;

namespace AndRod.StronglyTypedIds;

public static class DependencyInjection
{
    public static IServiceCollection AddStronglyTypedIds(
        this IServiceCollection services,
        Action<StronglyTypedIdConfiguration> configure)
    {
        var config = new StronglyTypedIdConfiguration();
        configure.Invoke(config);
        config.Build();

        services.AddSingleton(config);
        services.AddSingleton<StronglyTypedIdFactory>();

        return services;
    }
}
