using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Shared;

namespace AndRod.StronglyTypedIds.SystemTextJson.Test;

[TestClass]
public class TestEntitiesJsonConversion
{
    private readonly JsonSerializerOptions _options;

    public TestEntitiesJsonConversion()
    {
        IServiceCollection services = new ServiceCollection();
        services.AddStronglyTypedIds(config =>
        {
            config.Add<ByteId>();
        });
        services.AddStronlgyTypedIdJsonConverters(config =>
        {
            config.Add<ByteId>();
        });

        var provider = services.BuildServiceProvider();
        _options = provider.GetRequiredService<JsonSerializerOptions>();
    }

    [TestMethod]
    public void Can_Convert_ByteEntity()
    {
        var entity = new ByteEntity();
        var json = JsonSerializer.Serialize(entity, _options);
        var deserialized = JsonSerializer.Deserialize<ByteEntity>(json);
        Assert.IsNotNull(deserialized);
        Assert.AreEqual(entity.Id, deserialized.Id);
    }

    [TestMethod]
    public void Can_Convert_ShortEntity()
    {
        var entity = new ShortEntity();
        var json = JsonSerializer.Serialize(entity, _options);
        var deserialized = JsonSerializer.Deserialize<ShortEntity>(json);
        Assert.IsNotNull(deserialized);
        Assert.AreEqual(entity.Id, deserialized.Id);
    }

    [TestMethod]
    public void Can_Convert_UShortEntity()
    {
        var entity = new UShortEntity();
        var json = JsonSerializer.Serialize(entity, _options);
        var deserialized = JsonSerializer.Deserialize<UShortEntity>(json);
        Assert.IsNotNull(deserialized);
        Assert.AreEqual(entity.Id, deserialized.Id);
    }

    [TestMethod]
    public void Can_Convert_IntEntity()
    {
        var entity = new IntEntity();
        var json = JsonSerializer.Serialize(entity, _options);
        var deserialized = JsonSerializer.Deserialize<IntEntity>(json);
        Assert.IsNotNull(deserialized);
        Assert.AreEqual(entity.Id, deserialized.Id);
    }

    [TestMethod]
    public void Can_Convert_UIntEntity()
    {
        var entity = new UIntEntity();
        var json = JsonSerializer.Serialize(entity, _options);
        var deserialized = JsonSerializer.Deserialize<UIntEntity>(json);
        Assert.IsNotNull(deserialized);
        Assert.AreEqual(entity.Id, deserialized.Id);
    }

    [TestMethod]
    public void Can_Convert_LongEntity()
    {
        var entity = new LongEntity();
        var json = JsonSerializer.Serialize(entity, _options);
        var deserialized = JsonSerializer.Deserialize<LongEntity>(json);
        Assert.IsNotNull(deserialized);
        Assert.AreEqual(entity.Id, deserialized.Id);
    }

    [TestMethod]
    public void Can_Convert_ULongEntity()
    {
        var entity = new ULongEntity();
        var json = JsonSerializer.Serialize(entity, _options);
        var deserialized = JsonSerializer.Deserialize<ULongEntity>(json);
        Assert.IsNotNull(deserialized);
        Assert.AreEqual(entity.Id, deserialized.Id);
    }

    [TestMethod]
    public void Can_Convert_GuidEntity()
    {
        var entity = new GuidEntity();
        var json = JsonSerializer.Serialize(entity, _options);
        var deserialized = JsonSerializer.Deserialize<GuidEntity>(json);
        Assert.IsNotNull(deserialized);
        Assert.AreEqual(entity.Id, deserialized.Id);
    }
}
