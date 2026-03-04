using DinaCSharp.Core.Utils;
using DinaCSharp.Graphics;
using DinaCSharp.Inputs;
using DinaCSharp.Resources;
using DinaCSharp.Services;
using DinaCSharp.Services.Fonts;
using DinaCSharp.Services.Scenes;

using Dungeon100Steps.Core.Keys;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;


namespace Dungeon100Steps.GameMechanics.Scenes
{
    // Note : Utilisez la propriété 'SceneManager' (héritée) pour accéder au moteur.
    // Ne capturez pas le paramètre 'sceneManager' dans les méthodes pour éviter l'erreur CS9107.
    public class DefeatScene(SceneManager sceneManager) : Scene(sceneManager)
    {
        public event EventHandler OnDefeatCompleted;

        private const int MESSAGE_PANEL_BORDER_THICKNESS = 5;

        private MessageGroup _messageGroup;
        private Panel _background;

        private bool _completed;
        public override void Load()
        {
            var fontManager = ServiceLocator.Get<FontManager>(ServiceKeys.FontManager);
            var resourceManager = ServiceLocator.Get<ResourceManager>(ProjectServiceKeys.AssetsResourceManager);

            var texture = resourceManager.Load<Texture2D>(BackgroundKeys.Defeat);
            _background = new Panel(Vector2.Zero, ScreenDimensions, texture);
            
            var messageFont = fontManager.Load(FontKeys.Messages);
            var continueFont = fontManager.Load(FontKeys.Messages);

            var thickness = UIScaler.Scale(MESSAGE_PANEL_BORDER_THICKNESS);
            _messageGroup = new MessageGroup(messageFont, "YOU_ARE_DEAD", PaletteColors.Message,
                                             continueFont, "RETURN_TO_MAINMENU", PaletteColors.Message_Continue,
                                             ScreenDimensions, PaletteColors.Message_Panel_Background, PaletteColors.Message_Panel_Border,
                                             thickness);
            _messageGroup.Position = new Vector2(thickness, ScreenDimensions.Y - _messageGroup.Dimensions.Y - thickness * 2);
        }
        public override void Reset()
        {
            _completed = false;
        }
        public override void Update(GameTime gametime)
        {
            if (_completed)
                return;

            if (InputManager.IsPressedByAny(PlayerInputKeys.Activate))
            {
                OnDefeatCompleted?.Invoke(this, EventArgs.Empty);
                _completed = true;
            }
        }
        public override void Draw(SpriteBatch spritebatch)
        {
            _background?.Draw(spritebatch);
            _messageGroup?.Draw(spritebatch);
        }
    }
}
