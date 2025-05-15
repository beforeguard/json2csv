using System.Text;
using System.Text.Json;

namespace JsonToCsv.Services
{
    public class JsonToCsvConverterService
    {
        public string Convert(string json)
        {
            var stringBuilder = new StringBuilder();

            var document = JsonDocument.Parse(json);

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
