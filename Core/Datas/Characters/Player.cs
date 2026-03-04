using Dungeon100Steps.Core.Datas.Items;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Collections.Concurrent;

namespace Dungeon100Steps.Core.Datas.Characters
{
    public class Player(string name, Texture2D texture, int attack, int defense, int health, int mana, Texture2D bagtexture, float combatdelay)
        : Character(name, texture, attack, defense, health, mana, combatdelay)
    {
        private const int BASE_EXPERIENCE = 100;
        private const int LEVEL_PROGRESS_EXPERIENCE = 50;
        private const int MAX_HEALTH_LEVEL_UP_PERCENTAGE = 10;
        private const int MAX_MANA_LEVEL_UP_PERCENTAGE = 5;

        // Événements pour notifier les changements
        public event Action? OnLevelUp;

        private int _gold;
        

        public HeroClass Class { get; set; }
        public int Level { get; set; }
        public int TotalExperience { get; set; }
        public int LevelExperience { get; set; }

        public int Dexterity { get; set; }
        public int Constitution { get; set; }

        public int Gold
        {
            get => _gold;
            set
            {
                if (_gold != value)
                {
                    _gold = value;
                    RaiseStatsChanged();
                }
            }
        }

        public Inventory Inventory { get; set; } = new Inventory(bagtexture);
        public override void EquipWeapon(Weapon weapon)
        {
            Weapon? oldWeapon = Weapon;
            Inventory?.Remove(weapon);
            Weapon = weapon;

            if (oldWeapon != null)
                Inventory?.Add(oldWeapon);
        }
        public override void EquipArmor(Armor armor)
        {
            Armor? oldArmor = Armor;
            Inventory?.Remove(armor);
            Armor = armor;

            if (oldArmor != null)
                Inventory?.Add(oldArmor);
        }
        public void GetExperience(int xp)
        {
            TotalExperience += xp;
            LevelExperience += xp;
            var nextLevelExperience = BASE_EXPERIENCE + LEVEL_PROGRESS_EXPERIENCE * (Level + 1);
            if (LevelExperience > nextLevelExperience)
            {
                LevelExperience -= nextLevelExperience;
                GetLevelUp();
            }
        }
        private void GetLevelUp()
        {
            Level++;
            MaxHealth += MaxHealth * MAX_HEALTH_LEVEL_UP_PERCENTAGE / 100;
            BaseAttack += 2;
            BaseDefense++;
            MaxMana += MaxHealth * MAX_MANA_LEVEL_UP_PERCENTAGE / 100;

            OnLevelUp?.Invoke();
            RaiseStatsChanged(); // Toutes les stats changent au level up
        }
        public void DrinkPotion(Potion potion)
        {
            Inventory.Remove(potion);
            potion.Drink(this);
        }
        public void EquipBag(Bag bag)
        {
            if (bag.MaxCapacity > Inventory.Capacity)
            {
                Inventory.Texture = bag.Texture;
                Inventory.Capacity = bag.MaxCapacity;
            }
        }
    }
}
