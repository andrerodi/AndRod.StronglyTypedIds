namespace AndRod.StronglyTypedIds;

/// <summary>
/// Factory class for creating strongly-typed IDs.
/// </summary>
public sealed class StronglyTypedIdFactory(StronglyTypedIdConfiguration configuration)
{
    private readonly StronglyTypedIdConfiguration _configuration = configuration;
    public StronglyTypedIdConfiguration Configuration => _configuration;

    /// <summary>
    /// Returns a strongly-typed ID instance with the specified value.
    /// </summary>
    public IStronglyTypedId Create(Type type, object value)
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
    public TStronglyTypedId Create<TStronglyTypedId>(object value) where TStronglyTypedId : IStronglyTypedId
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
    public IStronglyTypedId Empty(Type type)
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
    public TStronglyTypedId Empty<TStronglyTypedId>() where TStronglyTypedId : IStronglyTypedId
    {
        return (TStronglyTypedId)Empty(typeof(TStronglyTypedId));
    }

    internal static TStronglyTypedId Empty<TStronglyTypedId, TValue>()
        where TStronglyTypedId : IStronglyTypedId<TValue>
        where TValue : struct, IEquatable<TValue>, IComparable<TValue>
    {
        return (TStronglyTypedId)Activator.CreateInstance(typeof(TStronglyTypedId), args: default(TValue))!;
    }
}
