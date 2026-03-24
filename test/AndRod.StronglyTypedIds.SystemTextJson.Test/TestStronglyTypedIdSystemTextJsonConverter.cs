using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Shared;

namespace AndRod.StronglyTypedIds.SystemTextJson.Test;

[TestClass]
public sealed class TestStronglyTypedIdSystemTextJsonConverter
{
    private readonly IServiceProvider _provider;

    public TestStronglyTypedIdSystemTextJsonConverter()
    {
        var services = new ServiceCollection();
        services.AddStronglyTypedIds(configure =>
        {
            configure.Add<ByteId>();
        });
        services.AddStronlgyTypedIdJsonConverters(configure =>
        {
            configure.Add<ByteId>();
        },
        new CompoundJsonConverter());

        _provider = services.BuildServiceProvider();
    }

    [TestMethod]
    public void Read_Should_ReturnEmpty_WhenValueIsDefault()
    {
        var converter = _provider.GetRequiredService<StronglyTypedIdSystemTextJsonConverter<ByteId, byte>>();
        var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes("{\"value\":0,\"type\":\"Shared.ByteId\"}"));

        var result = converter.Read(ref reader, typeof(ByteId), JsonSerializerOptions.Web);

        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.Value);
    }

    [TestMethod]
    public void Read_Should_ReturnValue_WhenValueIsNotDefault()
    {
        var converter = _provider.GetRequiredService<StronglyTypedIdSystemTextJsonConverter<ByteId, byte>>();
        var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes("{\"value\":1,\"type\":\"Shared.ByteId\"}"));

        var result = converter.Read(ref reader, typeof(ByteId), JsonSerializerOptions.Web);

        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Value);
    }

    [TestMethod]
    [SuppressMessage("Usage", "MSTEST0051:Assert.Throws should contain only a single statement/expression", Justification = "Ref must be in same scope as reader.")]
    public void Read_Should_Throw_WhenTypeMismatch()
    {
        var converter = _provider.GetRequiredService<StronglyTypedIdSystemTextJsonConverter<ByteId, byte>>();

        Assert.Throws<JsonException>(() =>
            {
                var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes("{\"value\":1,\"type\":\"Shared.IntId\"}"));
                converter.Read(ref reader, typeof(IntId), JsonSerializerOptions.Web);
            });
    }

    [TestMethod]
    [SuppressMessage("Usage", "MSTEST0051:Assert.Throws should contain only a single statement/expression", Justification = "Ref must be in same scope as reader.")]
    public void Read_Should_Throw_Exception_WhenInvalidValueParsed()
    {
        var converter = _provider.GetRequiredService<StronglyTypedIdSystemTextJsonConverter<ByteId, byte>>();

        Assert.Throws<JsonException>(() =>
            {
                var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes("{\"value\":1337,\"type\":\"Shared.ByteId\"}"));
                var byteId = converter.Read(ref reader, typeof(ByteId), JsonSerializerOptions.Web);
            });
    }

    [TestMethod]
    public void Write_Should_WriteByteIDValue()
    {
        const byte expectedValue = 1;
        const string expectedJson = "{\"value\":1,\"type\":\"Shared.ByteId\"}";

        var converter = _provider.GetRequiredService<StronglyTypedIdSystemTextJsonConverter<ByteId, byte>>();
        using var stream = new MemoryStream();
        using var writer = new Utf8JsonWriter(stream);
        converter.Write(writer, new ByteId(expectedValue), JsonSerializerOptions.Web);
        writer.Flush();

        var json = Encoding.UTF8.GetString(stream.ToArray());
        Assert.AreEqual(expectedJson, json);
    }

    [TestMethod]
    public void Write_Should_WriteCompoundIdValue()
    {
        const string expectedJson = "{\"value\":{\"id1\":1,\"id2\":2},\"type\":\"Shared.CompoundId\"}";

        var converter = _provider.GetRequiredService<StronglyTypedIdSystemTextJsonConverter<CompoundId, Compound>>();
        using var stream = new MemoryStream();
        using var writer = new Utf8JsonWriter(stream);
        converter.Write(writer, new CompoundId(new Compound(1, 2)), JsonSerializerOptions.Web);
        writer.Flush();

        var json = Encoding.UTF8.GetString(stream.ToArray());
        Assert.AreEqual(expectedJson, json);
    }

    [TestMethod]
    public void Converters_Should_BeRegistered_WhenUsingInstanceOptions()
    {
        var options = _provider.GetRequiredService<JsonSerializerOptions>();
        var converters = options.Converters;

        Assert.AreNotEqual(0, converters.Count);
    }

    [TestMethod]
    public void Deserialize_ByteId_Should_ReturnValue()
    {
        const string json = "{\"value\":1,\"type\":\"Shared.ByteId\"}";
        var options = _provider.GetRequiredService<JsonSerializerOptions>();

        var result = JsonSerializer.Deserialize<ByteId>(json, options);

        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Value);
    }

    [TestMethod]
    public void Deserialize_CompoundId_Should_ReturnValue()
    {
        const string json = "{\"value\":{\"id1\":1,\"id2\":2},\"type\":\"Shared.CompoundId\"}";
        var options = _provider.GetRequiredService<JsonSerializerOptions>();

        var result = JsonSerializer.Deserialize<CompoundId>(json, options);

        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Value.Id1);
        Assert.AreEqual(2, result.Value.Id2);
    }
}
