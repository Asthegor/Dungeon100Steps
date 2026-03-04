using DinaCSharp.Resources;

namespace Dungeon100Steps.Core.Datas.Items
{
    public static class ItemFactory
    {
        private static readonly Random _random = new Random();

        public static Item? CreateEquipment(int level, ItemType? itemType = null, Rarity? rarity = null)
        {
            itemType ??= RollItemType();

            if (itemType == ItemType.Weapon || itemType == ItemType.Armor)
            {
                rarity ??= RollRarity(level == 100);
                if (itemType == ItemType.Weapon)
                    return GetWeapon(rarity.Value);
                else
                    return GetArmor(rarity.Value);
            }
            else if (itemType == ItemType.Potion)
            {
                return PotionFactory.Get();
            }
            return null;
        }

        private static ItemType RollItemType()
        {
            int roll = _random.Next(0, 100);
            return roll switch
            {
                < (int)ItemType.Weapon => ItemType.Weapon,
                < (int)ItemType.Armor => ItemType.Armor,
                < (int)ItemType.Potion => ItemType.Potion,
                _ => ItemType.None,
            };

        }
        private static Rarity RollRarity(bool isBoss)
        {
            int roll = _random.Next(0, 1000);
            return roll switch
            {
                < (int)Rarity.Elite => isBoss ? Rarity.Elite : Rarity.Rare,
                < (int)Rarity.Rare => Rarity.Rare,
                < (int)Rarity.Uncommon => Rarity.Uncommon,
                < (int)Rarity.Common => Rarity.Common,
                _ => Rarity.Junk,
            };
        }

        public static Weapon GetWeapon(Rarity rarity)
        {
            return WeaponFactory.Get(rarity);
        }
        public static Armor GetArmor(Rarity rarity)
        {
            return ArmorFactory.Get(rarity);
        }

        public static Item GenerateTutorialWepon(ResourceManager resourceManager)
        {
            return WeaponFactory.GenerateTutorialWeapon(resourceManager);
        }
    }
}
