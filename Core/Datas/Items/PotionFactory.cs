using DinaCSharp.Resources;
using DinaCSharp.Services;

using Dungeon100Steps.Core.Keys;

using Microsoft.Xna.Framework.Graphics;

namespace Dungeon100Steps.Core.Datas.Items
{
    public static class PotionFactory
    {
        private static readonly int STACK_LIMIT = 10;
        private static readonly Random _random = new();
        private static ResourceManager? _resourceManager;
        private static List<(int Weight, Potion Potion)> _allPotions = [];
        private static bool _initialized;

        // Événement pour notifier la progression
        public static event EventHandler<PotionLoadProgressEventArgs>? OnPotionLoaded;

        // Propriétés pour suivre la progression
        public static int TotalPotionsToLoad { get; private set; }
        public static int PotionsLoaded { get; private set; }

        public static void Initialize()
        {
            if (_initialized) return;

            PotionsLoaded = 0;
            TotalPotionsToLoad = KeyCounter.Count(typeof(PotionKeys)) - 1;

            _resourceManager = ServiceLocator.Get<ResourceManager>(ProjectServiceKeys.AssetsResourceManager)
                ?? throw new NullReferenceException("AssetsResourceManager not found");

            AddPotions();

            _initialized = true;
        }

        public static Potion Get()
        {
            if (!_initialized)
                //throw new InvalidOperationException("PotionFactory not initialized.");
                Initialize();

            int totalWeigth = _allPotions.Sum(w => w.Weight);

            int roll = _random.Next(0, totalWeigth);
            int cursor = 0;
            foreach (var potion in _allPotions)
            {
                cursor += potion.Weight;
                if (roll < cursor)
                    return Clone(potion.Potion);
            }
            return Clone(_allPotions.First().Potion);
        }


        private static void AddPotions()
        {
            var prefix = "POTION";
            _allPotions =
            [
                (45, CreatePotion(PotionKeys.MinorHealth,
                                 $"{prefix}_{nameof(PotionKeys.MinorHealth).ToUpperInvariant()}",
                                 [
                                    new Bonus(BonusType.Health, "BONUS_HEALTH", percentage: 25)
                                 ],
                                 STACK_LIMIT)),
                (20, CreatePotion(PotionKeys.Mana,
                                 $"{prefix}_{nameof(PotionKeys.Mana).ToUpperInvariant()}",
                                 [
                                    new Bonus(BonusType.Mana, "BONUS_MANA", 60)
                                 ],
                                 STACK_LIMIT)),
                (25, CreatePotion(PotionKeys.Health,
                                 $"{prefix}_{nameof(PotionKeys.Health).ToUpperInvariant()}",
                                 [
                                    new Bonus(BonusType.Health, "BONUS_HEALTH", percentage: 50)
                                 ],
                                 STACK_LIMIT)),
                (10, CreatePotion(PotionKeys.LargeHealth,
                                 $"{prefix}_{nameof(PotionKeys.LargeHealth).ToUpperInvariant()}",
                                 [
                                    new Bonus(BonusType.Health, "BONUS_HEALTH", percentage: 80)
                                 ],
                                 STACK_LIMIT)),
            ];
        }
        private static Potion CreatePotion(Key<ResourceTag> key, string name, List<Bonus> bonuses, int stacklimit)
        {
            Potion potion = new Potion(name, _resourceManager!.Load<Texture2D>(key)!, bonuses, stacklimit);
            NotifyProgress();
            return potion;
        }
        private static void NotifyProgress()
        {
            PotionsLoaded++;
            OnPotionLoaded?.Invoke(null, new PotionLoadProgressEventArgs(PotionsLoaded, TotalPotionsToLoad));
        }
        public static void ClearEventSubscribers()
        {
            OnPotionLoaded = null;
        }
        private static Potion Clone(Potion original)
        {
            var bonusesCopy = original.Bonuses.Select(b => new Bonus(b.Type, b.TranslationKey, b.Amount, b.Percentage, b.Duration)).ToList();
            return new Potion(original.Name, original.Texture!, bonusesCopy, original.StackLimit);
        }
    }
    // Classe EventArgs pour l'événement de progression
    public class PotionLoadProgressEventArgs(int potionsLoaded, int totalPotions) : EventArgs
    {
        public int PotionsLoaded { get; } = potionsLoaded;
        public int TotalPotions { get; } = totalPotions;
        public float Progress => TotalPotions > 0 ? (float)PotionsLoaded / TotalPotions : 0f;
    }
}
