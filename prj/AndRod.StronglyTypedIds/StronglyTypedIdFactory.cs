using System.Reflection;

namespace AndRod.StronglyTypedIds;

/// <summary>
/// Factory class for creating strongly-typed IDs.
/// </summary>
public static class StronglyTypedIdFactory
{
    private static readonly StronglyTypedIdConfiguration _configuration = new();

    private static readonly HashSet<Type> _types = [];
    public static IReadOnlyCollection<Type> Types => _types;

    private static readonly Dictionary<Type, (Type ValueType, object DefaultValue)> _typeMap = [];
    public static IReadOnlyDictionary<Type, (Type ValueType, object DefaultValue)> TypeMap => _typeMap;

    static StronglyTypedIdFactory()
    {
        // Create the cache of strongly-typed ID value types including their default values
        foreach (var stronglyTypedIdType in GetAllStronglyTypedIds())
        {
            _types.Add(stronglyTypedIdType);
        }

        var stronglyTypedIdValueTypes = GetStronglyTypedIdValueTypes();

        foreach (var (stronglyTypedIdType, valueType) in stronglyTypedIdValueTypes)
        {
            _typeMap[stronglyTypedIdType] = (valueType, getDefaultValue(valueType));
        }

        static object getDefaultValue(Type type) => type.IsValueType ? Activator.CreateInstance(type)! : default!;
    }

    /// <summary>
    /// Configures the strongly-typed ID factory with the specified types.
    /// </summary>
    public static StronglyTypedIdConfiguration Configure()
    {
        return _configuration;
    }

    /// <summary>
    /// Returns a strongly-typed ID instance with the specified value.
    /// </summary>
    public static IStronglyTypedId Create(Type type, object value)
    {
        if (!_typeMap.TryGetValue(type, out var _))
        {
            throw new ArgumentException($"Unknown strongly-typed ID type: {type.FullName}");
        }

        return (IStronglyTypedId)Activator.CreateInstance(type, value)!;
    }

    /// <summary>
    /// Returns a strongly-typed ID instance with the specified value.
    /// </summary>
    public static TStronglyTypedId Create<TStronglyTypedId>(object value) where TStronglyTypedId : IStronglyTypedId
    {
        var type = typeof(TStronglyTypedId);

        return (TStronglyTypedId)Create(type, value);
    }

    /// <summary>
    /// Returns a strongly-typed ID instance with the specified value.
    /// </summary>
    internal static TStronglyTypedId Create<TStronglyTypedId, TValue>(TValue value)
        where TStronglyTypedId : IStronglyTypedId<TValue>
        where TValue : struct, IEquatable<TValue>, IComparable<TValue>
    {
        var type = typeof(TStronglyTypedId);

        return (TStronglyTypedId)Activator.CreateInstance(type, value)!;
    }

    /// <summary>
    /// Returns an empty instance of the strongly-typed ID type with the default value.
    /// </summary>
    public static IStronglyTypedId Empty(Type type)
    {
        if (!_typeMap.TryGetValue(type, out var valueType))
        {
            throw new ArgumentException($"Unknown strongly-typed ID type: {type.FullName}");
        }

        return (IStronglyTypedId)Activator.CreateInstance(type, args: valueType.DefaultValue)!;
    }

    /// <summary>
    /// Returns an empty instance of the strongly-typed ID type with the default value.
    /// </summary>
    public static TStronglyTypedId Empty<TStronglyTypedId>() where TStronglyTypedId : IStronglyTypedId
    {
        var type = typeof(TStronglyTypedId);

        return (TStronglyTypedId)Empty(type);
    }

    /// <summary>
    /// Returns an empty instance of the strongly-typed ID type with the default value.
    /// </summary>
    internal static TStronglyTypedId Empty<TStronglyTypedId, TValue>()
        where TStronglyTypedId : IStronglyTypedId<TValue>
        where TValue : struct, IEquatable<TValue>, IComparable<TValue>
    {
        var type = typeof(TStronglyTypedId);

        return (TStronglyTypedId)Activator.CreateInstance(type, args: default(TValue))!;
    }

    private static IEnumerable<Type> GetAllStronglyTypedIds()
    {
        if (_configuration is null)
        {
            return [];
        }

        var assemblies = GetAssemblies(_configuration.Types);
        return assemblies
            .SelectMany(a => a.GetTypes())
            .Where(t => typeof(IStronglyTypedId).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);

        static IEnumerable<Assembly> GetAssemblies(IEnumerable<Type> types)
        {
            foreach (var type in types)
            {
                var ass = Assembly.GetAssembly(type);

                if (ass is null)
                {
                    continue;
                }

                yield return ass;
            }
        }
    }

    private static IEnumerable<(Type StronglyTypedIdType, Type ValueType)> GetStronglyTypedIdValueTypes()
    {
        return GetAllStronglyTypedIds()
            .Select(t => (
                StronglyTypedIdType: t.BaseType!.GetGenericArguments()[-1],
                ValueType: t.BaseType!.GetGenericArguments()[0]
            ));
    }

    public sealed class StronglyTypedIdConfiguration
    {
        private readonly HashSet<Type> _types = [];
        public IReadOnlyCollection<Type> Types => _types;

        public StronglyTypedIdConfiguration Add(Type type)
        {
            _types.Add(type);
            return this;
        }
    }
}
