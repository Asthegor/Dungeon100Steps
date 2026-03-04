using DinaCSharp.Core;
using DinaCSharp.Core.Utils;
using DinaCSharp.Enums;
using DinaCSharp.Graphics;
using DinaCSharp.Services;
using DinaCSharp.Services.Fonts;
using DinaCSharp.Services.Scenes;

using Dungeon100Steps.Core.Keys;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Dungeon100Steps.GameMechanics.Scenes.Tutorial
{
    // Note : Utilisez la propriété 'SceneManager' (héritée) pour accéder au moteur.
    // Ne capturez pas le paramètre 'sceneManager' dans les méthodes pour éviter l'erreur CS9107.
    public class TutorialSkipScene(SceneManager sceneManager) : Scene(sceneManager)
    {
        private const int TUTORIALSKIP_PANEL_THICKNESS = 4;
        private Vector2 TUTORIALSKIP_BUTTON_DIMENSIONS = new Vector2(80, 30);

        private readonly FontManager _fontManager = ServiceLocator.Get<FontManager>(ServiceKeys.FontManager);

        private Group _tutorialSkipGroup;
        public override void Load()
        {
            _tutorialSkipGroup = CreateTutorialSkipGroup();
            _tutorialSkipGroup.Position = (ScreenDimensions - _tutorialSkipGroup.Dimensions) / 2;
        }
        public override void Reset()
        {
        }
        public override void Update(GameTime gametime)
        {
            _tutorialSkipGroup?.Update(gametime);
        }
        public override void Draw(SpriteBatch spritebatch)
        {
            _tutorialSkipGroup?.Draw(spritebatch);
        }
        #region Création de la fenêtre pour annuler le tutoriel
        private Group CreateTutorialSkipGroup()
        {
            var group = new Group();

            var backgroundPanel = new Panel(position: default, dimensions: ScreenDimensions / 4,
                                            backgroundcolor: PaletteColors.TutorialSkip_Panel_Background,
                                            bordercolor: PaletteColors.TutorialSkip_Panel_Border,
                                            thickness: UIScaler.Scale(TUTORIALSKIP_PANEL_THICKNESS));//,
                                            //withroundcorner: true, radius: UIScaler.Scale(TUTORIALSKIP_PANEL_THICKNESS));
            group.Add(backgroundPanel);

            var messageFont = _fontManager.Load(FontKeys.TutorialSkip_Message);
            var message = new Text(messageFont, "TUTORIALSKIP_MESSAGE", PaletteColors.TutorialSkip_Label,
                                   horizontalalignment: HorizontalAlignment.Center,
                                   verticalalignment: VerticalAlignment.Center)
            {
                Dimensions = new Vector2(backgroundPanel.Dimensions.X, backgroundPanel.Dimensions.Y / 2)
            };
            group.Add(message);


            var buttonFont = _fontManager.Load(FontKeys.TutorialSkip_Button_Label);
            var pos = new Vector2(backgroundPanel.Dimensions.X / 2 - UIScaler.Scale(TUTORIALSKIP_BUTTON_DIMENSIONS.X) * 1.5f,
                                  backgroundPanel.Dimensions.Y * 3 / 4 - UIScaler.Scale(TUTORIALSKIP_BUTTON_DIMENSIONS.X) / 2f);
            var noButton = new Button(position: pos, dimensions: UIScaler.Scale(TUTORIALSKIP_BUTTON_DIMENSIONS),
                                      font: buttonFont, "TUTORIALSKIP_NO", textColor: PaletteColors.TutorialSkip_Label,
                                      onClick: LaunchGame, onHover: OnHoverNoButton)// ,
                                      //withroundcorner: true, cornerradius: UIScaler.Scale(TUTORIALSKIP_BUTTON_CORNER_RADIUS))
            {
                BorderColor = PaletteColors.TutorialSkip_NoButton_Border,
                BackgroundColor = PaletteColors.TutorialSkip_NoButton_Background,
                BorderThickness = UIScaler.Scale(TUTORIALSKIP_PANEL_THICKNESS)
            };
            group.Add(noButton);

            pos = new Vector2(backgroundPanel.Dimensions.X / 2 + UIScaler.Scale(TUTORIALSKIP_BUTTON_DIMENSIONS.X) / 2f,
                              backgroundPanel.Dimensions.Y * 3 / 4 - UIScaler.Scale(TUTORIALSKIP_BUTTON_DIMENSIONS.X) / 2f);
            var yesButton = new Button(position: pos, dimensions: UIScaler.Scale(TUTORIALSKIP_BUTTON_DIMENSIONS),
                                       font: buttonFont, "TUTORIALSKIP_YES", textColor: PaletteColors.TutorialSkip_Label,
                                       onClick: LaunchTutorial, onHover: OnHoverYesButton) //,
                                       //withroundcorner: true, cornerradius: UIScaler.Scale(TUTORIALSKIP_BUTTON_CORNER_RADIUS))
            {
                BorderColor = PaletteColors.TutorialSkip_YesButton_Border,
                BackgroundColor = PaletteColors.TutorialSkip_YesButton_Background,
                BorderThickness = UIScaler.Scale(TUTORIALSKIP_PANEL_THICKNESS)
            };
            group.Add(yesButton);

            return group;
        }
        private void LaunchTutorial(Button button)
        {
            SceneManager.SetCurrentScene(ProjectSceneKeys.TutorialScene);
        }
        private static void OnHoverNoButton(Button button)
        {
            button.BorderColor = PaletteColors.TutorialSkip_NoButton_Hovered;
        }
        private void LaunchGame(Button button)
        {
            SceneManager.SetCurrentScene(ProjectSceneKeys.SelectPlayerScene);
        }
        private static void OnHoverYesButton(Button button)
        {
            button.BorderColor = PaletteColors.TutorialSkip_YesButton_Hovered;
        }
        #endregion

    }
}
