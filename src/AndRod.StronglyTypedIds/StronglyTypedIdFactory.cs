namespace AndRod.StronglyTypedIds;

/// <summary>
/// Factory class for creating strongly-typed IDs.
/// </summary>
public static class StronglyTypedIdFactory
{
    private static StronglyTypedIdConfiguration _configuration = new();
    public static StronglyTypedIdConfiguration Configuration => _configuration;

    /// <summary>
    /// Returns a strongly-typed ID instance with the specified value.
    /// </summary>
    public static IStronglyTypedId Create(Type type, object value)
    {
        if (!_configuration.TypeMap.TryGetValue(type, out var _))
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
    public static TStronglyTypedId Create<TStronglyTypedId, TValue>(TValue value)
        where TStronglyTypedId : StronglyTypedId<TStronglyTypedId, TValue>
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
        if (!_configuration.TypeMap.TryGetValue(type, out var valueType))
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
        return (TStronglyTypedId)Empty(typeof(TStronglyTypedId));
    }

    public static TStronglyTypedId Empty<TStronglyTypedId, TValue>()
        where TStronglyTypedId : IStronglyTypedId<TValue>
        where TValue : struct, IEquatable<TValue>, IComparable<TValue>
    {
        return (TStronglyTypedId)Activator.CreateInstance(typeof(TStronglyTypedId), args: default(TValue))!;
    }
}
