using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Shared;

namespace AndRod.StronglyTypedIds.Test;

[TestClass]
public sealed class TestStronglyTypedIdFactory
{
    private readonly IServiceProvider _provider;
    private readonly StronglyTypedIdFactory _factory;

    public TestStronglyTypedIdFactory()
    {
        ServiceCollection services = new();
        services.AddStronglyTypedIds(configure =>
        {
            configure.Add<ByteId>();
        });

        _provider = services.BuildServiceProvider();
        _factory = _provider.GetRequiredService<StronglyTypedIdFactory>();
    }

    private readonly HashSet<Type> _types = [];
    public IReadOnlyCollection<Type> Types => _types;

    [TestMethod]
    public void Create_ByteId_UsingGenerig_Should_CreateByteId()
    {
        const byte value = 1;
        var id = _factory.Create<ByteId>(value);
        Assert.IsNotNull(id);
        Assert.AreEqual(value, id.Value);
        Assert.AreEqual(value, ((IStronglyTypedId)id).Value);
        Assert.IsInstanceOfType<ByteId>(id);
        Assert.AreEqual(typeof(ByteId), id.StronglyTypedIdType);
    }

    [TestMethod]
    public void Create_ByteId_UsingType_Should_CreateByteId()
    {
        const byte value = 1;
        var id = _factory.Create(typeof(ByteId), value);
        Assert.IsNotNull(id);
        Assert.AreEqual(value, id.Value);
        Assert.AreEqual(value, ((IStronglyTypedId)id).Value);
        Assert.IsInstanceOfType<ByteId>(id);
        Assert.AreEqual(typeof(ByteId), id.StronglyTypedIdType);
    }

    [TestMethod]
    public void Create_ShortId_UsingGeneric_Should_CreateShortId()
    {
        const short value = 1;
        var id = _factory.Create<ShortId>(value);
        Assert.IsNotNull(id);
        Assert.AreEqual(value, id.Value);
        Assert.AreEqual(value, ((IStronglyTypedId)id).Value);
        Assert.IsInstanceOfType<ShortId>(id);
        Assert.AreEqual(typeof(ShortId), id.StronglyTypedIdType);
    }

    [TestMethod]
    public void Create_ShortId_UsingType_Should_CreateShortId()
    {
        const short value = 1;
        var id = _factory.Create(typeof(ShortId), value);
        Assert.IsNotNull(id);
        Assert.AreEqual(value, id.Value);
        Assert.AreEqual(value, ((IStronglyTypedId)id).Value);
        Assert.IsInstanceOfType<ShortId>(id);
        Assert.AreEqual(typeof(ShortId), id.StronglyTypedIdType);
    }

    [TestMethod]
    public void Create_UShortId_UsingGeneric_Should_CreateUShortId()
    {
        const ushort value = 1;
        var id = _factory.Create<UShortId>(value);
        Assert.IsNotNull(id);
        Assert.AreEqual(value, id.Value);
        Assert.AreEqual(value, ((IStronglyTypedId)id).Value);
        Assert.IsInstanceOfType<UShortId>(id);
        Assert.AreEqual(typeof(UShortId), id.StronglyTypedIdType);
    }

    [TestMethod]
    public void Create_UShortId_UsingType_Should_CreateUShortId()
    {
        const ushort value = 1;
        var id = _factory.Create(typeof(UShortId), value);
        Assert.IsNotNull(id);
        Assert.AreEqual(value, id.Value);
        Assert.AreEqual(value, ((IStronglyTypedId)id).Value);
        Assert.IsInstanceOfType<UShortId>(id);
        Assert.AreEqual(typeof(UShortId), id.StronglyTypedIdType);
    }

    [TestMethod]
    public void Create_IntId_UsingGeneric_Should_CreateIntId()
    {
        const int value = 1;
        var id = _factory.Create<IntId>(value);
        Assert.IsNotNull(id);
        Assert.AreEqual(value, id.Value);
        Assert.AreEqual(value, ((IStronglyTypedId)id).Value);
        Assert.IsInstanceOfType<IntId>(id);
        Assert.AreEqual(typeof(IntId), id.StronglyTypedIdType);
    }

    [TestMethod]
    public void Create_IntId_UsingType_Should_Create()
    {
        const int value = 1;
        var id = _factory.Create(typeof(IntId), value);
        Assert.IsNotNull(id);
        Assert.AreEqual(value, id.Value);
        Assert.AreEqual(value, ((IStronglyTypedId)id).Value);
        Assert.IsInstanceOfType<IntId>(id);
        Assert.AreEqual(typeof(IntId), id.StronglyTypedIdType);
    }

    [TestMethod]
    public void Create_UIntId_UsingGeneric_Should_CreateUIntId()
    {
        const uint value = 1;
        var id = _factory.Create<UIntId>(value);
        Assert.IsNotNull(id);
        Assert.AreEqual(value, id.Value);
        Assert.AreEqual(value, ((IStronglyTypedId)id).Value);
        Assert.IsInstanceOfType<UIntId>(id);
        Assert.AreEqual(typeof(UIntId), id.StronglyTypedIdType);
    }

    [TestMethod]
    public void Create_UIntId_UsingType_Should_CreateUIntId()
    {
        const uint value = 1;
        var id = _factory.Create(typeof(UIntId), value);
        Assert.IsNotNull(id);
        Assert.AreEqual(value, id.Value);
        Assert.AreEqual(value, ((IStronglyTypedId)id).Value);
        Assert.IsInstanceOfType<UIntId>(id);
        Assert.AreEqual(typeof(UIntId), id.StronglyTypedIdType);
    }

    [TestMethod]
    public void Create_LongId_UsingGeneric_Should_CreateLongId()
    {
        const long value = 1;
        var id = _factory.Create<LongId>(value);
        Assert.IsNotNull(id);
        Assert.AreEqual(value, id.Value);
        Assert.AreEqual(value, ((IStronglyTypedId)id).Value);
        Assert.IsInstanceOfType<LongId>(id);
        Assert.AreEqual(typeof(LongId), id.StronglyTypedIdType);
    }

    [TestMethod]
    public void Create_LongId_UsingType_Should_CreateLongId()
    {
        const long value = 1;
        var id = _factory.Create(typeof(LongId), value);
        Assert.IsNotNull(id);
        Assert.AreEqual(value, id.Value);
        Assert.AreEqual(value, ((IStronglyTypedId)id).Value);
        Assert.IsInstanceOfType<LongId>(id);
        Assert.AreEqual(typeof(LongId), id.StronglyTypedIdType);
    }

    [TestMethod]
    public void Create_ULongId_UsingGeneric_Should_CreateULongId()
    {
        const ulong value = 1;
        var id = _factory.Create<ULongId>(value);
        Assert.IsNotNull(id);
        Assert.AreEqual(value, id.Value);
        Assert.AreEqual(value, ((IStronglyTypedId)id).Value);
        Assert.IsInstanceOfType<ULongId>(id);
        Assert.AreEqual(typeof(ULongId), id.StronglyTypedIdType);
    }

    [TestMethod]
    public void Create_ULongId_UsingType_Should_CreateULongId()
    {
        const ulong value = 1;
        var id = _factory.Create(typeof(ULongId), value);
        Assert.IsNotNull(id);
        Assert.AreEqual(value, id.Value);
        Assert.AreEqual(value, ((IStronglyTypedId)id).Value);
        Assert.IsInstanceOfType<ULongId>(id);
        Assert.AreEqual(typeof(ULongId), id.StronglyTypedIdType);
    }

    [TestMethod]
    public void Create_GuidId_UsingGeneric_Should_Create()
    {
        Guid value = Guid.NewGuid();
        var id = _factory.Create<GuidId>(value);
        Assert.IsNotNull(id);
        Assert.AreEqual(value, id.Value);
        Assert.AreEqual(value, ((IStronglyTypedId)id).Value);
        Assert.IsInstanceOfType<GuidId>(id);
        Assert.AreEqual(typeof(GuidId), id.StronglyTypedIdType);
    }

    [TestMethod]
    public void Create_GuidId_UsingType_Should_Create()
    {
        Guid value = Guid.NewGuid();
        var id = _factory.Create(typeof(GuidId), value);
        Assert.IsNotNull(id);
        Assert.AreEqual(value, id.Value);
        Assert.AreEqual(value, ((IStronglyTypedId)id).Value);
        Assert.IsInstanceOfType<GuidId>(id);
        Assert.AreEqual(typeof(GuidId), id.StronglyTypedIdType);
    }

    [TestMethod]
    public void Create_GuidV7Id_UsingGeneric_Should_Create()
    {
        Guid value = Guid.CreateVersion7();
        var id = _factory.Create<GuidId>(value);
        Assert.IsNotNull(id);
        Assert.AreEqual(value, id.Value);
        Assert.AreEqual(value, ((IStronglyTypedId)id).Value);
        Assert.IsInstanceOfType<GuidId>(id);
        Assert.AreEqual(typeof(GuidId), id.StronglyTypedIdType);
    }

    [TestMethod]
    public void Create_GuidV7Id_UsingType_Should_Create()
    {
        Guid value = Guid.CreateVersion7();
        var id = _factory.Create(typeof(GuidId), value);
        Assert.IsNotNull(id);
        Assert.AreEqual(value, id.Value);
        Assert.AreEqual(value, ((IStronglyTypedId)id).Value);
        Assert.IsInstanceOfType<GuidId>(id);
        Assert.AreEqual(typeof(GuidId), id.StronglyTypedIdType);
    }

    [TestMethod]
    public void Empty_ByteId_UsingGeneric_Should_CreateEmpty()
    {
        var id = _factory.Empty<ByteId>();
        Assert.IsNotNull(id);
        Assert.AreEqual(default(byte), id.Value);
        Assert.AreEqual(default(byte), ((IStronglyTypedId)id).Value);
        Assert.IsInstanceOfType<ByteId>(id);
        Assert.AreEqual(typeof(ByteId), id.StronglyTypedIdType);
    }

    [TestMethod]
    public void Empty_ByteId_UsingType_Should_CreateEmpty()
    {
        var id = _factory.Empty(typeof(ByteId));
        Assert.IsNotNull(id);
        Assert.AreEqual(default(byte), id.Value);
        Assert.AreEqual(default(byte), ((IStronglyTypedId)id).Value);
        Assert.IsInstanceOfType<ByteId>(id);
        Assert.AreEqual(typeof(ByteId), id.StronglyTypedIdType);
    }

    [TestMethod]
    public void Empty_ShortId_UsingGeneric_Should_CreateEmpty()
    {
        var id = _factory.Empty<ShortId>();
        Assert.IsNotNull(id);
        Assert.AreEqual(default(short), id.Value);
        Assert.AreEqual(default(short), ((IStronglyTypedId)id).Value);
        Assert.IsInstanceOfType<ShortId>(id);
        Assert.AreEqual(typeof(ShortId), id.StronglyTypedIdType);
    }

    [TestMethod]
    public void Empty_ShortId_UsingType_Should_CreateEmpty()
    {
        var id = _factory.Empty(typeof(ShortId));
        Assert.IsNotNull(id);
        Assert.AreEqual(default(short), id.Value);
        Assert.AreEqual(default(short), ((IStronglyTypedId)id).Value);
        Assert.IsInstanceOfType<ShortId>(id);
        Assert.AreEqual(typeof(ShortId), id.StronglyTypedIdType);
    }

    [TestMethod]
    public void Empty_UShortId_UsingGeneric_Should_CreateEmpty()
    {
        var id = _factory.Empty<UShortId>();
        Assert.IsNotNull(id);
        Assert.AreEqual(default(ushort), id.Value);
        Assert.AreEqual(default(ushort), ((IStronglyTypedId)id).Value);
        Assert.IsInstanceOfType<UShortId>(id);
        Assert.AreEqual(typeof(UShortId), id.StronglyTypedIdType);
    }

    [TestMethod]
    public void Empty_UShortId_UsingType_Should_CreateEmpty()
    {
        var id = _factory.Empty(typeof(UShortId));
        Assert.IsNotNull(id);
        Assert.AreEqual(default(ushort), id.Value);
        Assert.AreEqual(default(ushort), ((IStronglyTypedId)id).Value);
        Assert.IsInstanceOfType<UShortId>(id);
        Assert.AreEqual(typeof(UShortId), id.StronglyTypedIdType);
    }

    [TestMethod]
    public void Empty_IntId_UsingGeneric_Should_CreateEmpty()
    {
        var id = _factory.Empty<IntId>();
        Assert.IsNotNull(id);
        Assert.AreEqual(default(int), id.Value);
        Assert.AreEqual(default(int), ((IStronglyTypedId)id).Value);
        Assert.IsInstanceOfType<IntId>(id);
        Assert.AreEqual(typeof(IntId), id.StronglyTypedIdType);
    }

    [TestMethod]
    public void Empty_IntId_UsingType_Should_CreateEmpty()
    {
        var id = _factory.Empty(typeof(IntId));
        Assert.IsNotNull(id);
        Assert.AreEqual(default(int), id.Value);
        Assert.AreEqual(default(int), ((IStronglyTypedId)id).Value);
        Assert.IsInstanceOfType<IntId>(id);
        Assert.AreEqual(typeof(IntId), id.StronglyTypedIdType);
    }

    [TestMethod]
    public void Empty_UIntId_UsingGeneric_Should_CreateEmpty()
    {
        var id = _factory.Empty<UIntId>();
        Assert.IsNotNull(id);
        Assert.AreEqual(default(uint), id.Value);
        Assert.AreEqual(default(uint), ((IStronglyTypedId)id).Value);
        Assert.IsInstanceOfType<UIntId>(id);
        Assert.AreEqual(typeof(UIntId), id.StronglyTypedIdType);
    }

    [TestMethod]
    public void Empty_UIntId_UsingType_Should_CreateEmpty()
    {
        var id = _factory.Empty(typeof(UIntId));
        Assert.IsNotNull(id);
        Assert.AreEqual(default(uint), id.Value);
        Assert.AreEqual(default(uint), ((IStronglyTypedId)id).Value);
        Assert.IsInstanceOfType<UIntId>(id);
        Assert.AreEqual(typeof(UIntId), id.StronglyTypedIdType);
    }

    [TestMethod]
    public void Empty_LongId_UsingGeneric_Should_CreateEmpty()
    {
        var id = _factory.Empty<LongId>();
        Assert.IsNotNull(id);
        Assert.AreEqual(default(long), id.Value);
        Assert.AreEqual(default(long), ((IStronglyTypedId)id).Value);
        Assert.IsInstanceOfType<LongId>(id);
        Assert.AreEqual(typeof(LongId), id.StronglyTypedIdType);
    }

    [TestMethod]
    public void Empty_LongId_UsingType_Should_CreateEmpty()
    {
        var id = _factory.Empty(typeof(LongId));
        Assert.IsNotNull(id);
        Assert.AreEqual(default(long), id.Value);
        Assert.AreEqual(default(long), ((IStronglyTypedId)id).Value);
        Assert.IsInstanceOfType<LongId>(id);
        Assert.AreEqual(typeof(LongId), id.StronglyTypedIdType);
    }

    [TestMethod]
    public void Empty_ULongId_UsingGeneric_Should_CreateEmpty()
    {
        var id = _factory.Empty<ULongId>();
        Assert.IsNotNull(id);
        Assert.AreEqual(default(ulong), id.Value);
        Assert.AreEqual(default(ulong), ((IStronglyTypedId)id).Value);
        Assert.IsInstanceOfType<ULongId>(id);
        Assert.AreEqual(typeof(ULongId), id.StronglyTypedIdType);
    }

    [TestMethod]
    public void Empty_ULongId_UsingType_Should_CreateEmpty()
    {
        var id = _factory.Empty(typeof(ULongId));
        Assert.IsNotNull(id);
        Assert.AreEqual(default(ulong), id.Value);
        Assert.AreEqual(default(ulong), ((IStronglyTypedId)id).Value);
        Assert.IsInstanceOfType<ULongId>(id);
        Assert.AreEqual(typeof(ULongId), id.StronglyTypedIdType);
    }

    [TestMethod]
    public void Empty_GuidId_UsingGeneric_Should_CreateEmpty()
    {
        var id = _factory.Empty<GuidId>();
        Assert.IsNotNull(id);
        Assert.AreEqual(default(Guid), id.Value);
        Assert.AreEqual(default(Guid), ((IStronglyTypedId)id).Value);
        Assert.IsInstanceOfType<GuidId>(id);
        Assert.AreEqual(typeof(GuidId), id.StronglyTypedIdType);
    }

    [TestMethod]
    public void Empty_GuidId_UsingType_Should_CreateEmpty()
    {
        var id = _factory.Empty(typeof(GuidId));
        Assert.IsNotNull(id);
        Assert.AreEqual(default(Guid), id.Value);
        Assert.AreEqual(default(Guid), ((IStronglyTypedId)id).Value);
        Assert.IsInstanceOfType<GuidId>(id);
        Assert.AreEqual(typeof(GuidId), id.StronglyTypedIdType);
    }

    [TestMethod]
    public void Empty_GuidV7Id_UsingGeneric_Should_CreateEmpty()
    {
        var id = _factory.Empty<GuidId>();
        Assert.IsNotNull(id);
        Assert.AreEqual(default, id.Value);
        Assert.AreEqual(default(Guid), ((IStronglyTypedId)id).Value);
        Assert.IsInstanceOfType<GuidId>(id);
        Assert.AreEqual(typeof(GuidId), id.StronglyTypedIdType);
    }

    [TestMethod]
    public void Empty_GuidV7Id_UsingType_Should_CreateEmpty()
    {
        var id = _factory.Empty(typeof(GuidId));
        Assert.IsNotNull(id);
        Assert.AreEqual(default(Guid), id.Value);
        Assert.AreEqual(default(Guid), ((IStronglyTypedId)id).Value);
        Assert.IsInstanceOfType<GuidId>(id);
        Assert.AreEqual(typeof(GuidId), id.StronglyTypedIdType);
    }

    [TestMethod]
    public void Create_CompoundId_UsingGeneric_Should_Create()
    {
        Compound value = new(1, 2);
        var id = _factory.Create<CompoundId>(value);
        Assert.IsNotNull(id);
        Assert.AreEqual(value, id.Value);
        Assert.AreEqual(value, ((IStronglyTypedId)id).Value);
        Assert.IsInstanceOfType<CompoundId>(id);
        Assert.AreEqual(typeof(CompoundId), id.StronglyTypedIdType);
    }

    [TestMethod]
    public void Create_CompoundId_UsingType_Should_Create()
    {
        Compound value = new(1, 2);
        var id = _factory.Create(typeof(CompoundId), value);
        Assert.IsNotNull(id);
        Assert.AreEqual(value, id.Value);
        Assert.AreEqual(value, ((IStronglyTypedId)id).Value);
        Assert.IsInstanceOfType<CompoundId>(id);
        Assert.AreEqual(typeof(CompoundId), id.StronglyTypedIdType);
    }

    [TestMethod]
    public void Empty_CompoundId_UsingGeneric_Should_CreateEmpty()
    {
        var id = _factory.Empty<CompoundId>();
        Assert.IsNotNull(id);
        Assert.AreEqual(default(Compound), id.Value);
        Assert.AreEqual(default(Compound), ((IStronglyTypedId)id).Value);
        Assert.AreEqual(0, id.Value.Id1);
        Assert.AreEqual(0, id.Value.Id2);
        Assert.IsInstanceOfType<CompoundId>(id);
        Assert.AreEqual(typeof(CompoundId), id.StronglyTypedIdType);
    }

    [TestMethod]
    public void Empty_CompoundId_UsingType_Should_CreateEmpty()
    {
        var id = _factory.Empty(typeof(CompoundId));
        Assert.IsNotNull(id);
        Assert.AreEqual(default(Compound), id.Value);
        Assert.AreEqual(default(Compound), ((IStronglyTypedId)id).Value);
        Assert.AreEqual(0, ((IStronglyTypedId<Compound>)id).Value.Id1);
        Assert.AreEqual(0, ((IStronglyTypedId<Compound>)id).Value.Id2);
        Assert.IsInstanceOfType<CompoundId>(id);
        Assert.AreEqual(typeof(CompoundId), id.StronglyTypedIdType);
    }
}
