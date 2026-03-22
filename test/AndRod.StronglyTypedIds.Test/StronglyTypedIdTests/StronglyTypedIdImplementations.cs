using AndRod.StronglyTypedIds;

namespace AndRod.StronglyTypedIds.Test.StronglyTypedIdTests;

public class ByteId(byte value) : StronglyTypedId<ByteId, byte>(value)
{
}

public class ShortId(short value) : StronglyTypedId<ShortId, short>(value)
{
}

public class UShortId(ushort value) : StronglyTypedId<UShortId, ushort>(value)
{
}

public class IntId(int value) : StronglyTypedId<IntId, int>(value)
{
}

public class UIntId(uint value) : StronglyTypedId<UIntId, uint>(value)
{
}

public class LongId(long value) : StronglyTypedId<LongId, long>(value)
{
}

public class ULongId(ulong value) : StronglyTypedId<ULongId, ulong>(value)
{
}

public class GuidId(Guid value) : StronglyTypedId<GuidId, Guid>(value)
{
}
