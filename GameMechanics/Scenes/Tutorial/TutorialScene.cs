using DinaCSharp.Core;
using DinaCSharp.Core.Utils;
using DinaCSharp.Graphics;
using DinaCSharp.Resources;
using DinaCSharp.Services;
using DinaCSharp.Services.Scenes;

using Dungeon100Steps.Core;
using Dungeon100Steps.Core.Datas.Dungeons;
using Dungeon100Steps.Core.Datas.Events;
using Dungeon100Steps.Core.Keys;
using Dungeon100Steps.GameMechanics.Scenes.Events;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

namespace Dungeon100Steps.GameMechanics.Scenes.Tutorial
{
    // Note : Utilisez la propriété 'SceneManager' (héritée) pour accéder au moteur.
    // Ne capturez pas le paramètre 'sceneManager' dans les méthodes pour éviter l'erreur CS9107.
    public class TutorialScene(SceneManager sceneManager) : Scene(sceneManager)
    {
        public event EventHandler<TutorialEventArgs> OnTutorialCompleted;

        private readonly Vector2 NEXT_BUTTON_DIMENSIONS = new Vector2();
        private readonly Vector2 NEXT_BUTTON_OFFSET = new Vector2(20, 20);

        private SceneManager _tutorialSceneManager;
        private bool _loadingFinished;
        private Dungeon _dungeon;

        private Group _introGroup1; // Message de bienvenue et indication des types d'événements
        private Group _introGroup2; // Explication des combats
        private Group _introGroup3; // Explication des trésors
        private Group _introGroup4; // Explication des pièges
        private Group _introGroup5; // Explication des événements narratifs
        private Group _introGroup6; // Conclusion de l'introduction

        private enum TutorialStage { Intro1, Intro2, Intro3, Intro4, Intro5, Intro6, InGame }
        private TutorialStage _currentStage = TutorialStage.Intro1;
        public override void Load()
        {
            _tutorialSceneManager = SceneManager.CreateNewInstance("GameContent");

            RegisterScenes();

            _dungeon = DungeonFactory.GenerateTutorial();

            _introGroup1 = CreateIntroGroup1();
            _introGroup2 = CreateIntroGroup2();
            _introGroup3 = CreateIntroGroup3();
            _introGroup4 = CreateIntroGroup4();
            _introGroup5 = CreateIntroGroup5();
            _introGroup6 = CreateIntroGroup6();
        }
        public override void Reset()
        {
            _loadingFinished = true;
        }
        public override void Update(GameTime gametime)
        {
            if (!_loadingFinished)
                return;

            switch (_currentStage)
            {
                case TutorialStage.Intro1:
                    _introGroup1?.Update(gametime);
                    break;
                case TutorialStage.Intro2:
                    _introGroup2?.Update(gametime);
                    break;
                case TutorialStage.Intro3:
                    _introGroup3?.Update(gametime);
                    break;
                case TutorialStage.Intro4:
                    _introGroup4?.Update(gametime);
                    break;
                case TutorialStage.Intro5:
                    _introGroup5?.Update(gametime);
                    break;
                case TutorialStage.Intro6:
                    _introGroup6?.Update(gametime);
                    break;
                case TutorialStage.InGame:
                    _tutorialSceneManager?.Update(gametime);
                    break;
            }
        }
        public override void Draw(SpriteBatch spritebatch)
        {
            switch(_currentStage)
            {
                case TutorialStage.Intro1:
                    _introGroup1?.Draw(spritebatch);
                    break;
                case TutorialStage.Intro2:
                    _introGroup2?.Draw(spritebatch);
                    break;
                case TutorialStage.Intro3:
                    _introGroup3?.Draw(spritebatch);
                    break;
                case TutorialStage.Intro4:
                    _introGroup4?.Draw(spritebatch);
                    break;
                case TutorialStage.Intro5:
                    _introGroup5?.Draw(spritebatch);
                    break;
                case TutorialStage.Intro6:
                    _introGroup6?.Draw(spritebatch);
                    break;
                case TutorialStage.InGame:
                    _tutorialSceneManager?.Draw(spritebatch);
                    break;
            }
        }

        private void RegisterScenes()
        {
            _tutorialSceneManager.AddScene(ProjectSceneKeys.CombatScene, () => new CombatScene(_tutorialSceneManager));
            _tutorialSceneManager.AddScene(ProjectSceneKeys.TreasureScene, () => new TreasureScene(_tutorialSceneManager));
            _tutorialSceneManager.AddScene(ProjectSceneKeys.TrapScene, () => new TrapScene(_tutorialSceneManager));
            _tutorialSceneManager.AddScene(ProjectSceneKeys.NarrativeScene, () => new NarrativeScene(_tutorialSceneManager));
        }
        private void LoadNextEvent()
        {
            Event currentEvent = _dungeon.NextEvent();
            ServiceLocator.Register(ProjectServiceKeys.CurrentEvent, currentEvent);

            switch (currentEvent)
            {
                case CombatEvent:
                    _tutorialSceneManager.SetCurrentScene(ProjectSceneKeys.CombatScene);
                    break;
                case TreasureEvent:
                    _tutorialSceneManager.SetCurrentScene(ProjectSceneKeys.TreasureScene);
                    break;
                case TrapEvent:
                    _tutorialSceneManager.SetCurrentScene(ProjectSceneKeys.TrapScene);
                    break;
                case NarrativeEvent:
                    _tutorialSceneManager.SetCurrentScene(ProjectSceneKeys.NarrativeScene);
                    break;
                default:
                    throw new InvalidOperationException("Type d'événement inconnu dans le tutoriel.");
            }
        }
        public override void ClearEventSubscribers()
        {
            // Désabonnement pour éviter les fuites mémoire
            _tutorialSceneManager?.Dispose();
            base.ClearEventSubscribers();
        }

        private Group CreateIntroGroup1()
        {
            var group = new Group();

            var nextButton = CreateNextButton();
            group.Add(nextButton);
            return group;
        }
        private Group CreateIntroGroup2()
        {
            var group = new Group();

            var nextButton = CreateNextButton();
            group.Add(nextButton);
            return group;
        }
        private Group CreateIntroGroup3()
        {
            var group = new Group();

            var nextButton = CreateNextButton();
            group.Add(nextButton);
            return group;
        }
        private Group CreateIntroGroup4()
        {
            var group = new Group();

            var nextButton = CreateNextButton();
            group.Add(nextButton);
            return group;
        }
        private Group CreateIntroGroup5()
        {
            var group = new Group();

            var nextButton = CreateNextButton();
            group.Add(nextButton);
            return group;
        }
        private Group CreateIntroGroup6()
        {
            var group = new Group();

            var nextButton = CreateNextButton();
            group.Add(nextButton);
            return group;
        }
        private Button CreateNextButton()
        {
            var resourceManager = ServiceLocator.Get<ResourceManager>(ProjectServiceKeys.AssetsResourceManager);
            var backgroundImage = resourceManager.Load<Texture2D>(GameResourceKeys.Button_Next);
            var pos = ScreenDimensions - UIScaler.Scale(NEXT_BUTTON_DIMENSIONS) - UIScaler.Scale(NEXT_BUTTON_OFFSET);
            return new Button(pos, backgroundImage, onClick: GoToNextState);
        }
        private void GoToNextState(Button button)
        {
            _currentStage++;
        }
    }
}
