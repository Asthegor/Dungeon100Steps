using DinaCSharp.Services;
using DinaCSharp.Services.Scenes;

namespace Dungeon100Steps.Core.Keys
{
    public class ProjectSceneKeys
    {
        public static readonly Key<SceneTag> MainMenu = Key<SceneTag>.FromString("MainMenu");
        public static readonly Key<SceneTag> OptionsMenu = Key<SceneTag>.FromString("OptionsMenu");
        public static readonly Key<SceneTag> GameScene = Key<SceneTag>.FromString("GameScene");


        #region Tutorial Scenes
        public static readonly Key<SceneTag> TutorialSkipScene = Key<SceneTag>.FromString("TutorialSkipScene");
        public static readonly Key<SceneTag> TutorialScene = Key<SceneTag>.FromString("TutorialScene");
        #endregion

        #region Game Scene
        public static readonly Key<SceneTag> LoadingScene = Key<SceneTag>.FromString("LoadingScene");
        public static readonly Key<SceneTag> SelectPlayerScene = Key<SceneTag>.FromString("SelectPlayerScene");
        public static readonly Key<SceneTag> CityScene = Key<SceneTag>.FromString("CityScene");
        public static readonly Key<SceneTag> InventoryScene = Key<SceneTag>.FromString("InventoryScene");

        public static readonly Key<SceneTag> PauseScene = Key<SceneTag>.FromString("PauseScene");
        public static readonly Key<SceneTag> DefeatScene = Key<SceneTag>.FromString("DefeatScene");
        public static readonly Key<SceneTag> VictoryScene = Key<SceneTag>.FromString("VictoryScene");
        #endregion

        #region Scenes inside the Dungeon
        public static readonly Key<SceneTag> CombatScene = Key<SceneTag>.FromString("CombatScene");
        public static readonly Key<SceneTag> TreasureScene = Key<SceneTag>.FromString("TreasureScene");
        public static readonly Key<SceneTag> TrapScene = Key<SceneTag>.FromString("TrapScene");
        public static readonly Key<SceneTag> NarrativeScene = Key<SceneTag>.FromString("NarrativeScene");
        public static readonly Key<SceneTag> WaitingScene = Key<SceneTag>.FromString("WaitingScene");
        #endregion

        #region City Scenes
        public static readonly Key<SceneTag> BlacksmithScene = Key<SceneTag>.FromString("BlacksmithScene");
        public static readonly Key<SceneTag> HerboristScene = Key<SceneTag>.FromString("HerboristScene");
        public static readonly Key<SceneTag> TavernScene = Key<SceneTag>.FromString("TavernScene");
        #endregion

        public static readonly Key<SceneTag> TestScene = Key<SceneTag>.FromString("TestScene");
    }
}
