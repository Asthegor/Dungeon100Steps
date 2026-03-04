using Dungeon100Steps.Core.Datas.Characters;
using Dungeon100Steps.Core.Datas.Dungeons;

namespace Dungeon100Steps.Core.Datas
{
    public class GameData
    {
        public bool IsTutorialSkipped { get; set; }
        public Dungeon? CurrentDungeon { get; set; }
        public Player? Player { get; set; }

    }
}
