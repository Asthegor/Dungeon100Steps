using DinaCSharp.Resources;
using DinaCSharp.Services;


namespace Dungeon100Steps.Core.Keys
{
    public static class WeaponKeys
    {
        public static readonly Key<ResourceTag> DefaultWeapon = Key<ResourceTag>.FromString("Weapons/DefaultWeapon");

        // JUNK
        public static readonly Key<ResourceTag> Junk_BendPoker = Key<ResourceTag>.FromString("Weapons/Junk/BendPoker");
        public static readonly Key<ResourceTag> Junk_BrokenPickaxeHandle = Key<ResourceTag>.FromString("Weapons/Junk/BrokenPickaxeHandle");
        public static readonly Key<ResourceTag> Junk_DullKitchenKnife = Key<ResourceTag>.FromString("Weapons/Junk/DullKitchenKnife");
        public static readonly Key<ResourceTag> Junk_LargeMammalBone = Key<ResourceTag>.FromString("Weapons/Junk/LargeMammalBone");
        public static readonly Key<ResourceTag> Junk_PotLid = Key<ResourceTag>.FromString("Weapons/Junk/PotLid");
        public static readonly Key<ResourceTag> Junk_RustyDagger = Key<ResourceTag>.FromString("Weapons/Junk/RustyDagger");
        public static readonly Key<ResourceTag> Junk_SharpenedBranch = Key<ResourceTag>.FromString("Weapons/Junk/SharpenedBranch");
        public static readonly Key<ResourceTag> Junk_StoneClub = Key<ResourceTag>.FromString("Weapons/Junk/StoneClub");
        public static readonly Key<ResourceTag> Junk_WoodenTrainingSword = Key<ResourceTag>.FromString("Weapons/Junk/WoodenTrainingSword");

        // COMMON
        public static readonly Key<ResourceTag> Common_BasicMorningstar = Key<ResourceTag>.FromString("Weapons/Common/BasicMorningstar");
        public static readonly Key<ResourceTag> Common_GuardSpear = Key<ResourceTag>.FromString("Weapons/Common/GuardSpear");
        public static readonly Key<ResourceTag> Common_HuntingLongbow = Key<ResourceTag>.FromString("Weapons/Common/HuntingLongbow");
        public static readonly Key<ResourceTag> Common_InfantryPike = Key<ResourceTag>.FromString("Weapons/Common/InfantryPike");
        public static readonly Key<ResourceTag> Common_IronGladius = Key<ResourceTag>.FromString("Weapons/Common/IronGladius");
        public static readonly Key<ResourceTag> Common_IronShodStaff = Key<ResourceTag>.FromString("Weapons/Common/IronShodStaff");
        public static readonly Key<ResourceTag> Common_LightRapier = Key<ResourceTag>.FromString("Weapons/Common/LightRapier");
        public static readonly Key<ResourceTag> Common_MilitiaShortsword = Key<ResourceTag>.FromString("Weapons/Common/MilitiaShortsword");
        public static readonly Key<ResourceTag> Common_NomadScimitar = Key<ResourceTag>.FromString("Weapons/Common/NomadScimitar");
        public static readonly Key<ResourceTag> Common_PeasantBillhook = Key<ResourceTag>.FromString("Weapons/Common/PeasantBillhook");
        public static readonly Key<ResourceTag> Common_PrecisionSling = Key<ResourceTag>.FromString("Weapons/Common/PrecisionSling");
        public static readonly Key<ResourceTag> Common_SailorCutlass = Key<ResourceTag>.FromString("Weapons/Common/SailorCutlass");
        public static readonly Key<ResourceTag> Common_SimpleMace = Key<ResourceTag>.FromString("Weapons/Common/SimpleMace");
        public static readonly Key<ResourceTag> Common_SmithingHammer = Key<ResourceTag>.FromString("Weapons/Common/SmithingHammer");
        public static readonly Key<ResourceTag> Common_SpikedClub = Key<ResourceTag>.FromString("Weapons/Common/SpikedClub");
        public static readonly Key<ResourceTag> Common_SteelDagger = Key<ResourceTag>.FromString("Weapons/Common/SteelDagger");
        public static readonly Key<ResourceTag> Common_ThrowingHatchet = Key<ResourceTag>.FromString("Weapons/Common/ThrowingHatchet");
        public static readonly Key<ResourceTag> Common_WoodmanAxe = Key<ResourceTag>.FromString("Weapons/Common/WoodmanAxe");
        public static readonly Key<ResourceTag> Common_WornBastardSword = Key<ResourceTag>.FromString("Weapons/Common/WornBastardSword");
        public static readonly Key<ResourceTag> Common_YewShortbow = Key<ResourceTag>.FromString("Weapons/Common/YewShortBow");

        // UNCOMMON
        public static readonly Key<ResourceTag> Uncommon_CompositeBow = Key<ResourceTag>.FromString("Weapons/Uncommon/CompositeBow");
        public static readonly Key<ResourceTag> Uncommon_DoubleEdgedBattleAxe = Key<ResourceTag>.FromString("Weapons/Uncommon/DoubleEdgedBattleAxe");
        public static readonly Key<ResourceTag> Uncommon_DuelistEstoc = Key<ResourceTag>.FromString("Weapons/Uncommon/DuelistEstoc");
        public static readonly Key<ResourceTag> Uncommon_GladiatorTrident = Key<ResourceTag>.FromString("Weapons/Uncommon/GladiatorTrident");
        public static readonly Key<ResourceTag> Uncommon_GreatWarHammer = Key<ResourceTag>.FromString("Weapons/Uncommon/GreatWarHammer");
        public static readonly Key<ResourceTag> Uncommon_HardenedSteelLongsword = Key<ResourceTag>.FromString("Weapons/Uncommon/HardenedSteelLongsword");
        public static readonly Key<ResourceTag> Uncommon_OfficerSpear = Key<ResourceTag>.FromString("Weapons/Uncommon/OfficerSpear");
        public static readonly Key<ResourceTag> Uncommon_OfficerSaber = Key<ResourceTag>.FromString("Weapons/Uncommon/OfficerSaber");
        public static readonly Key<ResourceTag> Uncommon_RangerBow = Key<ResourceTag>.FromString("Weapons/Uncommon/RangerBow");
        public static readonly Key<ResourceTag> Uncommon_ReinforcedHalberd = Key<ResourceTag>.FromString("Weapons/Uncommon/ReinforcedHalberd");
        public static readonly Key<ResourceTag> Uncommon_TemplarHeavyMace = Key<ResourceTag>.FromString("Weapons/Uncommon/TemplarHeavyMace");
        public static readonly Key<ResourceTag> Uncommon_ThiefDagger = Key<ResourceTag>.FromString("Weapons/Uncommon/ThiefDagger");
        public static readonly Key<ResourceTag> Uncommon_Flail = Key<ResourceTag>.FromString("Weapons/Uncommon/Flail");
        public static readonly Key<ResourceTag> Uncommon_Claymore = Key<ResourceTag>.FromString("Weapons/Uncommon/Claymore");
        public static readonly Key<ResourceTag> Uncommon_HandCrossbow = Key<ResourceTag>.FromString("Weapons/Uncommon/HandCrossbow");

        // RARE
        public static readonly Key<ResourceTag> Rare_AssassinDirk = Key<ResourceTag>.FromString("Weapons/Rare/AssassinDirk");
        public static readonly Key<ResourceTag> Rare_EbonyWoodBow = Key<ResourceTag>.FromString("Weapons/Rare/EbonyWoodBow");
        public static readonly Key<ResourceTag> Rare_FireFlail = Key<ResourceTag>.FromString("Weapons/Rare/FireFlail");
        public static readonly Key<ResourceTag> Rare_FrostSpear = Key<ResourceTag>.FromString("Weapons/Rare/FrostSpear");
        public static readonly Key<ResourceTag> Rare_GlowingJusticeSword = Key<ResourceTag>.FromString("Weapons/Rare/GlowingJusticeSword");
        public static readonly Key<ResourceTag> Rare_HeavySiegeCrossbow = Key<ResourceTag>.FromString("Weapons/Rare/HeavySiegeCrossbow");
        public static readonly Key<ResourceTag> Rare_MasterKatana = Key<ResourceTag>.FromString("Weapons/Rare/MasterKatana");
        public static readonly Key<ResourceTag> Rare_MithrilBlade = Key<ResourceTag>.FromString("Weapons/Rare/MithrilBlade");
        public static readonly Key<ResourceTag> Rare_RunicAxe = Key<ResourceTag>.FromString("Weapons/Rare/RunicAxe");
        public static readonly Key<ResourceTag> Rare_ThunderHammer = Key<ResourceTag>.FromString("Weapons/Rare/ThunderHammer");

        // ELITE
        public static readonly Key<ResourceTag> Elite_Agonizer = Key<ResourceTag>.FromString("Weapons/Elite/Agonizer");
        public static readonly Key<ResourceTag> Elite_MountainBreaker = Key<ResourceTag>.FromString("Weapons/Elite/MountainBreaker");
        public static readonly Key<ResourceTag> Elite_DragonBreath = Key<ResourceTag>.FromString("Weapons/Elite/DragonBreath");
        public static readonly Key<ResourceTag> Elite_EclipseShard = Key<ResourceTag>.FromString("Weapons/Elite/EclipseShard");
        public static readonly Key<ResourceTag> Elite_Doomsday = Key<ResourceTag>.FromString("Weapons/Elite/Doomsday");

    }
}
