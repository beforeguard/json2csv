using System.Text;
using System.Text.Json;

namespace JsonToCsv.Services
{
    public class JsonConverterService
    {
        public string ConvertToCsv(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                throw new ArgumentException(
                    paramName: nameof(json),
                    message: $"{nameof(json)} cannot be null or empty.");
            }

            var stringBuilder = new StringBuilder();

            JsonDocument document;
            try
            {
                document = JsonDocument.Parse(json);
            }
            catch
            {
                throw new ArgumentException(
                    paramName: nameof(json),
                    message: $"{nameof(json)} is not valid.");
            }

            if (document.RootElement.ValueKind == JsonValueKind.Array)
            {
                foreach (var element in document.RootElement.EnumerateArray())
                {
                    if (element.ValueKind == JsonValueKind.Object)
                    {
                        var properties = element.EnumerateObject().ToList();
                        if (stringBuilder.Length == 0)
                        {
                            stringBuilder.AppendLine(string.Join(",", properties.Select(p => p.Name)));
                        }

                        stringBuilder.AppendLine(string.Join(",", properties.Select(p => p.Value.ToString())));
                    }
                }
            }

            return stringBuilder.ToString();
        }
    }
}
