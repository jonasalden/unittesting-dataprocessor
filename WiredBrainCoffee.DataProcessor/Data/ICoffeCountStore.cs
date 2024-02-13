
using WiredBrainCoffee.DataProcessor.Model;

namespace WiredBrainCoffee.DataProcessor.Data;

public partial interface ICoffeCountStore
{
    void Save(CoffeCountItem coffeCountItem);
}
