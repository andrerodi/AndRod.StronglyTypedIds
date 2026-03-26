using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;

namespace AndRod.StronglyTypedIds.SystemTextJson;

public static class DependencyInjection
{
    private static JsonSerializerOptions? _cachedOptions;

    /// <summary>
    /// Adds strongly-typed ID JSON converters to the service collection.
    /// If a <see cref="JsonSerializerOptions"/> service is already registered, it will be used; otherwise, a new one will be created with <see cref="JsonSerializerDefaults.Web"/> as default.
    /// </summary>
    public static IServiceCollection AddStronlgyTypedIdJsonConverters(
        this IServiceCollection services,
        Action<StronglyTypedIdConfiguration> configure,
        params JsonConverter[] converters)
    {
        var configuration = new StronglyTypedIdConfiguration();
        configure(configuration);
        configuration.Build();

        var factory = new StronglyTypedIdJsonConverterFactory(configuration);

        foreach (var converter in factory.CreateStronglyTypedIdJsonConverterTypes())
        {
            services.AddSingleton(converter);
        }

        services.AddSingleton<StronglyTypedIdJsonConverterFactory>();

        services.AddSingleton<JsonSerializerOptions>(sp =>
        {
            var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
            foreach (var converter in factory.CreateStronglyTypedIdJsonConverterTypes())
            {
                options.Converters.Add((JsonConverter)sp.GetRequiredService(converter));
            }

            foreach (var converter in converters)
            {
                options.Converters.Add(converter);
            }

            _cachedOptions = options;
            return options;
        });

        return services;
    }
}
