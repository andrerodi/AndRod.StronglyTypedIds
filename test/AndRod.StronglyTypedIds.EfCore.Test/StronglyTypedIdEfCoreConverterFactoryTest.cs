using Microsoft.Extensions.DependencyInjection;

using Shared;

namespace AndRod.StronglyTypedIds.EfCore.Test;

[TestClass]
public sealed class StronglyTypedIdEfCoreConverterFactoryTest
{
    [ClassInitialize]
    public static void Initialize(TestContext testContext)
    {
        var services = new ServiceCollection();
        services.AddStronglyTypedIds(configure => configure.Add<ByteId>());
        services.BuildServiceProvider();
    }

    [TestMethod]
    public void Create_ByteIdConverter_Should_ReturnConverter()
    {
        var converter = StronglyTypedIdEfCoreConverterFactory.Create<ByteId>();

        Assert.IsNotNull(converter);
        Assert.IsInstanceOfType(converter, typeof(StronglyTypedIdEfCoreConverter<ByteId, byte>));
    }

    [TestMethod]
    public void Create_IntIdConverter_Should_ReturnConverter()
    {
        var converter = StronglyTypedIdEfCoreConverterFactory.Create<IntId>();

        Assert.IsNotNull(converter);
        Assert.IsInstanceOfType(converter, typeof(StronglyTypedIdEfCoreConverter<IntId, int>));
    }

    [TestMethod]
    public void Create_UIntIdConverter_Should_ReturnConverter()
    {
        var converter = StronglyTypedIdEfCoreConverterFactory.Create<UIntId>();

        Assert.IsNotNull(converter);
        Assert.IsInstanceOfType(converter, typeof(StronglyTypedIdEfCoreConverter<UIntId, uint>));
    }

    [TestMethod]
    public void Create_LongIdConverter_Should_ReturnConverter()
    {
        var converter = StronglyTypedIdEfCoreConverterFactory.Create<LongId>();

        Assert.IsNotNull(converter);
        Assert.IsInstanceOfType(converter, typeof(StronglyTypedIdEfCoreConverter<LongId, long>));
    }

    [TestMethod]
    public void Create_ULongIdConverter_Should_ReturnConverter()
    {
        var converter = StronglyTypedIdEfCoreConverterFactory.Create<ULongId>();

        Assert.IsNotNull(converter);
        Assert.IsInstanceOfType(converter, typeof(StronglyTypedIdEfCoreConverter<ULongId, ulong>));
    }

    [TestMethod]
    public void Create_GuidIdConverter_Should_ReturnConverter()
    {
        var converter = StronglyTypedIdEfCoreConverterFactory.Create<GuidId>();

        Assert.IsNotNull(converter);
        Assert.IsInstanceOfType(converter, typeof(StronglyTypedIdEfCoreConverter<GuidId, Guid>));
    }

    [TestMethod]
    public void Create_CompoundIdConverter_Should_ReturnConverter()
    {
        var converter = StronglyTypedIdEfCoreConverterFactory.Create<CompoundId>();

        Assert.IsNotNull(converter);
        Assert.IsInstanceOfType(converter, typeof(StronglyTypedIdEfCoreConverter<CompoundId, Compound>));
    }

    [TestMethod]
    public void Convert_ByteId_Should_ReturnOriginalValue()
    {
        var converter = StronglyTypedIdEfCoreConverterFactory.Create<ByteId>();
        var originalValue = new ByteId(123);
        var convertedValue = converter.ConvertToProvider(originalValue);

        Assert.AreEqual((byte)123, convertedValue);
    }

    [TestMethod]
    public void Convert_Byte_To_ByteId_Should_ReturnOriginalValue()
    {
        var converter = StronglyTypedIdEfCoreConverterFactory.Create<ByteId>();
        var originalValue = (byte)123;
        var convertedValue = converter.ConvertFromProvider(originalValue);

        Assert.AreEqual(new ByteId(123), convertedValue);
    }
}