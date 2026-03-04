using DinaCSharp.Resources;
using DinaCSharp.Services;

using Dungeon100Steps.Core.Datas.Enemies;
using Dungeon100Steps.Core.Keys;

using Microsoft.Xna.Framework.Graphics;

namespace Dungeon100Steps.Core.Datas.Characters
{
    public class EnemyFactory
    {
        private const int BOSS_RATIO = 5;

        private static readonly List<(int Weigth, Func<int, bool, Enemy> Factory)> ZONE1_ENEMIES =
        [
            ( 60, GenerateRat ),
            ( 40, GenerateSlime )
        ];

        private static readonly List<(int Weigth, Func<int, bool, Enemy> Factory)> ZONE2_ENEMIES = 
        [
            ( 60, GenerateRat ),
            ( 40, GenerateSlime )
        ];
        private static readonly List<(int Weigth, Func<int, bool, Enemy> Factory)> ZONE3_ENEMIES = 
        [
            ( 60, GenerateRat ),
            ( 40, GenerateSlime )
        ];
        private static readonly List<(int Weigth, Func<int, bool, Enemy> Factory)> ZONE4_ENEMIES = 
        [
            ( 60, GenerateRat ),
            ( 40, GenerateSlime )
        ];
        private static readonly List<Func<int, bool, Enemy>> BOSS_ENEMIES =
        [
            GenerateRat,
            GenerateSlime,
        ];

        private static readonly Random _random = new Random();
        private static ResourceManager? _resourceManager;
        public static Enemy Generate(int level, ResourceManager resourceManager)
        {
            ArgumentNullException.ThrowIfNull(resourceManager);
            
            _resourceManager = resourceManager;
            return level switch
            {
                <= (int)Zone.Zone1 => GetRandomEnemy(ZONE1_ENEMIES, level),
                <= (int)Zone.Zone2 => GetRandomEnemy(ZONE2_ENEMIES, level),
                <= (int)Zone.Zone3 => GetRandomEnemy(ZONE3_ENEMIES, level),
                <= (int)Zone.Zone4 => GetRandomEnemy(ZONE4_ENEMIES, level),
                _ => throw new InvalidOperationException("Unknown zone")
            };
        }
        public static Enemy GenerateTutorialRat(ResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
            return GenerateRat(1);
        }
        public static Enemy GenerateTutorialBoss(ResourceManager resourceManager)
        {
            _resourceManager = resourceManager;

            // Slime - Boss
            // HP : 20
            // AttackAmount : 5
            // Defense : 5
            return GenerateSlime(3, true);
        }
        public static Enemy GenerateBoss(int level, ResourceManager resourceManager)
        {
            _resourceManager = resourceManager;

            // Normalement, les donjons seront organisés par tranche de 10 niveaux.
            int index = level / 10;
            return BOSS_ENEMIES[index](level, true);
        }


        private static Enemy GetRandomEnemy(List<(int Weight, Func<int, bool, Enemy> Factory)> enemies, int level)
        {
            int totalWeight = enemies.Sum(e => e.Weight);
            int rnd = _random!.Next(0, totalWeight);
            int cursor = 0;

            foreach (var (Weight, factory) in enemies)
            {
                cursor += Weight;
                if (rnd < cursor)
                    return factory(level, false);
            }
            return enemies.First().Factory(level, false);
        }

        #region Fonctions de génération des monstres
        private static Enemy GenerateRat(int level, bool isBoss = false)
        {
            var health = GetHealthFromLevel(level, 5, isBoss);
            var attack = GetAttackFromLevel(level, 2, isBoss);
            var defense = GetDefenseFromLevel(level, 1, isBoss);

            var rndTexture = _random!.Next(0, 2);
            Key<ResourceTag> textureKey = rndTexture == 0 ? EnemyKeys.Rat1 : EnemyKeys.Rat2;
            return new Enemy("Rat", _resourceManager!.Load<Texture2D>(textureKey)!, attack, defense, health, combatdelay: 2f);
        }

        private static Enemy GenerateSlime(int level, bool isBoss = false)
        {
            var health = GetHealthFromLevel(level, 10, isBoss);
            var attack = GetAttackFromLevel(level, 1, isBoss);
            var defense = GetDefenseFromLevel(level, 1, isBoss);

            var rndTexture = _random!.Next(0, 2);
            Key<ResourceTag> textureKey = rndTexture == 0 ? EnemyKeys.Slime1 : EnemyKeys.Slime2;
            return new Enemy("Slime",_resourceManager!.Load<Texture2D>(textureKey)!, attack, defense, health, combatdelay: 2.5f);
        }
        #endregion

        #region Fonctions utilitaires

        private static int GetHealthFromLevel(int level, int baseValue, bool isBoss = false)
        {
            return (baseValue + (int)((baseValue / 2f) * (level - 1))) * (isBoss ? BOSS_RATIO : 1);
        }
        private static int GetAttackFromLevel(int level, int baseValue, bool isBoss = false)
        {
            return (baseValue + (int)((baseValue / 4f) * (level - 1))) * (isBoss ? BOSS_RATIO : 1);
        }
        private static int GetDefenseFromLevel(int level, int baseValue, bool isBoss = false)
        {
            return (baseValue + (int)((baseValue / 3f) * (level - 1))) * (isBoss ? BOSS_RATIO : 1);
        }
        #endregion
    }
}
