using Dungeon100Steps.Core.Datas.Items;

namespace Dungeon100Steps.Core.Datas.Loot
{
    public class LootFactory<T> where T : Item
    {
        private readonly List<LootEntry<T>> _table = new();
        private readonly Random _random = new();

        public void Add(int weight, Func<T> factory) => _table.Add(new LootEntry<T>(weight, factory));

        public T Roll()
        {
            int totalWeight = _table.Sum(e => e.Weight);
            int rnd = _random.Next(0, totalWeight);
            int cursor = 0;

            foreach (var entry in _table)
            {
                cursor += entry.Weight;
                if (rnd < cursor)
                    return entry.Factory();
            }

            return _table.First().Factory();
        }
    }
}
