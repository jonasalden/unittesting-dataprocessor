using WiredBrainCoffee.DataProcessor.Data;
using WiredBrainCoffee.DataProcessor.Model;
using WiredBrainCoffee.DataProcessor.Processing;

namespace WiredBrainCoffe.DataProcessor.Processing;

public class MachineDataProcessorTests : IDisposable
{
    private readonly FakeCoffeCountStore _coffeCountStore;
    private readonly MachineDataProcessor _machineDataProcessor;

    public MachineDataProcessorTests()
    {
         _coffeCountStore = new FakeCoffeCountStore();
         _machineDataProcessor = new MachineDataProcessor(_coffeCountStore);
    }

    [Fact]
    public void ShouldSaveCountPerCoffeType()
    {
        // Arrange
        var items = new[]
        {
            new MachineDataItem ("Cappuccino",new DateTime(2022,10,27,8,0,0)),
            new MachineDataItem ("Cappuccino",new DateTime(2022,10,27,9,0,0)),
            new MachineDataItem ("Espresso", new DateTime(2022,10,27,10,0,0))
        };

        // Act
        _machineDataProcessor.ProcessItems(items);

        // Assert
        Assert.Equal(2, _coffeCountStore.SavedItems.Count);

        var item = _coffeCountStore.SavedItems[0];
        Assert.Equal("Cappuccino", item.CoffeeType);
        Assert.Equal(2, item.Count);

        item = _coffeCountStore.SavedItems[1];
        Assert.Equal("Espresso", item.CoffeeType);
        Assert.Equal(1, item.Count);


    }

    [Fact]
    public void ShouldClearPreviousCoffeCount()
    {
        // Arrange
        var items = new[]
        {
            new MachineDataItem ("Cappuccino",new DateTime(2022,10,27,8,0,0)),
        };

        // Act
        _machineDataProcessor.ProcessItems(items);
        _machineDataProcessor.ProcessItems(items);

        // Assert
        Assert.Equal(2, _coffeCountStore.SavedItems.Count);

        foreach (var item in _coffeCountStore.SavedItems)
        {
            Assert.Equal("Cappuccino", item.CoffeeType);
            Assert.Equal(1, item.Count);
        }
    }

    [Fact]
    public void ShouldIgnoreItemsThatAreNotNewer()
    {
        // Arrange
        var items = new[]
        {
            new MachineDataItem ("Cappuccino",new DateTime(2022,10,27,8,0,0)),
            new MachineDataItem ("Cappuccino",new DateTime(2022,10,27,7,0,0)),
            new MachineDataItem ("Cappuccino",new DateTime(2022,10,27,7,10,0)),
            new MachineDataItem ("Cappuccino",new DateTime(2022,10,27,9,0,0)),
            new MachineDataItem ("Espresso", new DateTime(2022,10,27,10,0,0)),
            new MachineDataItem ("Espresso", new DateTime(2022,10,27,10,0,0))
        };

        // Act
        _machineDataProcessor.ProcessItems(items);

        // Assert
        Assert.Equal(2, _coffeCountStore.SavedItems.Count);

        var item = _coffeCountStore.SavedItems[0];
        Assert.Equal("Cappuccino", item.CoffeeType);
        Assert.Equal(2, item.Count);

        item = _coffeCountStore.SavedItems[1];
        Assert.Equal("Espresso", item.CoffeeType);
        Assert.Equal(1, item.Count);




    }

    public void Dispose()
    {
       // This runs after every test
    }
}

public class FakeCoffeCountStore : ICoffeCountStore
{
    public List<CoffeCountItem> SavedItems { get; } = new();
    public void Save(CoffeCountItem item)
    {
        SavedItems.Add(item);
    }
}
