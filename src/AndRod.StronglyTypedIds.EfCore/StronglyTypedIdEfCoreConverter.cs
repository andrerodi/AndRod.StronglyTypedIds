using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AndRod.StronglyTypedIds.EfCore;

/// <summary>
/// A value converter for strongly typed IDs that can be used with Entity Framework Core.
/// </summary>
public sealed class StronglyTypedIdEfCoreConverter<TStronglyTypedId, TValue>() : ValueConverter<TStronglyTypedId, TValue>(
            x => x.Value,
            x => StronglyTypedId<TStronglyTypedId, TValue>.Create(x))
    where TStronglyTypedId : StronglyTypedId<TStronglyTypedId, TValue>
    where TValue : struct, IEquatable<TValue>, IComparable<TValue>
{
}