using DinaCSharp.Resources;

using Dungeon100Steps.Core.Datas.Enemies;
using Dungeon100Steps.Core.Datas.Items;
namespace Dungeon100Steps.Core.Datas.Events
{
    public class CombatEvent(Enemy enemy, int rewardGold, int rewardXP, Item? loot) : Event(EventType.Combat)
    {
        public Enemy Enemy { get; private set; } = enemy;
        public int RewardGold { get; private set; } = rewardGold;
        public int RewardXP { get; private set; } = rewardXP;
        public Item? Loot { get; private set; } = loot;
    }
}
