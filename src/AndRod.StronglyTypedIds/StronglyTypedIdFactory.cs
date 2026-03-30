namespace AndRod.StronglyTypedIds;

/// <summary>
/// Factory class for creating strongly-typed IDs.
/// </summary>
public static class StronglyTypedIdFactory
{
    public static StronglyTypedIdConfiguration Configuration { get; } = new();

    /// <summary>
    /// Returns a strongly-typed ID instance with the specified value.
    /// </summary>
    public static IStronglyTypedId Create(Type type, object value)
    {
        if (!Configuration.TypeMap.TryGetValue(type, out var _))
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
        return Configuration.TypeMap.TryGetValue(type, out var valueType)
            ? (IStronglyTypedId)Activator.CreateInstance(type, args: valueType.DefaultValue)!
            : throw new ArgumentException($"Unknown strongly-typed ID type: {type.FullName}");
    }

    /// <summary>
    /// Returns an empty instance of the strongly-typed ID type with the default value.
    /// </summary>
    public static TStronglyTypedId Empty<TStronglyTypedId>() where TStronglyTypedId : IStronglyTypedId
    {
        return (TStronglyTypedId)Empty(typeof(TStronglyTypedId));
    }

    /// <summary>
    /// Creates an empty instance of the specified strongly-typed ID type with the default value.
    /// </summary>
    public static TStronglyTypedId Empty<TStronglyTypedId, TValue>()
        where TStronglyTypedId : IStronglyTypedId<TValue>
        where TValue : struct, IEquatable<TValue>, IComparable<TValue>
    {
        return (TStronglyTypedId)Activator.CreateInstance(typeof(TStronglyTypedId), args: default(TValue))!;
    }

    /// <summary>
    /// Gets the underlying value type of the specified strongly-typed ID type.
    /// </summary>
    public static Type GetValueType<TStronglyTypedId>()
        where TStronglyTypedId : IStronglyTypedId
    {
        return Configuration.GetValueType(typeof(TStronglyTypedId));
    }
}