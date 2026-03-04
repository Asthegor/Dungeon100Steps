using Dungeon100Steps.Core.Datas.Items;

namespace Dungeon100Steps.Core.Datas.Events
{
    public class TreasureEvent(Item? loot, int gold = 0, TrapEvent? trap = null, string description = "") : Event(EventType.Treasure, description)
    {
        public TrapEvent? Trap { get; set; } = trap;
        public Item? Loot { get; set; } = loot;
        public int Gold { get; set; } = gold;
    }
}
