using DinaCSharp.Core.Utils;
using DinaCSharp.Graphics;
using DinaCSharp.Resources;
using DinaCSharp.Services;
using DinaCSharp.Services.Fonts;
using DinaCSharp.Services.Scenes;

using Dungeon100Steps.Core.Keys;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Diagnostics;

namespace Dungeon100Steps.GameMechanics.Scenes.City
{
    // Note : Utilisez la propriété 'SceneManager' (héritée) pour accéder au moteur.
    // Ne capturez pas le paramètre 'sceneManager' dans les méthodes pour éviter l'erreur CS9107.
    public class BlacksmithScene(SceneManager sceneManager) : Scene(sceneManager)
    {
        private readonly Vector2 BUTTON_NEXT_DIMENSIONS = new Vector2(136, 80);

        private readonly FontManager _fontManager = ServiceLocator.Get<FontManager>(ServiceKeys.FontManager);
        private readonly ResourceManager _resourceManager = ServiceLocator.Get<ResourceManager>(ProjectServiceKeys.AssetsResourceManager);

        private Button _backButton;
        public override void Load()
        {

            _backButton = new Button(position: new Vector2(ScreenDimensions.X * 4 / 5, ScreenDimensions.Y * 7 / 8),
                         dimensions: UIScaler.Scale(BUTTON_NEXT_DIMENSIONS),
                         font: _fontManager.Load(FontKeys.BackButton_Text),
                         content: "UI_BACK",
                         textColor: PaletteColors.BackButton_Text,
                         backgroundImage: _resourceManager.Load<Texture2D>(GameResourceKeys.Button_Next),
                         onClick: ReturnToPreviousScene, onHover: OnButtonNextHovered);
        }
        public override void Reset()
        {
            Trace.WriteLine(GetType().Name);
        }
        public override void Update(GameTime gametime)
        {
            _backButton?.Update(gametime);
        }
        public override void Draw(SpriteBatch spritebatch)
        {
            _backButton?.Draw(spritebatch);
        }


        #region Back Button
        private void ReturnToPreviousScene(Button button)
        {
            SetCurrentScene(ProjectSceneKeys.CityScene);
        }
        private void OnButtonNextHovered(Button button)
        {
            button.TextColor = PaletteColors.BackButton_Text_Hovered;
        }
        #endregion
    }
}
