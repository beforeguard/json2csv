using JsonToCsv.Services;

namespace JsonToCsv.Tests.Services
{
    public class JsonToCsvConverterServiceTests
    {
        private readonly JsonToCsvConverterService _jsonToCsvConverterService = new();

        [Fact]
        public void Convert_Should_ConvertToCsv()
        {
            var expectedCsv = "Name,Age\r\nJohn,30\r\nJane,25\r\n";
            var json = "[{\"Name\":\"John\",\"Age\":30},{\"Name\":\"Jane\",\"Age\":25}]";

            var result = _jsonToCsvConverterService.Convert(json);

            result.Should().Be(expectedCsv);
        }
    }
}
