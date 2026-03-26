using AndRod.StronglyTypedIds;

namespace Shared;

public sealed record ByteId(byte Value) : StronglyTypedId<ByteId, byte>(Value)
{
}

public sealed record ShortId(short Value) : StronglyTypedId<ShortId, short>(Value)
{
}

public sealed record UShortId(ushort Value) : StronglyTypedId<UShortId, ushort>(Value)
{
}

public sealed record IntId(int Value) : StronglyTypedId<IntId, int>(Value)
{
}

public sealed record UIntId(uint Value) : StronglyTypedId<UIntId, uint>(Value)
{
}

public sealed record LongId(long Value) : StronglyTypedId<LongId, long>(Value)
{
}

public sealed record ULongId(ulong Value) : StronglyTypedId<ULongId, ulong>(Value)
{
}

public sealed record GuidId(Guid Value) : StronglyTypedId<GuidId, Guid>(Value)
{
}
