namespace AndRod.StronglyTypedIds;

/// <summary>
/// A strongly-typed ID that wraps a value of type <typeparamref name="TValue"/>.
/// </summary>
public abstract class StronglyTypedId<TSelf, TValue>(TValue value) : IStronglyTypedId<TValue>, IStronglyTypedId, IEquatable<StronglyTypedId<TSelf, TValue>>, IComparable<StronglyTypedId<TSelf, TValue>>
where TSelf : StronglyTypedId<TSelf, TValue>
where TValue : struct, IEquatable<TValue>, IComparable<TValue>
{
    /// <inheritdoc />
    public TValue Value { get; } = value;

    /// <inheritdoc />
    object IStronglyTypedId.Value => Value;

    /// <inheritdoc />
    public Type StronglyTypedIdType { get; } = typeof(TSelf);

    /// <inheritdoc />
    public Type Type { get; } = typeof(TValue);

    /// <inheritdoc />
    public bool IsTransient => Value.Equals(default);

    /// <inheritdoc />
    public bool Equals(StronglyTypedId<TSelf, TValue>? other)
    {
        if (other is null) return false;
        return Value.Equals(other.Value);
    }

    /// <inheritdoc />
    public int CompareTo(StronglyTypedId<TSelf, TValue>? other)
    {
        if (other is null) return 1;
        return Value.CompareTo(other.Value);
    }

    /// <summary>
    /// Compares two strongly-typed IDs for equality.
    /// </summary>
    public static bool operator ==(StronglyTypedId<TSelf, TValue> a, StronglyTypedId<TSelf, TValue> b) => a?.Equals(b) ?? false;

    /// <summary>
    /// Compares two strongly-typed IDs for inequality.
    /// </summary>
    public static bool operator !=(StronglyTypedId<TSelf, TValue> a, StronglyTypedId<TSelf, TValue> b) => !(a == b);

    /// <summary>
    /// Compares two strongly-typed IDs for equality.
    /// </summary>
    public override bool Equals(object? obj) => obj is StronglyTypedId<TSelf, TValue> other && this.Equals(other);

    /// <summary>
    /// Returns the hash code of the underlying value.
    /// </summary>
    public override int GetHashCode() => Value.GetHashCode();

    /// <summary>
    /// Creates a new strongly-typed ID of type <see cref="TSelf"/> with the given value.
    /// </summary>
    public static TSelf Create(TValue value) => StronglyTypedIdFactory.Create<TSelf, TValue>(value);

    /// <summary>
    /// Returns an empty strongly-typed ID of type <see cref="TSelf"/> with the default value.
    /// </summary>
    public static TSelf Empty() => StronglyTypedIdFactory.Empty<TSelf, TValue>();
}
