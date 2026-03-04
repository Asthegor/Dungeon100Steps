using DinaCSharp.Services.Scenes;

using Dungeon100Steps.Core;
using Dungeon100Steps.Core.Datas.Events;
using Dungeon100Steps.Core.Datas.Items;
using Dungeon100Steps.Core.Keys;
using Dungeon100Steps.GameMechanics.Scenes.City;
using Dungeon100Steps.GameMechanics.Scenes.Events;
using Dungeon100Steps.GameMechanics.Scenes.Tutorial;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Dungeon100Steps.GameMechanics.Scenes
{
    public class GameScene(SceneManager sceneManager) : Scene(sceneManager)
    {
        private SceneManager _gameSceneManager;

        public override void Load()
        {
            _gameSceneManager = SceneManager.CreateNewInstance("AssetsContent");
            RegisterSubScenes();
        }
        public override void Reset()
        {
            _gameSceneManager.SetCurrentScene(ProjectSceneKeys.TutorialSkipScene);
        }
        public override void Update(GameTime gametime)
        {
            _gameSceneManager.Update(gametime);
        }
        public override void Draw(SpriteBatch spritebatch)
        {
            _gameSceneManager.Draw(spritebatch);
        }

        public void RegisterSubScenes()
        {
            _gameSceneManager.AddScene(ProjectSceneKeys.SelectPlayerScene, () => new SelectPlayerScene(_gameSceneManager));
            
            _gameSceneManager.AddScene(ProjectSceneKeys.TutorialSkipScene, () => new TutorialSkipScene(_gameSceneManager));
            _gameSceneManager.AddScene(ProjectSceneKeys.TutorialScene, () => new TutorialScene(_gameSceneManager));
            
            _gameSceneManager.AddScene(ProjectSceneKeys.CityScene, () => new CityScene(_gameSceneManager));
            _gameSceneManager.AddScene(ProjectSceneKeys.BlacksmithScene, () => new BlacksmithScene(_gameSceneManager));
            _gameSceneManager.AddScene(ProjectSceneKeys.HerboristScene, () => new HerboristScene(_gameSceneManager));
            _gameSceneManager.AddScene(ProjectSceneKeys.TavernScene, () => new TavernScene(_gameSceneManager));
            _gameSceneManager.AddScene(ProjectSceneKeys.InventoryScene, () => new InventoryScene(_gameSceneManager));

            _gameSceneManager.AddScene(ProjectSceneKeys.WaitingScene, () => new WaitingScene(_gameSceneManager));
            _gameSceneManager.AddScene(ProjectSceneKeys.CombatScene, () => new CombatScene(_gameSceneManager));
            _gameSceneManager.AddScene(ProjectSceneKeys.NarrativeScene, () => new NarrativeScene(_gameSceneManager));
            _gameSceneManager.AddScene(ProjectSceneKeys.TrapScene, () => new TrapScene(_gameSceneManager));
            _gameSceneManager.AddScene(ProjectSceneKeys.TreasureScene, () => new TreasureScene(_gameSceneManager));

            _gameSceneManager.AddScene(ProjectSceneKeys.PauseScene, () => new PauseScene(_gameSceneManager));
            
            var defeatScene = new DefeatScene(_gameSceneManager);
            defeatScene.OnDefeatCompleted += (s, e) => SetCurrentScene(ProjectSceneKeys.MainMenu);
            _gameSceneManager.AddScene(ProjectSceneKeys.DefeatScene, () => defeatScene);

            _gameSceneManager.AddScene(ProjectSceneKeys.VictoryScene, () => new VictoryScene(_gameSceneManager));
        }

        private void OnTutorialCompleted(object sender, TutorialEventArgs e)
        {
            if (e.Result == EventResult.Defeat)
            {
                // TODO: Gérer l'effacement des données sauvegardées si le joueur perd le tutoriel ?
                SetCurrentScene(ProjectSceneKeys.MainMenu);
                return;
            }
            _gameSceneManager.SetCurrentScene(ProjectSceneKeys.CityScene);
        }

    }
}
