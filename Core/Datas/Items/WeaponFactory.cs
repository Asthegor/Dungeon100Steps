using DinaCSharp.Resources;
using DinaCSharp.Services;

using Dungeon100Steps.Core.Keys;

using Microsoft.Xna.Framework.Graphics;


namespace Dungeon100Steps.Core.Datas.Items
{
    public static class WeaponFactory
    {
        private static readonly Random _random = new();
        private static ResourceManager? _resourceManager;
        private static readonly Dictionary<Rarity, List<(int Weight, Weapon Weapon)>> _allWeapons = [];
        private static bool _initialized;

        // Événement pour notifier la progression
        public static event EventHandler<WeaponLoadProgressEventArgs>? OnWeaponLoaded;

        // Propriétés pour suivre la progression
        public static int TotalWeaponsToLoad { get; private set; }
        public static int WeaponsLoaded { get; private set; }

        public static void Initialize()
        {
            if (_initialized)
                return;

            WeaponsLoaded = 0;
            TotalWeaponsToLoad = KeyCounter.Count(typeof(WeaponKeys)) - 1;

            _resourceManager = ServiceLocator.Get<ResourceManager>(ProjectServiceKeys.AssetsResourceManager)
                ?? throw new NullReferenceException("AssetsResourceManager not found");
            AddJunkWeapons();
            AddCommonWeapons();
            AddUncommonWeapons();
            AddRareWeapons();
            AddEliteWeapons();

            _initialized = true;
        }

        public static Weapon Get(Rarity rarity)
        {
            if (!_initialized)
                //throw new InvalidOperationException("WeaponFactory not initialized");
                Initialize();

            List<(int Weight, Weapon Weapon)> weapons = _allWeapons[rarity];

            int totalWeigth = weapons.Sum(w => w.Weight);

            int roll = _random.Next(0, totalWeigth);
            int cursor = 0;
            foreach (var weapon in weapons)
            {
                cursor += weapon.Weight;
                if (roll < cursor)
                    return Clone(weapon.Weapon);
            }
            return Clone(weapons.First().Weapon);
        }


        private static void AddJunkWeapons()
        {
            var rarity = Rarity.Junk;
            List<(int Weight, Weapon Weapon)> weapons =
            [
                (9, CreateWeapon(rarity, WeaponKeys.Junk_BendPoker,
                                 nameof(WeaponKeys.Junk_BendPoker).ToUpperInvariant(),
                             [
                                new Bonus(BonusType.Attack, "BONUS_ATTACK", 3)
                             ])),
                (10, CreateWeapon(rarity, WeaponKeys.Junk_BrokenPickaxeHandle,
                                 nameof(WeaponKeys.Junk_BrokenPickaxeHandle).ToUpperInvariant(),
                             [
                                new Bonus(BonusType.Attack, "BONUS_ATTACK", 2)
                             ])),
                (20, CreateWeapon(rarity, WeaponKeys.Junk_DullKitchenKnife,
                                 nameof(WeaponKeys.Junk_DullKitchenKnife).ToUpperInvariant(),
                             [
                                new Bonus(BonusType.Attack, "BONUS_ATTACK", 1)
                             ])),
                (16, CreateWeapon(rarity, WeaponKeys.Junk_LargeMammalBone,
                                 nameof(WeaponKeys.Junk_LargeMammalBone).ToUpperInvariant(),
                             [
                                new Bonus(BonusType.Attack, "BONUS_ATTACK", 2)
                             ])),
                (4, CreateWeapon(rarity, WeaponKeys.Junk_PotLid,
                                 nameof(WeaponKeys.Junk_PotLid).ToUpperInvariant(),
                             [
                                new Bonus(BonusType.Attack, "BONUS_ATTACK", 0)
                             ])),
                (8, CreateWeapon(rarity, WeaponKeys.Junk_RustyDagger,
                                 nameof(WeaponKeys.Junk_RustyDagger).ToUpperInvariant(),
                             [
                                new Bonus(BonusType.Attack, "BONUS_ATTACK", 1)
                             ])),
                (15, CreateWeapon(rarity, WeaponKeys.Junk_SharpenedBranch,
                                 nameof(WeaponKeys.Junk_SharpenedBranch).ToUpperInvariant(),
                             [
                                new Bonus(BonusType.Attack, "BONUS_ATTACK", 1)
                             ])),
                (6, CreateWeapon(rarity, WeaponKeys.Junk_StoneClub,
                                 nameof(WeaponKeys.Junk_StoneClub).ToUpperInvariant(),
                             [
                                new Bonus(BonusType.Attack, "BONUS_ATTACK", 2)
                             ])),
                (7, CreateWeapon(rarity, WeaponKeys.Junk_WoodenTrainingSword,
                                 nameof(WeaponKeys.Junk_WoodenTrainingSword).ToUpperInvariant(),
                             [
                                new Bonus(BonusType.Attack, "BONUS_ATTACK", 1)
                             ])),
            ];
            _allWeapons[Rarity.Junk] = weapons;
        }
        private static void AddCommonWeapons()
        {
            var rarity = Rarity.Common;
            List<(int Weight, Weapon Weapon)> weapons =
            [
                (6, CreateWeapon(rarity, WeaponKeys.Common_BasicMorningstar,
                                 nameof(WeaponKeys.Common_BasicMorningstar).ToUpperInvariant(),
                                 [
                                     new Bonus(BonusType.Attack, "BONUS_ATTACK", 5)
                                 ])),
                (7, CreateWeapon(rarity, WeaponKeys.Common_GuardSpear,
                                 nameof(WeaponKeys.Common_GuardSpear).ToUpperInvariant(),
                             [
                                new Bonus(BonusType.Attack, "BONUS_ATTACK", 6)
                             ])),
                (5, CreateWeapon(rarity, WeaponKeys.Common_HuntingLongbow,
                                 nameof(WeaponKeys.Common_HuntingLongbow).ToUpperInvariant(),
                             [
                                new Bonus(BonusType.Attack, "BONUS_ATTACK", 4)
                             ])),
                (6, CreateWeapon(rarity, WeaponKeys.Common_InfantryPike,
                                 nameof(WeaponKeys.Common_InfantryPike).ToUpperInvariant(),
                             [
                                new Bonus(BonusType.Attack, "BONUS_ATTACK", 5)
                             ])),
                (2, CreateWeapon(rarity, WeaponKeys.Common_IronGladius,
                                 nameof(WeaponKeys.Common_IronGladius).ToUpperInvariant(),
                             [
                                new Bonus(BonusType.Attack, "BONUS_ATTACK", 8)
                             ])),
                (6, CreateWeapon(rarity, WeaponKeys.Common_IronShodStaff,
                                 nameof(WeaponKeys.Common_IronShodStaff).ToUpperInvariant(),
                             [
                                new Bonus(BonusType.Attack, "BONUS_ATTACK", 6)
                             ])),
                (5, CreateWeapon(rarity, WeaponKeys.Common_LightRapier,
                                 nameof(WeaponKeys.Common_LightRapier).ToUpperInvariant(),
                             [
                                new Bonus(BonusType.Attack, "BONUS_ATTACK", 5)
                             ])),
                (3, CreateWeapon(rarity, WeaponKeys.Common_MilitiaShortsword,
                                 nameof(WeaponKeys.Common_MilitiaShortsword).ToUpperInvariant(),
                             [
                                new Bonus(BonusType.Attack, "BONUS_ATTACK", 7)
                             ])),
                (4, CreateWeapon(rarity, WeaponKeys.Common_NomadScimitar,
                                 nameof(WeaponKeys.Common_NomadScimitar).ToUpperInvariant(),
                             [
                                new Bonus(BonusType.Attack, "BONUS_ATTACK", 7)
                             ])),
                (6, CreateWeapon(rarity, WeaponKeys.Common_PeasantBillhook,
                                 nameof(WeaponKeys.Common_PeasantBillhook).ToUpperInvariant(),
                             [
                                new Bonus(BonusType.Attack, "BONUS_ATTACK", 6)
                             ])),
                (7, CreateWeapon(rarity, WeaponKeys.Common_SailorCutlass,
                                 nameof(WeaponKeys.Common_SailorCutlass).ToUpperInvariant(),
                             [
                                new Bonus(BonusType.Attack, "BONUS_ATTACK", 4)
                             ])),
                (5, CreateWeapon(rarity, WeaponKeys.Common_SimpleMace,
                                 nameof(WeaponKeys.Common_SimpleMace).ToUpperInvariant(),
                             [
                                new Bonus(BonusType.Attack, "BONUS_ATTACK", 5)
                             ])),
                (3, CreateWeapon(rarity, WeaponKeys.Common_SmithingHammer,
                                 nameof(WeaponKeys.Common_SmithingHammer).ToUpperInvariant(),
                             [
                                new Bonus(BonusType.Attack, "BONUS_ATTACK", 7)
                             ])),
                (8, CreateWeapon(rarity, WeaponKeys.Common_SpikedClub,
                                 nameof(WeaponKeys.Common_SpikedClub).ToUpperInvariant(),
                             [
                                new Bonus(BonusType.Attack, "BONUS_ATTACK", 6)
                             ])),
                (5, CreateWeapon(rarity, WeaponKeys.Common_SteelDagger,
                                 nameof(WeaponKeys.Common_SteelDagger).ToUpperInvariant(),
                             [
                                new Bonus(BonusType.Attack, "BONUS_ATTACK", 5)
                             ])),
                (6, CreateWeapon(rarity, WeaponKeys.Common_ThrowingHatchet,
                                 nameof(WeaponKeys.Common_ThrowingHatchet).ToUpperInvariant(),
                             [
                                new Bonus(BonusType.Attack, "BONUS_ATTACK", 5)
                             ])),
                (3, CreateWeapon(rarity, WeaponKeys.Common_WoodmanAxe,
                                 nameof(WeaponKeys.Common_WoodmanAxe).ToUpperInvariant(),
                             [
                                new Bonus(BonusType.Attack, "BONUS_ATTACK", 7)
                             ])),
                (1, CreateWeapon(rarity, WeaponKeys.Common_WornBastardSword,
                                 nameof(WeaponKeys.Common_WornBastardSword).ToUpperInvariant(),
                             [
                                new Bonus(BonusType.Attack, "BONUS_ATTACK", 9)
                             ])),
                (6, CreateWeapon(rarity, WeaponKeys.Common_YewShortbow,
                                 nameof(WeaponKeys.Common_YewShortbow).ToUpperInvariant(),
                             [
                                new Bonus(BonusType.Attack, "BONUS_ATTACK", 6)
                             ])),
            ];
            _allWeapons[Rarity.Common] = weapons;
        }
        private static void AddUncommonWeapons()
        {
            var rarity = Rarity.Uncommon;
            List<(int Weight, Weapon Weapon)> weapons =
            [
                (8, CreateWeapon(rarity, WeaponKeys.Uncommon_Claymore,
                                 nameof(WeaponKeys.Uncommon_Claymore).ToUpperInvariant(),
                             [
                                new Bonus(BonusType.Attack, "BONUS_ATTACK", 11)
                             ])),
                (10, CreateWeapon(rarity, WeaponKeys.Uncommon_CompositeBow,
                                 nameof(WeaponKeys.Uncommon_CompositeBow).ToUpperInvariant(),
                             [
                                new Bonus(BonusType.Attack, "BONUS_ATTACK", 9)
                             ])),
                (6, CreateWeapon(rarity, WeaponKeys.Uncommon_DoubleEdgedBattleAxe,
                                 nameof(WeaponKeys.Uncommon_DoubleEdgedBattleAxe).ToUpperInvariant(),
                             [
                                new Bonus(BonusType.Attack, "BONUS_ATTACK", 17)
                             ])),
                (9, CreateWeapon(rarity, WeaponKeys.Uncommon_DuelistEstoc,
                                 nameof(WeaponKeys.Uncommon_DuelistEstoc).ToUpperInvariant(),
                             [
                                new Bonus(BonusType.Attack, "BONUS_ATTACK", 9)
                             ])),
                (7, CreateWeapon(rarity, WeaponKeys.Uncommon_Flail,
                                 nameof(WeaponKeys.Uncommon_Flail).ToUpperInvariant(),
                             [
                                new Bonus(BonusType.Attack, "BONUS_ATTACK", 10)
                             ])),
                (8, CreateWeapon(rarity, WeaponKeys.Uncommon_GladiatorTrident,
                                 nameof(WeaponKeys.Uncommon_GladiatorTrident).ToUpperInvariant(),
                             [
                                new Bonus(BonusType.Attack, "BONUS_ATTACK", 12)
                             ])),
                (5, CreateWeapon(rarity, WeaponKeys.Uncommon_GreatWarHammer,
                                 nameof(WeaponKeys.Uncommon_GreatWarHammer).ToUpperInvariant(),
                             [
                                new Bonus(BonusType.Attack, "BONUS_ATTACK", 13)
                             ])),
                (10, CreateWeapon(rarity, WeaponKeys.Uncommon_HandCrossbow,
                                 nameof(WeaponKeys.Uncommon_HandCrossbow).ToUpperInvariant(),
                             [
                                new Bonus(BonusType.Attack, "BONUS_ATTACK", 9)
                             ])),
                (4, CreateWeapon(rarity, WeaponKeys.Uncommon_HardenedSteelLongsword,
                                 nameof(WeaponKeys.Uncommon_HardenedSteelLongsword).ToUpperInvariant(),
                             [
                                new Bonus(BonusType.Attack, "BONUS_ATTACK", 16)
                             ])),
                (8, CreateWeapon(rarity, WeaponKeys.Uncommon_OfficerSaber,
                                 nameof(WeaponKeys.Uncommon_OfficerSaber).ToUpperInvariant(),
                             [
                                new Bonus(BonusType.Attack, "BONUS_ATTACK", 11)
                             ])),
                (7, CreateWeapon(rarity, WeaponKeys.Uncommon_OfficerSpear,
                                 nameof(WeaponKeys.Uncommon_OfficerSpear).ToUpperInvariant(),
                             [
                                new Bonus(BonusType.Attack, "BONUS_ATTACK", 13)
                             ])),
                (5, CreateWeapon(rarity, WeaponKeys.Uncommon_RangerBow,
                                 nameof(WeaponKeys.Uncommon_RangerBow).ToUpperInvariant(),
                             [
                                new Bonus(BonusType.Attack, "BONUS_ATTACK", 9)
                             ])),
                (5, CreateWeapon(rarity, WeaponKeys.Uncommon_ReinforcedHalberd,
                                 nameof(WeaponKeys.Uncommon_ReinforcedHalberd).ToUpperInvariant(),
                             [
                                new Bonus(BonusType.Attack, "BONUS_ATTACK", 11)
                             ])),
                (7, CreateWeapon(rarity, WeaponKeys.Uncommon_TemplarHeavyMace,
                                 nameof(WeaponKeys.Uncommon_TemplarHeavyMace).ToUpperInvariant(),
                             [
                                new Bonus(BonusType.Attack, "BONUS_ATTACK", 10)
                             ])),
                (10, CreateWeapon(rarity, WeaponKeys.Uncommon_ThiefDagger,
                                 nameof(WeaponKeys.Uncommon_ThiefDagger).ToUpperInvariant(),
                             [
                                new Bonus(BonusType.Attack, "BONUS_ATTACK", 9)
                             ])),
            ];
            _allWeapons[Rarity.Uncommon] = weapons;
        }
        private static void AddRareWeapons()
        {
            var rarity = Rarity.Rare;
            List<(int Weight, Weapon Weapon)> weapons =
            [
                (10, CreateWeapon(rarity, WeaponKeys.Rare_AssassinDirk,
                                 nameof(WeaponKeys.Rare_AssassinDirk).ToUpperInvariant(),
                             [
                                new Bonus(BonusType.Attack, "BONUS_ATTACK", 11),
                                new Bonus(BonusType.Poison, "BONUS_POISON", 10, duration: 2)
                             ])),
                (15, CreateWeapon(rarity, WeaponKeys.Rare_EbonyWoodBow,
                                 nameof(WeaponKeys.Rare_EbonyWoodBow).ToUpperInvariant(),
                             [
                                new Bonus(BonusType.Attack, "BONUS_ATTACK", 19)
                             ])),
                (10, CreateWeapon(rarity, WeaponKeys.Rare_FireFlail,
                                 nameof(WeaponKeys.Rare_FireFlail).ToUpperInvariant(),
                             [
                                new Bonus(BonusType.Attack, "BONUS_ATTACK", 14),
                                new Bonus(BonusType.Fire, "BONUS_FIRE", 8, duration: 3)
                             ])),
                (10, CreateWeapon(rarity, WeaponKeys.Rare_FrostSpear,
                                 nameof(WeaponKeys.Rare_FrostSpear).ToUpperInvariant(),
                             [
                                new Bonus(BonusType.Attack, "BONUS_ATTACK", 16),
                                new Bonus(BonusType.Ice, "BONUS_ICE", 5)
                             ])),
                (5, CreateWeapon(rarity, WeaponKeys.Rare_GlowingJusticeSword,
                                 nameof(WeaponKeys.Rare_GlowingJusticeSword).ToUpperInvariant(),
                             [
                                new Bonus(BonusType.Attack, "BONUS_ATTACK", 20),
                                new Bonus(BonusType.Health, "BONUS_HEALTH", percentage: 5)
                             ])),
                (15, CreateWeapon(rarity, WeaponKeys.Rare_HeavySiegeCrossbow,
                                 nameof(WeaponKeys.Rare_HeavySiegeCrossbow).ToUpperInvariant(),
                             [
                                new Bonus(BonusType.Attack, "BONUS_ATTACK", 19)
                             ])),
                (5, CreateWeapon(rarity, WeaponKeys.Rare_MasterKatana,
                                 nameof(WeaponKeys.Rare_MasterKatana).ToUpperInvariant(),
                             [
                                new Bonus(BonusType.Attack, "BONUS_ATTACK", 22),
                                new Bonus(BonusType.Bleed, "BONUS_BLEED", percentage: 1, duration: 3)
                             ])),
                (15, CreateWeapon(rarity, WeaponKeys.Rare_MithrilBlade,
                                 nameof(WeaponKeys.Rare_MithrilBlade).ToUpperInvariant(),
                             [
                                new Bonus(BonusType.Attack, "BONUS_ATTACK", 18)
                             ])),
                (10, CreateWeapon(rarity, WeaponKeys.Rare_RunicAxe,
                                 nameof(WeaponKeys.Rare_RunicAxe).ToUpperInvariant(),
                             [
                                new Bonus(BonusType.Attack, "BONUS_ATTACK", 20),
                                new Bonus(BonusType.Mana, "BONUS_MANA", 5)
                             ])),
                (5, CreateWeapon(rarity, WeaponKeys.Rare_ThunderHammer,
                                 nameof(WeaponKeys.Rare_ThunderHammer).ToUpperInvariant(),
                             [
                                new Bonus(BonusType.Attack, "BONUS_ATTACK", 23),
                                new Bonus(BonusType.Stunt, "BONUS_STUNT")
                             ])),
            ];
            _allWeapons[Rarity.Rare] = weapons;
        }
        private static void AddEliteWeapons()
        {
            var rarity = Rarity.Elite;
            List<(int Weight, Weapon Weapon)> weapons =
            [
                (20, CreateWeapon(rarity, WeaponKeys.Elite_Agonizer,
                                  nameof(WeaponKeys.Elite_Agonizer).ToUpperInvariant(),
                                  [
                                      new Bonus(BonusType.Attack, "BONUS_ATTACK", 30),
                                      new Bonus(BonusType.Bleed, "BONUS_BLEED", percentage: 5, duration: 5),
                                      new Bonus(BonusType.Poison, "BONUS_POISON", percentage: 5, duration: 3)
                                  ])),
                (20, CreateWeapon(rarity, WeaponKeys.Elite_Doomsday,
                                  nameof(WeaponKeys.Elite_Doomsday).ToUpperInvariant(),
                                  [
                                      new Bonus(BonusType.Attack, "BONUS_ATTACK", 45),
                                      new Bonus(BonusType.ResistBleed, "BONUS_RESISTBLEED", percentage: 25),
                                      new Bonus(BonusType.Health, "MALUS_HEALTH", percentage: -15)
                                  ])),
                (20, CreateWeapon(rarity, WeaponKeys.Elite_DragonBreath,
                                  nameof(WeaponKeys.Elite_DragonBreath).ToUpperInvariant(),
                                  [
                                      new Bonus(BonusType.Attack, "BONUS_ATTACK", 31),
                                      new Bonus(BonusType.Fire, "BONUS_FIRE", 16),
                                      new Bonus(BonusType.ResistFire, "BONUS_RESISTFIRE", percentage : 100)
                                  ])),
                (20, CreateWeapon(rarity, WeaponKeys.Elite_EclipseShard,
                                  nameof(WeaponKeys.Elite_EclipseShard).ToUpperInvariant(),
                                  [
                                      new Bonus(BonusType.Attack, "BONUS_ATTACK", 34),
                                      new Bonus(BonusType.Mana, "BONUS_MANA", percentage : 10),
                                      new Bonus(BonusType.ResistIce, "BONUS_RESISTICE", percentage : 50)
                                  ])),
                (20, CreateWeapon(rarity, WeaponKeys.Elite_MountainBreaker,
                                  nameof(WeaponKeys.Elite_MountainBreaker).ToUpperInvariant(),
                                  [
                                      new Bonus(BonusType.Attack, "BONUS_ATTACK", 36),
                                      new Bonus(BonusType.ResistStunt, "BONUS_RESISTSTUNT", percentage : 75)
                                  ])),
            ];
            _allWeapons[Rarity.Elite] = weapons;
        }
        private static Weapon CreateWeapon(Rarity rarity, Key<ResourceTag> key, string name, List<Bonus> bonuses)
        {
            Weapon weapon = new Weapon(name, _resourceManager!.Load<Texture2D>(key)!, bonuses)
            {
                Rarity = rarity
            };

            NotifyProgress();

            return weapon;
        }
        private static void NotifyProgress()
        {
            WeaponsLoaded++;
            OnWeaponLoaded?.Invoke(null, new WeaponLoadProgressEventArgs(WeaponsLoaded, TotalWeaponsToLoad));
        }
        public static void ClearEventSubscribers()
        {
            OnWeaponLoaded = null;
        }

        public static Item GenerateTutorialWeapon(ResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
            return CreateWeapon(Rarity.Junk, WeaponKeys.Junk_SharpenedBranch,
                                nameof(WeaponKeys.Junk_SharpenedBranch).ToUpperInvariant(),
                                [
                                    new Bonus(BonusType.Attack, "BONUS_ATTACK", 1)
                                ]);
        }
        private static Weapon Clone(Weapon original)
        {
            var bonusesCopy = original.Bonuses.Select(b => new Bonus(b.Type, b.TranslationKey, b.Amount, b.Percentage, b.Duration)).ToList();
            return new Weapon(original.Name, original.Texture!, bonusesCopy)
            {
                Rarity = original.Rarity
            };
        }
    }
    // Classe EventArgs pour l'événement de progression
    public class WeaponLoadProgressEventArgs(int weaponsLoaded, int totalWeapons) : EventArgs
    {
        public int WeaponsLoaded { get; } = weaponsLoaded;
        public int TotalWeapons { get; } = totalWeapons;
        public float Progress => TotalWeapons > 0 ? (float)WeaponsLoaded / TotalWeapons : 0f;
    }
}
