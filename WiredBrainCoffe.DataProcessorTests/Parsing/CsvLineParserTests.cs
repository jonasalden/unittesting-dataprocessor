using WiredBrainCoffee.DataProcessor.Parsing;
namespace WiredBrainCoffe.DataProcessor.Parsing;

public class CsvLineParserTests
{
    [Fact]
    public void ShouldParseValidLine()
    {
        // Arrange
        string[] csvLines = new[] { "Cappuccino;10/27/2022 8:06:04 AM" };

        // Act
        var machineDataItems = CsvLineParser.Parse(csvLines);

        // Assert
        Assert.NotNull(machineDataItems); // Check if model is not null.
        Assert.Single(machineDataItems); // Check if model has only one item.
        Assert.Equal("Cappuccino", machineDataItems[0].CoffeeType); // Check if CoffeeType is correct.
        Assert.Equal(new DateTime(2022, 10, 27, 8, 6, 4), machineDataItems[0].CreatedAt); // Check if CreatedAt is correct.
    }

    [Fact]
    public void ShouldSkipEmptyLines()
    {
        // Arrange
        string[] csvLines = new[] { "", " " };

        // Act
        var machineDataItems = CsvLineParser.Parse(csvLines);

        // Assert
        Assert.NotNull(machineDataItems); // Check if model is not null.
        Assert.Empty(machineDataItems); // Check if model is empty
    }

    [InlineData("Cappucino", "Invalid line")]
    [InlineData("Cappucino;InvalidDateTime", "Invalid datetime in csv line")]
    [Theory]
    public void ShouldThrowExceptionForInvalidLine(string csvLine, string expectedMessagePrefix)
    {
        // Arrange
        string[] csvLines = new[] { csvLine };

        // Act and Assert
        var exception = Assert.Throws<Exception>(() => CsvLineParser.Parse(csvLines));

        Assert.Equal($"{expectedMessagePrefix}: {csvLine}", exception.Message);
    }
}
