using System.Reflection;
using System.Text.Json.Serialization;

namespace AndRod.StronglyTypedIds.SystemTextJson;

public sealed class StronglyTypedIdConverterConfiguration
{
    private readonly HashSet<Type> _types = [];
    public IReadOnlyCollection<Type> Types => _types;

    public StronglyTypedIdConverterConfiguration Add(Type type)
    {
        _types.Add(type);
        return this;
    }
}

public static class AssemblyScan
{
    private static StronglyTypedIdConverterConfiguration? _configuration;

    public static void Configure(Action<StronglyTypedIdConverterConfiguration> configure)
    {
        _configuration = new StronglyTypedIdConverterConfiguration();
        configure(_configuration);
    }

    public static IEnumerable<Type> GetAllConverters()
    {
        if (_configuration is null)
        {
            return [];
        }

        var assemblies = GetAssemblies(_configuration.Types);

        return assemblies
            .SelectMany(a => a.GetTypes())
            .Where(t => t.IsAssignableTo(typeof(StronglyTypedIdSystemTextJsonConverter<,>)));

        static IEnumerable<Assembly> GetAssemblies(IEnumerable<Type> types)
        {
            foreach (var type in types)
            {
                yield return type.Assembly;
            }
        }
    }

    public static IEnumerable<JsonConverter> CreateStronglyTypedIdConverters()
    {
        var stronglyTypedIdTypes = StronglyTypedIdFactory.TypeMap;

        foreach (var stronglyTypedIdType in stronglyTypedIdTypes)
        {
            var genericClassType = typeof(StronglyTypedIdSystemTextJsonConverter<,>)
                .MakeGenericType(stronglyTypedIdType.Key, stronglyTypedIdType.Value.ValueType);

            yield return (JsonConverter)Activator.CreateInstance(genericClassType)!;
        }
    }
}
