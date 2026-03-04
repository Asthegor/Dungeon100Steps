using DinaCSharp.Services.Scenes;

using Dungeon100Steps.Core.Keys;
using Dungeon100Steps.GameMechanics.Scenes;
using Dungeon100Steps.UI.Scenes;

namespace Dungeon100Steps.UI
{
    public static class UISceneRegistry
    {
        public static void RegisterScenes(SceneManager sceneManager)
        {
            sceneManager.AddScene(ProjectSceneKeys.MainMenu, () => new MainMenuScene(sceneManager));
            sceneManager.AddScene(ProjectSceneKeys.OptionsMenu, () => new OptionsMenuScene(sceneManager));
            sceneManager.AddScene(ProjectSceneKeys.SelectPlayerScene, () => new SelectPlayerScene(sceneManager));
        }
    }
}
