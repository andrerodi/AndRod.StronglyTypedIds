using System.Text.Json.Serialization;

namespace AndRod.StronglyTypedIds.SystemTextJson;

/// <summary>
/// Provides methods for scanning assemblies and creating strongly-typed ID converters.
/// </summary>
internal sealed class StronglyTypedIdJsonConverterFactory(StronglyTypedIdConfiguration configuration)
{
    private readonly StronglyTypedIdConfiguration _configuration = configuration;
    public StronglyTypedIdConfiguration Configuration => _configuration;

    /// <summary>
    /// Creates strongly-typed ID converters for all registered strongly-typed IDs.
    /// It uses the <see cref="StronglyTypedIdFactory"/> to get the registered types and creates a converter for each one.
    /// </summary>
    internal IEnumerable<Type> CreateStronglyTypedIdJsonConverterTypes()
    {
        foreach (var item in _configuration.TypeMap)
        {
            var genericClassType = typeof(StronglyTypedIdSystemTextJsonConverter<,>)
                .MakeGenericType(item.Key, item.Value.ValueType);

            yield return genericClassType;
        }
    }
}
