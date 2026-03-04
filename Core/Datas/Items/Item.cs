using DinaCSharp.Services;

using Microsoft.Xna.Framework.Graphics;

namespace Dungeon100Steps.Core.Datas.Items
{

    public abstract class Item(string name, Texture2D? texture, List<Bonus> bonuses, int stackLimit)
    {
        public string Name { get; protected set; } = name;
        public Texture2D? Texture { get; protected set; } = texture;
        public Rarity Rarity { get; set; }
        public List<Bonus> Bonuses { get; protected set; } = bonuses;
        public int StackLimit { get; protected set; } = stackLimit;
    }
}
