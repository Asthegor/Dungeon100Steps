using Dungeon100Steps.Core.Datas.Characters;

using Microsoft.Xna.Framework.Graphics;

namespace Dungeon100Steps.Core.Datas.Items
{
    public class Potion(string name, Texture2D texture, List<Bonus> bonuses, int stacklimit)
        : Item(name, texture, bonuses, stacklimit)
    {
        public void Drink(Player player)
        {
            foreach (var bonus in Bonuses)
            {
                switch (bonus.Type)
                {
                    case BonusType.Health:
                        int healthToRestore = bonus.GetTotalAmount(player.MaxHealth);
                        player.RestoreHealth(healthToRestore);
                        break;
                    case BonusType.Mana:
                        int manaToRestore = bonus.GetTotalAmount(player.MaxMana);
                        player.RestoreMana(manaToRestore);
                        break;
                }
            }
        }
    }
}
