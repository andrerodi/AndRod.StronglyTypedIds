using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;

namespace AndRod.StronglyTypedIds.SystemTextJson;

public static class DependencyInjection
{
    /// <summary>
    /// Adds strongly-typed ID JSON converters to the service collection.
    /// If a <see cref="JsonSerializerOptions"/> service is already registered, it will be used; otherwise, a new one will be created with <see cref="JsonSerializerDefaults.Web"/> as default.
    /// </summary>
    public static IServiceCollection AddStronlgyTypedIdJsonConverters(
        this IServiceCollection services,
        Action<StronglyTypedIdConfiguration> configure,
        params JsonConverter[] converters)
    {
        var configuration = StronglyTypedIdJsonConverterFactory.Configuration;
        configure(configuration);
        configuration.Build();

        foreach (var converterType in StronglyTypedIdJsonConverterFactory.CreateStronglyTypedIdJsonConverterTypes())
        {
            services.AddSingleton(converterType);
        }

        services.AddSingleton<JsonSerializerOptions>(sp =>
        {
            var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);

            foreach (var converter in StronglyTypedIdJsonConverterFactory.CreateStronglyTypedIdJsonConverters())
            {
                options.Converters.Add(converter);
            }

            foreach (var converter in converters)
            {
                options.Converters.Add(converter);
            }

            return options;
        });

        return services;
    }
}
