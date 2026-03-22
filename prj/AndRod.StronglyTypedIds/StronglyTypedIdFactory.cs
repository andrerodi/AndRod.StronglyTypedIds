namespace AndRod.StronglyTypedIds;

/// <summary>
/// Factory class for creating strongly-typed IDs.
/// </summary>
public static class StronglyTypedIdFactory
{
    private static readonly Dictionary<Type, (Type ValueType, object DefaultValue)> _typeMap = [];

    static StronglyTypedIdFactory()
    {
        // Scan all assemblies for types that implement IStronglyTypedId
        var stronglyTypedIdImplementationTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => typeof(IStronglyTypedId).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);

        foreach (var type in stronglyTypedIdImplementationTypes)
        {
            var genericValueType = getGenericValueType(type);
            _typeMap[type] = new(genericValueType, getDefaultValue(genericValueType));
        }

        static Type getGenericValueType(Type type) => type.BaseType!.GetGenericArguments()[1]!;

        static object getDefaultValue(Type type) => type.IsValueType ? Activator.CreateInstance(type)! : default!;
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
}
