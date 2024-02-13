using WiredBrainCoffee.DataProcessor.Model;
namespace WiredBrainCoffee.DataProcessor.Data;

public class ConsoleCoffeCountStore : ICoffeCountStore
{
    private readonly TextWriter _textWriter;
    public ConsoleCoffeCountStore() : this(Console.Out) { }
    public ConsoleCoffeCountStore(TextWriter textWriter)
    {
        _textWriter = textWriter;
    }
    public void Save(CoffeCountItem item)
    {
        var line = $"{item.CoffeeType}:{item.Count}";
        _textWriter.WriteLine(line);
    }
}
