namespace Shared;

public sealed class ByteEntity
{
    public ByteId Id { get; set; } = ByteId.Empty();
    public string Name { get; set; } = nameof(ByteEntity);
}

public sealed class ShortEntity
{
    public ShortId Id { get; set; } = ShortId.Empty();
    public string Name { get; set; } = nameof(ShortEntity);
}

public sealed class UShortEntity
{
    public UShortId Id { get; set; } = UShortId.Empty();
    public string Name { get; set; } = nameof(UShortEntity);
}

public sealed class IntEntity
{
    public IntId Id { get; set; } = IntId.Empty();
    public string Name { get; set; } = nameof(IntEntity);
}

public sealed class UIntEntity
{
    public UIntId Id { get; set; } = UIntId.Empty();
    public string Name { get; set; } = nameof(UIntEntity);
}

public sealed class LongEntity
{
    public LongId Id { get; set; } = LongId.Empty();
    public string Name { get; set; } = nameof(LongEntity);
}

public sealed class ULongEntity
{
    public ULongId Id { get; set; } = ULongId.Empty();
    public string Name { get; set; } = nameof(ULongEntity);
}

public sealed class GuidEntity
{
    public GuidId Id { get; set; } = GuidId.Empty();
    public string Name { get; set; } = nameof(GuidEntity);
}
