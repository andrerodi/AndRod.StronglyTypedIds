using System.Text.Json.Serialization;

namespace AndRod.StronglyTypedIds.SystemTextJson;

/// <summary>
/// Provides methods for scanning assemblies and creating strongly-typed ID converters.
/// </summary>
internal static class StronglyTypedIdJsonConverterFactory
{
    private static StronglyTypedIdConfiguration _configuration = new();
    public static StronglyTypedIdConfiguration Configuration => _configuration;

    /// <summary>
    /// Creates strongly-typed ID converters for all registered strongly-typed IDs.
    /// It uses the <see cref="StronglyTypedIdFactory"/> to get the registered types and creates a converter for each one.
    /// </summary>
    public static IEnumerable<Type> CreateStronglyTypedIdJsonConverterTypes()
    {
        return Configuration
                .TypeMap
                .Select(item => typeof(StronglyTypedIdSystemTextJsonConverter<,>)
                .MakeGenericType(item.Key, item.Value.ValueType));
    }

    public static IEnumerable<JsonConverter> CreateStronglyTypedIdJsonConverters()
    {
        return CreateStronglyTypedIdJsonConverterTypes()
            .Select(type => (JsonConverter)Activator.CreateInstance(type)!);
    }
}
