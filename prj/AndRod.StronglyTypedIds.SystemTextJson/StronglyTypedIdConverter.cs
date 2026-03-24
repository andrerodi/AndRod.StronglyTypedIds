using System.Text.Json;
using System.Text.Json.Serialization;

namespace AndRod.StronglyTypedIds.SystemTextJson;

public sealed class StronglyTypedIdSystemTextJsonConverter<TStronglyTypedId, TValue> : JsonConverter<TStronglyTypedId>
    where TStronglyTypedId : IStronglyTypedId<TValue>, IStronglyTypedId
    where TValue : struct, IEquatable<TValue>, IComparable<TValue>
{
    private static readonly string _nameofValue = nameof(IStronglyTypedId<>.Value);
    private static readonly string _nameofType = nameof(IStronglyTypedId.Type);

    /// <summary>
    /// Reads and converts the JSON to a strongly-typed ID of type <typeparamref name="TStronglyTypedId"/>.
    /// </summary>
    public override TStronglyTypedId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        Type genericStrongType = typeof(TStronglyTypedId);
        if (typeToConvert.FullName != genericStrongType.FullName)
        {
            throw new JsonException($"Expected type '{typeof(TStronglyTypedId).FullName}' to match strongly-typed ID type '{typeToConvert.FullName}'");
        }

        using var document = JsonDocument.ParseValue(ref reader);
        var root = document.RootElement;
        var valueRoot = root.GetProperty(options.PropertyNamingPolicy?.ConvertName(_nameofValue) ?? _nameofValue);
        var type = root.GetProperty(options.PropertyNamingPolicy?.ConvertName(_nameofType) ?? _nameofType).GetString();

        if (type != genericStrongType.FullName)
        {
            throw new JsonException($"Expected type '{genericStrongType.FullName}' to match strongly-typed ID type '{type}'");
        }

        var value = valueRoot.Deserialize<TValue>(options);
        return StronglyTypedIdFactory.Create<TStronglyTypedId>(value);
    }

    /// <summary>
    /// Writes a strongly-typed ID of type <typeparamref name="TStronglyTypedId"/> to JSON.
    /// </summary>
    public override void Write(Utf8JsonWriter writer, TStronglyTypedId value, JsonSerializerOptions options)
    {
        TValue idValue = ((IStronglyTypedId<TValue>)value).Value;
        writer.WriteStartObject();
        writer.WritePropertyName(options.PropertyNamingPolicy?.ConvertName(_nameofValue) ?? _nameofValue);
        JsonSerializer.Serialize(writer, idValue, options);
        writer.WritePropertyName(options.PropertyNamingPolicy?.ConvertName(_nameofType) ?? _nameofType);
        writer.WriteStringValue(value.StronglyTypedIdType.FullName);
        writer.WriteEndObject();
    }
}
