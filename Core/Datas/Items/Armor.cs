using Microsoft.Xna.Framework.Graphics;

namespace Dungeon100Steps.Core.Datas.Items
{
    public class Armor(string name, Texture2D texture, List<Bonus> bonuses)
        : Item(name, texture, bonuses, stackLimit: 1)
    {
    }
}
