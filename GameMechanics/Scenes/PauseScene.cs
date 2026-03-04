using DinaCSharp.Core.Utils;
using DinaCSharp.Enums;
using DinaCSharp.Graphics;
using DinaCSharp.Resources;
using DinaCSharp.Services;
using DinaCSharp.Services.Fonts;
using DinaCSharp.Services.Menus;
using DinaCSharp.Services.Scenes;

using Dungeon100Steps.Core.Keys;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

namespace Dungeon100Steps.GameMechanics.Scenes
{
    // Note : Utilisez la propriété 'SceneManager' (héritée) pour accéder au moteur.
    // Ne capturez pas le paramètre 'sceneManager' dans les méthodes pour éviter l'erreur CS9107.
    public class PauseScene(SceneManager sceneManager) : Scene(sceneManager)
    {
        private readonly FontManager _fontManager = ServiceLocator.Get<FontManager>(ServiceKeys.FontManager);
        private readonly ResourceManager _resourceManager = ServiceLocator.Get<ResourceManager>(ProjectServiceKeys.AssetsResourceManager);

        private Key<SceneTag> _previousSceneKey;

        private MenuManager _menu;

        public override void Load()
        {
            _menu = CreateMenu();
        }
        public override void Reset()
        {
            _previousSceneKey = SceneManager.GetResource<Key<SceneTag>>("PreviousScene");
        }
        public override void Update(GameTime gametime)
        {
            _menu?.Update(gametime);
        }
        public override void Draw(SpriteBatch spritebatch)
        {
            _menu?.Draw(spritebatch);
        }


        private MenuManager CreateMenu()
        {
            var titleFont = _fontManager.Load(FontKeys.Pause_Title);
            var menuItemFont = _fontManager.Load(FontKeys.Pause_Texts);

            var menu = new MenuManager();

            AddMenuItem(menu, menuItemFont, "PAUSE_CONTINUE", ReturnToPreviousScene);
            AddMenuItem(menu, menuItemFont, "PAUSE_SAVE", SaveGameAndReturnToPreviousScene);

            return menu;
        }

        private void AddMenuItem(MenuManager menu, SpriteFont font, string content, Func<MenuItem, MenuItem> onActivation)
        {
            menu.AddItem(font, content, PaletteColors.Pause_Text,
                         selection: OnMenuItemSelection, deselection: OnMenuItemDeselection,
                         activation: onActivation);
        }


        private MenuItem OnMenuItemSelection(MenuItem menuItem)
        {
            menuItem.Color = PaletteColors.Pause_Text_Selected;
            return menuItem;
        }
        private MenuItem OnMenuItemDeselection(MenuItem menuItem)
        {
            menuItem.Color = PaletteColors.Pause_Text;
            return menuItem;
        }
        private MenuItem ReturnToPreviousScene(MenuItem menuItem)
        {
            SetCurrentScene(_previousSceneKey);
            return menuItem;
        }
        private MenuItem SaveGameAndReturnToPreviousScene(MenuItem menuItem)
        {

            return ReturnToPreviousScene(menuItem);
        }
    }
}
