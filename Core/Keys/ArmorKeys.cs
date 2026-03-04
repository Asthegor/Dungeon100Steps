using DinaCSharp.Resources;
using DinaCSharp.Services;

using System;
using System.Collections.Generic;
using System.Text;

namespace Dungeon100Steps.Core.Keys
{
    public static class ArmorKeys
    {
        public static readonly Key<ResourceTag> DefaultArmor = Key<ResourceTag>.FromString("Armors/DefaultArmor");

        // JUNK
        public static readonly Key<ResourceTag> Junk_BrokenChainmail = Key<ResourceTag>.FromString("Armors/Junk/BrokenChainmail");
        public static readonly Key<ResourceTag> Junk_CrackedBuckler = Key<ResourceTag>.FromString("Armors/Junk/CrackedBuckler");
        public static readonly Key<ResourceTag> Junk_DirtyCloak = Key<ResourceTag>.FromString("Armors/Junk/DirtyCloak");
        public static readonly Key<ResourceTag> Junk_ImprovisedHelmet = Key<ResourceTag>.FromString("Armors/Junk/ImprovisedHelmet");
        public static readonly Key<ResourceTag> Junk_PatchedPants = Key<ResourceTag>.FromString("Armors/Junk/PatchedPants");
        public static readonly Key<ResourceTag> Junk_RottenWoodShield = Key<ResourceTag>.FromString("Armors/Junk/RottenWoodShield");
        public static readonly Key<ResourceTag> Junk_RustyChestplate = Key<ResourceTag>.FromString("Armors/Junk/RustyChestplate");

        // COMMON
        public static readonly Key<ResourceTag> Common_BronzeBracers = Key<ResourceTag>.FromString("Armors/Common/BronzeBracers");
        public static readonly Key<ResourceTag> Common_ChainmailCoif = Key<ResourceTag>.FromString("Armors/Common/ChainmailCoif");
        public static readonly Key<ResourceTag> Common_GuardArmour = Key<ResourceTag>.FromString("Armors/Common/GuardArmour");
        public static readonly Key<ResourceTag> Common_IronHelmet = Key<ResourceTag>.FromString("Armors/Common/IronHelmet");
        public static readonly Key<ResourceTag> Common_LeatherVest = Key<ResourceTag>.FromString("Armors/Common/LeatherVest");
        public static readonly Key<ResourceTag> Common_LightShield = Key<ResourceTag>.FromString("Armors/Common/LightShield");
        public static readonly Key<ResourceTag> Common_PaddedJacket = Key<ResourceTag>.FromString("Armors/Common/PaddedJacket");
        public static readonly Key<ResourceTag> Common_ReinforcedGloves = Key<ResourceTag>.FromString("Armors/Common/ReinforcedGloves");
        public static readonly Key<ResourceTag> Common_SoldierBoots = Key<ResourceTag>.FromString("Armors/Common/SoldierBoots");

        // UNCOMMON
        public static readonly Key<ResourceTag> Uncommon_KnightGauntlets = Key<ResourceTag>.FromString("Armors/Uncommon/KnightGauntlets");
        public static readonly Key<ResourceTag> Uncommon_LeatherArmor = Key<ResourceTag>.FromString("Armors/Uncommon/LeatherArmor");
        public static readonly Key<ResourceTag> Uncommon_ScoutCloak = Key<ResourceTag>.FromString("Armors/Uncommon/ScoutCloak");
        public static readonly Key<ResourceTag> Uncommon_SteelBreastplate = Key<ResourceTag>.FromString("Armors/Uncommon/SteelBreastplate");
        public static readonly Key<ResourceTag> Uncommon_SteelHelmet = Key<ResourceTag>.FromString("Armors/Uncommon/SteelHelmet");

        // RARE
        public static readonly Key<ResourceTag> Rare_DragonScaleShield = Key<ResourceTag>.FromString("Armors/Rare/DragonScaleShield");
        public static readonly Key<ResourceTag> Rare_EnchantedRobe = Key<ResourceTag>.FromString("Armors/Rare/EnchantedRobe");
        public static readonly Key<ResourceTag> Rare_MithrilChainmail = Key<ResourceTag>.FromString("Armors/Rare/MithrilChainmail");

        // ELITE
        public static readonly Key<ResourceTag> Elite_AetherPlate = Key<ResourceTag>.FromString("Armors/Elite/AetherPlate");
        public static readonly Key<ResourceTag> Elite_PhoenixEmbrace = Key<ResourceTag>.FromString("Armors/Elite/PhoenixEmbrace");
        public static readonly Key<ResourceTag> Elite_VoidStalkerGarb = Key<ResourceTag>.FromString("Armors/Elite/VoidStalkerGarb");
    }
}
