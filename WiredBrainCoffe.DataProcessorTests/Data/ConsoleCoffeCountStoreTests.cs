using WiredBrainCoffee.DataProcessor.Data;
using WiredBrainCoffee.DataProcessor.Model;

namespace WiredBrainCoffe.DataProcessor.Data;

public class ConsoleCoffeCountStoreTests
{

    [Fact]
    public void ShouldWriteToConsole()
    {
        // Arrange
        var item = new CoffeCountItem("Cappuccino", 5);
        var stringWriter = new StringWriter();
        var consoleCoffeCountStore = new ConsoleCoffeCountStore(stringWriter);

        // Act
        consoleCoffeCountStore.Save(item);

        // Assert
        var result = stringWriter.ToString();
        Assert.Equal($"{item.CoffeeType}:{item.Count}{Environment.NewLine}", result);
    }
}
