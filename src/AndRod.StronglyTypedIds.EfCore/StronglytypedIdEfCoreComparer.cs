using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AndRod.StronglyTypedIds.EfCore;

/// <summary>
/// A value comparer for strongly typed IDs that can be used with Entity Framework Core.
/// </summary>
public sealed class StronglytypedIdEfCoreComparer<TStronglyTypedId>()
    : ValueComparer<TStronglyTypedId>(
            (x, y) => EqualityCheck(x, y),
            x => x.GetHashCode())
    where TStronglyTypedId : IStronglyTypedId
{
    private static bool EqualityCheck(TStronglyTypedId? x, TStronglyTypedId? y)
    {
        if (x is null || y is null)
        {
            return false;
        }

        return x.Equals(y);
    }
}