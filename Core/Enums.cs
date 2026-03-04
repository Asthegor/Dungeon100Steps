namespace Dungeon100Steps.Core
{
    public enum HeroClass { Warrior, Mage, Thief }
    public enum Genre { Female, Male }
    public enum EventType { Combat, Treasure, Trap, Narrative, None }
    public enum Rarity { Elite = 10, Rare = 50, Uncommon = 200, Common = 600, Junk }
    public enum BonusType
    {
        Attack,
        Defense,
        Health,
        Mana,
        Poison,
        Fire,
        Ice,
        Bleed,
        Stunt,
        ResistFire,
        ResistStunt,
        ResistIce,
        ResistBleed,
        ResistPoison,
    }
    public enum ItemType { Weapon = 30, Armor = 55, Potion = 75, None }
    public enum TreasureType { Normal = 65, Trapped = 95, Rare = 100 }
    public enum Zone { Zone1 = 25, Zone2 = 50, Zone3 = 75, Zone4 = 99 }
    public enum AddToInventoryResult { Added, InventoryFull, StackLimitExcedeed, Error }

    public enum EventResult
    {
        None,
        Victory,
        Defeat,
        Fled
    }
    public enum TrapType
    {
        None,
        Pit,
        Darts,
        PoisonGas,
        PressurePlate,
        Max
    }
    public enum EventState { ShowingMessage, Action, ProcessingAction, ShowingResult, ShowingDamage }

}
