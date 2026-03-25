using Shared;

namespace AndRod.StronglyTypedIds.Test;

[TestClass]
public class TestStronglyTypedId
{
    [TestMethod]
    public void Test_StaticCreateMethod_ShouldCreate_ByteId()
    {
        var id = ByteId.Create(1);
        Assert.AreEqual(1, id.Value);
        Assert.IsInstanceOfType<ByteId>(id);
    }

    [TestMethod]
    public void Test_StaticCreateMethod_ShouldCreate_ShortId()
    {
        var id = ShortId.Create(1);
        Assert.AreEqual(1, id.Value);
        Assert.IsInstanceOfType<ShortId>(id);
    }

    [TestMethod]
    public void Test_StaticCreateMethod_ShouldCreate_UShortId()
    {
        var id = UShortId.Create(1);
        Assert.AreEqual(1, id.Value);
        Assert.IsInstanceOfType<UShortId>(id);
    }

    [TestMethod]
    public void Test_StaticCreateMethod_ShouldCreate_IntId()
    {
        var id = IntId.Create(1);
        Assert.AreEqual(1, id.Value);
        Assert.IsInstanceOfType<IntId>(id);
    }

    [TestMethod]
    public void Test_StaticCreateMethod_ShouldCreate_UIntId()
    {
        var id = UIntId.Create(1);
        Assert.AreEqual((uint)1, id.Value);
        Assert.IsInstanceOfType<UIntId>(id);
    }

    [TestMethod]
    public void Test_StaticCreateMethod_ShouldCreate_LongId()
    {
        var id = LongId.Create(1);
        Assert.AreEqual(1, id.Value);
        Assert.IsInstanceOfType<LongId>(id);
    }

    [TestMethod]
    public void Test_StaticCreateMethod_ShouldCreate_ULongId()
    {
        var id = ULongId.Create(1);
        Assert.AreEqual((ulong)1, id.Value);
        Assert.IsInstanceOfType<ULongId>(id);
    }

    [TestMethod]
    public void Test_StaticCreateMethod_ShouldCreate_GuidId()
    {
        var value = Guid.NewGuid();
        var id = GuidId.Create(value);
        Assert.AreEqual(value, id.Value);
        Assert.IsInstanceOfType<GuidId>(id);
    }

    [TestMethod]
    public void Test_StaticCreateMethod_ShouldCreate_CompoundId()
    {
        var id = CompoundId.Create(new Compound(13, 37));
        Assert.AreEqual(new Compound(13, 37), id.Value);
        Assert.AreEqual(13, id.Value.Id1);
        Assert.AreEqual(37, id.Value.Id2);
        Assert.IsInstanceOfType<CompoundId>(id);
    }
}
