using System.Text.Json;
using System.Text.Json.Serialization;
using Shared;

namespace AndRod.StronglyTypedIds.SystemTextJson.Test;

public sealed class CompoundJsonConverter : JsonConverter<Compound>
{
    public override Compound Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException("Expected start of object");
        }

        int id1 = 0, id2 = 0;
        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
            {
                break;
            }

            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException("Expected property name");
            }

            var propertyName = reader.GetString();
            reader.Read();

            switch (propertyName)
            {
                case "id1":
                    id1 = reader.GetInt32();
                    break;
                case "id2":
                    id2 = reader.GetInt32();
                    break;
            }
        }

        return new Compound(id1, id2);
    }

    public override void Write(Utf8JsonWriter writer, Compound value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteNumber("id1", value.Id1);
        writer.WriteNumber("id2", value.Id2);
        writer.WriteEndObject();
    }
}
