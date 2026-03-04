using DinaCSharp.Core.Utils;
using DinaCSharp.Enums;
using DinaCSharp.Graphics;
using DinaCSharp.Resources;
using DinaCSharp.Services;
using DinaCSharp.Services.Audio;
using DinaCSharp.Services.Fonts;
using DinaCSharp.Services.Menus;
using DinaCSharp.Services.Scenes;

using Dungeon100Steps.Core.Keys;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

namespace Dungeon100Steps.UI.Scenes
{
    public class MainMenuScene(SceneManager sceneManager) : Scene(sceneManager)
    {
        private const float MENU_SPACING_X = 0;
        private const float MENU_SPACING_Y = 20;

        private Vector2 MENU_SPACING = new Vector2(MENU_SPACING_X, MENU_SPACING_Y);
        private Vector2 MENU_OFFSET_TITLE_SHADOW = new Vector2(3, 3);
        private Vector2 MENU_TITLE_POSITION = new Vector2(0, sceneManager.ScreenDimensions.Y / 10);

        private FontManager _fontManager;
        private SoundManager _soundManager;
        private ResourceManager _resourceManager;
        private MenuManager _menuManager;

        private Panel _background;

        private bool _savedGameFound;
        public override void Load()
        {
            _savedGameFound = false;
            LoadingManagers();
            LoadingBackground();
            CreateMenu();
        }
        public override void Reset()
        {
            _menuManager?.Reset();
        }
        public override void Update(GameTime gametime)
        {
            _menuManager?.Update(gametime);
        }
        public override void Draw(SpriteBatch spritebatch)
        {
            _background?.Draw(spritebatch);
            _menuManager?.Draw(spritebatch);
        }

        private void LoadingManagers()
        {
            _fontManager = ServiceLocator.Get<FontManager>(ServiceKeys.FontManager);
            _soundManager = ServiceLocator.Get<SoundManager>(ProjectServiceKeys.SoundManager);
            _resourceManager = ServiceLocator.Get<ResourceManager>(ProjectServiceKeys.UIResourceManager);
            _menuManager = new MenuManager(MENU_SPACING);
        }
        private void LoadingBackground()
        {
            Texture2D backgroundTexture = _resourceManager.Load<Texture2D>(UIResourceKeys.MainMenu_Background);
            _background = new Panel(default, default, backgroundTexture, 0);
            _background.Position = (ScreenDimensions - UIScaler.Scale(_background.Dimensions)) / 2;
        }
        private void CreateMenu()
        {
            // Polices du titre et des items de menu
            // (définies avec la police par défaut)
            SpriteFont titleFont = _fontManager.Load(FontKeys.MainMenu_Title);
            SpriteFont menuItemFont = _fontManager.Load(FontKeys.MainMenu_MenuItems);

            #region Ajout du titre
            var mainTitle = _menuManager.AddTitle(titleFont, "GAME_TITLE",
                                                  MENU_TITLE_POSITION, PaletteColors.MainMenu_Title,
                                                  PaletteColors.MainMenu_Title_Shadow, MENU_OFFSET_TITLE_SHADOW);

            _menuManager.CenterTitles(ScreenDimensions);
            #endregion


            AddMenuItem(menuItemFont, "MAINMENU_CONTINUE", LoadGame, !_savedGameFound);
            AddMenuItem(menuItemFont, "MAINMENU_PLAY", LaunchGame);
            AddMenuItem(menuItemFont, "MAINMENU_OPTIONS", LaunchOptions);
            AddMenuItem(menuItemFont, "MAINMENU_QUIT", QuitGame);
            _menuManager.CenterMenuItems(ScreenDimensions);
            _menuManager.ItemsPosition = new Vector2((ScreenDimensions.X - _menuManager.ItemsDimensions.X) / 2, ScreenDimensions.Y / 2);

            // Ajout des touches pour le menu
            _menuManager.SetActionKeys(
                (MenuAction.Up, PlayerInputKeys.Up),
                (MenuAction.Down, PlayerInputKeys.Down),
                (MenuAction.Left, PlayerInputKeys.Left),
                (MenuAction.Right, PlayerInputKeys.Right),
                (MenuAction.Activate, PlayerInputKeys.Activate),
                (MenuAction.Cancel, PlayerInputKeys.Cancel)
                );

        }


        #region Fonctions utilitaires
        private void AddMenuItem(SpriteFont menuItemFont, string key, Func<MenuItem, MenuItem> onClick, bool disabled = false)
        {
            var item = _menuManager.AddItem(menuItemFont, key, PaletteColors.MenuItem, Selection, Deselection, onClick, HorizontalAlignment.Center);
            if (disabled)
            {
                item.State = MenuItemState.Disable;
                item.DisableColor = PaletteColors.MenuItem_Disabled;
            }
        }
        #endregion


        #region Fonctions génériques du menu
        private MenuItem Selection(MenuItem item)
        {
            item.Color = PaletteColors.MenuItem_Hovered;
            return item;
        }
        private MenuItem Deselection(MenuItem item)
        {
            item.Color = PaletteColors.MenuItem;
            return item;
        }
        private MenuItem LaunchGame(MenuItem item)
        {
            _soundManager.StopSong();
            SetCurrentScene(ProjectSceneKeys.GameScene, true);
            return item;
        }
        private MenuItem LoadGame(MenuItem item)
        {
            _soundManager.StopSong();
            //TODO: [MAINMENU] Chargement de la sauvegarde
            SetCurrentScene(ProjectSceneKeys.GameScene);
            return item;
        }
        private MenuItem LaunchOptions(MenuItem item)
        {
            _soundManager.StopSong();
            SetCurrentScene(ProjectSceneKeys.OptionsMenu);
            return item;
        }
        private MenuItem QuitGame(MenuItem item)
        {
            Exit();
            return item;
        }
        #endregion
    }
}
