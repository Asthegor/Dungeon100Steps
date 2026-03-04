using DinaCSharp.Core;
using DinaCSharp.Graphics;
using DinaCSharp.Inputs;
using DinaCSharp.Resources;
using DinaCSharp.Services;
using DinaCSharp.Services.Fonts;
using DinaCSharp.Services.Scenes;

using Dungeon100Steps.Core;
using Dungeon100Steps.Core.Datas.Characters;
using Dungeon100Steps.Core.Datas.Dungeons;
using Dungeon100Steps.Core.Datas.Events;
using Dungeon100Steps.Core.Keys;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Diagnostics;

namespace Dungeon100Steps.GameMechanics.Scenes.Events
{
    // Note : Utilisez la propriété 'SceneManager' (héritée) pour accéder au moteur.
    // Ne capturez pas le paramètre 'sceneManager' dans les méthodes pour éviter l'erreur CS9107.
    public class TreasureScene(SceneManager sceneManager) : Scene(sceneManager)
    {
        private readonly FontManager _fontManager = ServiceLocator.Get<FontManager>(ServiceKeys.FontManager);
        private readonly ResourceManager _resourceManager = ServiceLocator.Get<ResourceManager>(ProjectServiceKeys.AssetsResourceManager);

        private const int MESSAGE_PANEL_BORDER_THICKNESS = 5;

        private Player _player;
        private Dungeon _currentDungeon;
        private TreasureEvent _currentEvent;

        private EventState _currentState;

        private Group _messageGroup;
        private Text _messageText;
        private Text _continueText;
        private Panel _messagePanel;
        public override void Load()
        {
            _player = ServiceLocator.Get<Player>(ProjectServiceKeys.Player);

            var messageFont = _fontManager.Load(FontKeys.Messages);
            var continueFont = _fontManager.Load(FontKeys.Messages);
            _messageGroup = EventUtils.CreateMessageGroup(messageFont, "TREASURE_DEFAULT", continueFont, "PRESS_TO_CONTINUE",
                                                          ScreenDimensions, MESSAGE_PANEL_BORDER_THICKNESS,
                                                          out _messageText, out _messagePanel, out _continueText);
        }
        public override void Reset()
        {
            _currentDungeon = ServiceLocator.Get<Dungeon>(ProjectServiceKeys.CurrentDungeon)
                ?? throw new InvalidOperationException("Aucun donjon trouvé.");
            _currentEvent = ServiceLocator.Get<TreasureEvent>(ProjectServiceKeys.CurrentEvent) 
                ?? throw new InvalidOperationException($"CurrentEvent n'est pas de type '{_currentEvent.GetType().Name}' dans le ServiceLocator.");
#if DEBUG
            Trace.WriteLine(GetType().Name);
#endif
            _currentState = EventState.ShowingMessage;
        }
        public override void Update(GameTime gametime)
        {
            //switch 
            if (_player.IsDead)
            {
                GoToWaitingScene(EventResult.Defeat);
                return;
            }
#if DEBUG
            if (InputManager.IsPressedByAny(PlayerInputKeys.Activate))
                GoToWaitingScene(EventResult.Victory);
#endif

        }
        public override void Draw(SpriteBatch spritebatch)
        {
        }
        private void GoToWaitingScene(EventResult result)
        {
            _currentEvent.Result = result;
            SetCurrentScene(ProjectSceneKeys.WaitingScene);
        }
    }
}
