using Dungeon100Steps.Core.Datas.Items;

namespace Dungeon100Steps.Core.Datas.Loot
{
    public class LootEntry<T>(int weight, Func<T> factory) where T : Item
    {
        public int Weight { get; set; } = weight;
        public Func<T> Factory { get; set; } = factory;
    }
}
