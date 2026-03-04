using DinaCSharp.Resources;
using DinaCSharp.Services;

namespace Dungeon100Steps.Core.Keys
{
    public static class BackgroundKeys
    {
        #region City
        public static readonly Key<ResourceTag> Herborist = Key<ResourceTag>.FromString("Backgrounds/Herborist");

        #endregion


        #region Dungeon
        public static readonly Key<ResourceTag> CombatRoom1 = Key<ResourceTag>.FromString("Backgrounds/Dungeon/CombatRoom1");
        public static readonly Key<ResourceTag> CombatRoom2 = Key<ResourceTag>.FromString("Backgrounds/Dungeon/CombatRoom2");
        public static readonly Key<ResourceTag> CombatRoom3 = Key<ResourceTag>.FromString("Backgrounds/Dungeon/CombatRoom3");
        public static readonly Key<ResourceTag> CombatRoom4 = Key<ResourceTag>.FromString("Backgrounds/Dungeon/CombatRoom4");

        public static readonly Key<ResourceTag> TrapRoom1 = Key<ResourceTag>.FromString("Backgrounds/Dungeon/TrapRoom1");
        public static readonly Key<ResourceTag> TrapRoom1_Default = Key<ResourceTag>.FromString("Backgrounds/Dungeon/TrapRoom1_Default");
        public static readonly Key<ResourceTag> TrapRoom2 = Key<ResourceTag>.FromString("Backgrounds/Dungeon/TrapRoom2");
        public static readonly Key<ResourceTag> TrapRoom2_Default = Key<ResourceTag>.FromString("Backgrounds/Dungeon/TrapRoom2_Default");

        public static readonly Key<ResourceTag> TreasureRoom1 = Key<ResourceTag>.FromString("Backgrounds/Dungeon/TreasureRoom1");
        public static readonly Key<ResourceTag> TreasureRoom2 = Key<ResourceTag>.FromString("Backgrounds/Dungeon/TreasureRoom2");
        #endregion

        public static readonly Key<ResourceTag> Defeat = Key<ResourceTag>.FromString("Backgrounds/Defeat");
    }
}
