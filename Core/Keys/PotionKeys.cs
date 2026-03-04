using DinaCSharp.Resources;
using DinaCSharp.Services;

namespace Dungeon100Steps.Core.Keys
{
    public static class PotionKeys
    {
        public static readonly Key<ResourceTag> MinorHealth = Key<ResourceTag>.FromString("Potions/MinorHealth");
        public static readonly Key<ResourceTag> Health = Key<ResourceTag>.FromString("Potions/Health");
        public static readonly Key<ResourceTag> LargeHealth = Key<ResourceTag>.FromString("Potions/LargeHealth");
        public static readonly Key<ResourceTag> Mana = Key<ResourceTag>.FromString("Potions/Mana");
    }
}
