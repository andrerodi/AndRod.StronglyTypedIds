using AndRod.StronglyTypedIds;

namespace Shared;

public readonly struct Compound(int id1, int id2) : IEquatable<Compound>, IComparable<Compound>
{
    public int Id1 { get; } = id1;
    public int Id2 { get; } = id2;

    public int CompareTo(Compound other)
    {
        int result = Id1.CompareTo(other.Id1);
        if (result == 0)
        {
            result = Id2.CompareTo(other.Id2);
        }
        return result;
    }

    public bool Equals(Compound other)
    {
        return Id1 == other.Id1 && Id2 == other.Id2;
    }
}

public sealed record CompoundId(Compound value) : StronglyTypedId<CompoundId, Compound>(value)
{
}
