using DinaCSharp.Services;

namespace Dungeon100Steps.Core.Keys
{
    public class ProjectServiceKeys
    {
        public static readonly Key<ServiceTag> Config = Key<ServiceTag>.FromString("Config.dat");
        public static readonly Key<ServiceTag> DefaultConfig = Key<ServiceTag>.FromString("DefaultConfig");
        public static readonly Key<ServiceTag> PlayerController = Key<ServiceTag>.FromString("PlayerController");
        public static readonly Key<ServiceTag> SoundManager = Key<ServiceTag>.FromString("SoundManager");
        public static readonly Key<ServiceTag> UIResourceManager = Key<ServiceTag>.FromString("UIResourceManager");
        public static readonly Key<ServiceTag> AssetsResourceManager = Key<ServiceTag>.FromString("AssetsResourceManager");

        public static readonly Key<ServiceTag> Player = Key<ServiceTag>.FromString("Player");
        public static readonly Key<ServiceTag> CurrentEnemy = Key<ServiceTag>.FromString("CurrentEnemy");
        public static readonly Key<ServiceTag> CurrentDungeon = Key<ServiceTag>.FromString("CurrentDungeon");
        public static readonly Key<ServiceTag> CurrentEvent = Key<ServiceTag>.FromString("CurrentEvent");
    }
}
