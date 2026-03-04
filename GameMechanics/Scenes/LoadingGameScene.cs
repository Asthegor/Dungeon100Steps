using DinaCSharp.Core.Utils;
using DinaCSharp.Graphics;
using DinaCSharp.Interfaces;
using DinaCSharp.Services;
using DinaCSharp.Services.Fonts;
using DinaCSharp.Services.Scenes;

using Dungeon100Steps.Core.Keys;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Dungeon100Steps.GameMechanics.Scenes
{
    public class LoadingGameScene(SceneManager sceneManager) : Scene(sceneManager), ILoadingScreen
    {
        private readonly Vector2 MESSAGE_OFFSET = new Vector2(0, 10);

        private readonly FontManager _fontManager = ServiceLocator.Get<FontManager>(ServiceKeys.FontManager);

        public string Message { get => _message.Content; set => _message.Content = value; }

        private ProgressBar _progressBar;
        private Text _message;

        public override void Load()
        {
            //var titleFont = _fontManager.Load(FontKeys.Loading_Title);
            var messageFont = _fontManager.Load(FontKeys.Loading_Message);

            // Création de la barre de progression
            Vector2 progressBarPosition = new Vector2(ScreenDimensions.X * 0.1f, ScreenDimensions.Y / 2 + messageFont.LineSpacing);
            Vector2 progressBarDimensions = new Vector2(ScreenDimensions.X * 0.8f, messageFont.LineSpacing);
            _progressBar = new ProgressBar(value: 0, minValue: 0, maxValue: 100, 
                                           position: progressBarPosition, dimensions: progressBarDimensions, 
                                           frontColor: PaletteColors.Loading_Progress_Front,
                                           borderColor: PaletteColors.Loading_Progress_Border,
                                           backColor: PaletteColors.Loading_Progress_Back,
                                           borderThickness: 2);
            // Création du texte
            _message = new Text(font: messageFont, content: "", color: PaletteColors.Loading_Message,
                                position: progressBarPosition + new Vector2(0, progressBarDimensions.Y) + UIScaler.Scale(MESSAGE_OFFSET));


        }
        public override void Reset()
        {
            LoadingProgress = 0;
        }
        public override void Update(GameTime gametime)
        {
            if (_progressBar != null)
            {
                _progressBar.Value = LoadingProgress * 100;
                _progressBar.Update(gametime);
            }
            _message?.Update(gametime);

        }
        public override void Draw(SpriteBatch spritebatch)
        {
            // Rajouter les autres contrôles ici

            // Toujours laisser ces 2 contrôles à la fin
            _progressBar?.Draw(spritebatch);
            _message?.Draw(spritebatch);
        }
    }
}
