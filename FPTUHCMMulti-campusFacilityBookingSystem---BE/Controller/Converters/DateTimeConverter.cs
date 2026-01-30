using System.Text.Json;
using System.Text.Json.Serialization;

namespace Controller.Converters
{
    /// <summary>
    /// Custom JSON converter để format DateTime theo định dạng "dd/MM/yyyy HH:mm:ss"
    /// </summary>
    public class DateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Hỗ trợ đọc cả format ISO 8601 và format custom
            if (reader.TokenType == JsonTokenType.String)
            {
                var dateString = reader.GetString();
                if (DateTime.TryParse(dateString, out var date))
                {
                    return date;
                }
            }
            throw new JsonException($"Không thể parse DateTime: {reader.GetString()}");
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            // Format: "10/12/2025 09:10:11"
            writer.WriteStringValue(value.ToString("dd/MM/yyyy HH:mm:ss"));
        }
    }

    /// <summary>
    /// Custom JSON converter cho DateTime? (nullable)
    /// </summary>
    public class NullableDateTimeConverter : JsonConverter<DateTime?>
    {
        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }

            if (reader.TokenType == JsonTokenType.String)
            {
                var dateString = reader.GetString();
                if (string.IsNullOrEmpty(dateString))
                {
                    return null;
                }
                if (DateTime.TryParse(dateString, out var date))
                {
                    return date;
                }
            }
            throw new JsonException($"Không thể parse DateTime?: {reader.GetString()}");
        }

        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNullValue();
            }
            else
            {
                // Format: "10/12/2025 09:10:11"
                writer.WriteStringValue(value.Value.ToString("dd/MM/yyyy HH:mm:ss"));
            }
        }
    }
}

