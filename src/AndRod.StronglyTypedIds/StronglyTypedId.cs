using AndRod.Core;

namespace AndRod.StronglyTypedIds;

/// <summary>
/// A strongly-typed ID that wraps a value of type <typeparamref name="TValue"/>.
/// </summary>
public abstract record StronglyTypedId<TSelf, TValue>(TValue Value) :
    IStronglyTypedId<TValue>,
    IStronglyTypedId,
    IEquatable<TSelf>,
    IComparable<TSelf>,
    ITypedCloneable<TSelf>
    where TSelf : StronglyTypedId<TSelf, TValue>
    where TValue : struct, IEquatable<TValue>, IComparable<TValue>
{
    /// <inheritdoc />
    public TValue Value { get; } = Value;

    /// <inheritdoc />
    object IStronglyTypedId.Value => Value;

    /// <inheritdoc />
    public Type StronglyTypedIdType { get; } = typeof(TSelf);

    /// <inheritdoc />
    public Type Type { get; } = typeof(TValue);

    /// <inheritdoc />
    public bool IsTransient => Value.Equals(default);

    /// <inheritdoc />
    public virtual bool Equals(TSelf? other)
    {
        if (other is null) return false;
        return Value.Equals(other.Value);
    }

    /// <inheritdoc />
    public int CompareTo(TSelf? other)
    {
        if (other is null) return 1;
        return Value.CompareTo(other.Value);
    }

    /// <inheritdoc />
    TSelf ITypedCloneable<TSelf>.Clone() => StronglyTypedIdFactory.Create<TSelf, TValue>(Value);

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

    /// <summary>
    /// Returns a string representation of the strongly-typed ID in the format "<see cref="TSelf"/>: <see cref="Value"/>".
    /// </summary>
    public override string ToString() => $"{typeof(TSelf).Name}: {Value}";
}
