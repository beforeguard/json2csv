using JsonToCsv.Services;

namespace JsonToCsv.Tests.Services
{
    public class JsonConverterServiceTests
    {
        private readonly JsonConverterService _jsonToCsvConverterService = new();

        [Fact]
        public void ConvertToCsv_Should_ConvertToCsv()
        {
            var expectedCsv = "Name,Age\r\nJohn,30\r\nJane,25\r\n";
            var json = "[{\"Name\":\"John\",\"Age\":30},{\"Name\":\"Jane\",\"Age\":25}]";

            var result = _jsonToCsvConverterService.ConvertToCsv(json);

            result.Should().Be(expectedCsv);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ConvertToCsv_Should_ThrowException_When_JsonIsNullOrEmpty(string? json)
        {
            var convert = () => _jsonToCsvConverterService.ConvertToCsv(json);

            convert
                .Should()
                .Throw<ArgumentException>()
                .WithMessage($"{nameof(json)} cannot be null or empty.*");
        }

        [Theory]
        [InlineData("Thisisnotvalidjson")]
        public void ConvertToCsv_Should_ThrowException_When_JsonIsInvalid(string? json)
        {
            var convert = () => _jsonToCsvConverterService.ConvertToCsv(json);

            convert
                .Should()
                .Throw<ArgumentException>()
                .WithMessage($"{nameof(json)} is not valid.*");
        }

        [Fact]
        public void ConvertToJson_Should_ThrowException_When_JsonContainsNestedObject()
        {
            var json = "[{\"Name\":\"John\",\"Age\":30,\"Object\":{}}]";

            var convert = () => _jsonToCsvConverterService.ConvertToCsv(json);

            convert
                .Should()
                .Throw<ArgumentException>()
                .WithMessage($"{nameof(json)} is cannot contain nested objects.*");
        }
    }
}
