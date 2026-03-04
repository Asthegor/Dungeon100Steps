using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Text;

namespace Dungeon100Steps.Core.Datas.Items
{
    public class Bag(Texture2D texture, int maxcapacity)
    {
        public Texture2D Texture { get; private set; } = texture;
        public int MaxCapacity { get; private set; } = maxcapacity;
    }
}
