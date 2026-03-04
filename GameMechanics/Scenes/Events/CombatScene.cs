using DinaCSharp.Core;
using DinaCSharp.Core.Utils;
using DinaCSharp.Graphics;
using DinaCSharp.Inputs;
using DinaCSharp.Services;
using DinaCSharp.Services.Fonts;
using DinaCSharp.Services.Menus;
using DinaCSharp.Services.Scenes;

using Dungeon100Steps.Core;
using Dungeon100Steps.Core.Datas.Characters;
using Dungeon100Steps.Core.Datas.Dungeons;
using Dungeon100Steps.Core.Datas.Enemies;
using Dungeon100Steps.Core.Datas.Events;
using Dungeon100Steps.Core.Datas.Items;
using Dungeon100Steps.Core.Keys;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Dungeon100Steps.GameMechanics.Scenes.Events
{
    public class CombatScene(SceneManager sceneManager) : Scene(sceneManager)
    {
        private const int COMBAT_MENU_BORDER_THICKNESS = 8;
        private readonly Vector2 COMBAT_MENU_BORDER_SPACING = new Vector2(15, 15);

        private readonly Vector2 PANEL_DIMENSIONS = new Vector2(192, 384);
        private readonly Vector2 PROGRESSBAR_DIMENSIONS = new Vector2(192, 15);
        private const float HEALTH_OFFSET_Y = 10;
        private const float MANA_OFFSET_Y = 5;
        private const int PROGRESSBAR_BORDER_THICKNESS = 2;

        private const float PLAYERMENU_OFFSET_Y = 20;

        private readonly FontManager _fontManager = ServiceLocator.Get<FontManager>(ServiceKeys.FontManager);

        private Player _player;
        private Group _playerGroup;
        private ProgressBar _playerHealthBar;
        private ProgressBar _playerManaBar;
        private MenuManager _playerMenu;
        private MenuManager _playerItemsMenu;

        private Enemy _enemy;
        private Group _enemyGroup;
        private ProgressBar _enemyHealthBar;
        private ProgressBar _enemyManaBar;

        private readonly Dictionary<MenuItem, Potion> _potionsMenuItems = [];

        private Dungeon _currentDungeon;
        private CombatEvent _currentEvent;
        private EventState _currentState;
        public override void Load()
        {
            _player = ServiceLocator.Get<Player>(ProjectServiceKeys.Player);

            _playerGroup = CreateTextureHealthAndManaGroup(_player, out _playerHealthBar, out _playerManaBar);
            _playerGroup.Position = new Vector2(ScreenDimensions.X * 3 / 4 - (_playerGroup.Dimensions.X / 2),
                                                (ScreenDimensions.Y - _playerGroup.Dimensions.Y) / 2);

            _playerMenu = CreatePlayerMenu();
            _playerMenu.ItemsPosition = _playerGroup.Position + new Vector2(0, _playerGroup.Dimensions.Y + UIScaler.Scale(PLAYERMENU_OFFSET_Y));

            _playerItemsMenu = CreatePlayerItemsMenu();
            _playerItemsMenu.ItemsPosition = _playerMenu.ItemsPosition + new Vector2(_playerMenu.ItemsDimensions.X, 0);

            _player.OnStatsChanged += OnPlayerStatsChanged;

        }
        public override void Reset()
        {
            _currentDungeon = ServiceLocator.Get<Dungeon>(ProjectServiceKeys.CurrentDungeon)
                ?? throw new InvalidOperationException("Aucun donjon trouvé.");
            _currentEvent = ServiceLocator.Get<CombatEvent>(ProjectServiceKeys.CurrentEvent)
                ?? throw new InvalidOperationException($"CurrentEvent n'est pas de type '{_currentEvent.GetType().Name}' dans le ServiceLocator.");

            _currentState = EventState.ShowingMessage;

            _enemy = _currentEvent.Enemy;
            _enemy.OnStatsChanged += OnEnemyStatsChanged;

            _enemyGroup = CreateTextureHealthAndManaGroup(_enemy, out _enemyHealthBar, out _enemyManaBar);
            _enemyGroup.Position = new Vector2(ScreenDimensions.X * 1 / 4 - (_enemyGroup.Dimensions.X / 2),
                                               (ScreenDimensions.Y - _enemyGroup.Dimensions.Y) / 2);
        }
        public override void Update(GameTime gametime)
        {
            switch (_currentState)
            {
                case EventState.ShowingMessage:
                    if (InputManager.IsPressedByAny(PlayerInputKeys.Activate))
                    {
                        _currentState = EventState.Action;
                    }
                    break;
                case EventState.Action:
                    _player.Update(gametime);
                    if (!_playerItemsMenu.Visible)
                        _playerMenu?.Update(gametime);
                    else
                        _playerItemsMenu?.Update(gametime);
                    break;
                case EventState.ShowingResult:
                    if (InputManager.IsPressedByAny(PlayerInputKeys.Activate))
                    {
                        if (_enemy.IsDead)
                        {
                            GoToWaitingScene(EventResult.Victory);
                            return;
                        }
                        if (_player.IsDead)
                        {
                            GoToWaitingScene(EventResult.Defeat);
                            return;
                        }
                    }
                    break;
            }
        }
        public override void Draw(SpriteBatch spritebatch)
        {
            _playerGroup?.Draw(spritebatch);
            _enemyGroup?.Draw(spritebatch);

            switch (_currentState)
            {
                case EventState.ShowingMessage:
                    break;
                case EventState.Action:
                    _playerMenu?.Draw(spritebatch);
                    _playerItemsMenu?.Draw(spritebatch);
                    break;
                case EventState.ShowingResult:
                    break;
            }
        }

        private Group CreateTextureHealthAndManaGroup(Character entity, out ProgressBar healthBar, out ProgressBar manaBar)
        {
            var group = new Group();
            var entityPanel = new Panel(default, UIScaler.Scale(PANEL_DIMENSIONS), entity.Texture);
            group.Add(entityPanel);

            var pos = new Vector2(entityPanel.Position.X, entityPanel.Dimensions.Y + UIScaler.Scale(HEALTH_OFFSET_Y));
            healthBar = new ProgressBar(value: entity.Health, minValue: 0, maxValue: entity.MaxHealth,
                                            position: pos, dimensions: UIScaler.Scale(PROGRESSBAR_DIMENSIONS),
                                            frontColor: PaletteColors.Combat_HealthBar_Front,
                                            borderColor: PaletteColors.Combat_HealthBar_Border,
                                            backColor: PaletteColors.Combat_HealthBar_Back,
                                            borderThickness: UIScaler.Scale(PROGRESSBAR_BORDER_THICKNESS));
            group.Add(healthBar);

            pos += new Vector2(0, healthBar.Dimensions.Y + UIScaler.Scale(MANA_OFFSET_Y));
            manaBar = new ProgressBar(value: entity.Mana, minValue: 0, maxValue: entity.MaxMana,
                                          position: pos, dimensions: UIScaler.Scale(PROGRESSBAR_DIMENSIONS),
                                          frontColor: PaletteColors.Combat_ManaBar_Front,
                                          borderColor: PaletteColors.Combat_ManaBar_Border,
                                          backColor: PaletteColors.Combat_ManaBar_Back,
                                          borderThickness: UIScaler.Scale(PROGRESSBAR_BORDER_THICKNESS));
            group.Add(manaBar);

            return group;
        }

        #region Menu principal du joueur
        private MenuManager CreatePlayerMenu()
        {

            var font = _fontManager.Load(FontKeys.PlayerMenu_Items);

            var menuManager = new MenuManager();

            menuManager.AddItem(font, "COMBAT_ATTACK", PaletteColors.MenuItem, OnActionSelected, OnActionDeselected, AttackEnemy);
            menuManager.AddItem(font, "COMBAT_DEFENSE", PaletteColors.MenuItem, OnActionSelected, OnActionDeselected, Defend);
            menuManager.AddItem(font, "COMBAT_ITEM", PaletteColors.MenuItem, OnActionSelected, OnActionDeselected, DisplayItemsMenu);
            menuManager.AddItem(font, "COMBAT_FLEE", PaletteColors.MenuItem, OnActionSelected, OnActionDeselected, Flee);

            var panel = new Panel(position: default, dimensions: default,
                                  backgroundcolor: PaletteColors.Combat_Menu_Background,
                                  bordercolor: PaletteColors.Combat_Menu_Border,
                                  thickness: UIScaler.Scale(COMBAT_MENU_BORDER_THICKNESS));

            menuManager.SetItemsBackground(panel, UIScaler.Scale(COMBAT_MENU_BORDER_SPACING));

            return menuManager;
        }
        private MenuItem Flee(MenuItem item)
        {
            return item;
        }
        private MenuItem DisplayItemsMenu(MenuItem item)
        {
            _playerItemsMenu.Visible = true;
            return item;
        }
        private MenuItem Defend(MenuItem item)
        {
            return item;
        }
        private MenuItem AttackEnemy(MenuItem item)
        {
            _enemy.TakeDamage(_player.AttackAmount);
            return item;
        }
        private MenuItem OnActionSelected(MenuItem item)
        {
            item.Color = PaletteColors.MenuItem_Hovered;
            return item;
        }
        private MenuItem OnActionDeselected(MenuItem item)
        {
            item.Color = PaletteColors.MenuItem;
            return item;
        }
        #endregion

        #region Menu pour les items
        private MenuManager CreatePlayerItemsMenu()
        {
            var font = _fontManager.Load(FontKeys.PlayerMenu_Items);
            var menuManager = new MenuManager();

            var uniquePotions = _player.Inventory.Slots
                .Select(slot => slot.Item)
                .OfType<Potion>()
                .Distinct()
                .ToList();

            foreach (var potion in uniquePotions)
            {
                var menuItem = menuManager.AddItem(font, potion.Name, PaletteColors.MenuItem, OnPotionSelected, OnPotionDeselected, DrinkPotion);
                _potionsMenuItems[menuItem] = potion;
            }

            // On masque le menu tant que le joueur n'a pas sélectionner l'option "COMBAT_ITEM"
            menuManager.Visible = false;
            return menuManager;
        }
        private MenuItem OnPotionSelected(MenuItem item)
        {
            item.Color = PaletteColors.MenuItem_Hovered;
            return item;
        }
        private MenuItem OnPotionDeselected(MenuItem item)
        {
            item.Color = PaletteColors.MenuItem;
            return item;
        }
        private MenuItem DrinkPotion(MenuItem item)
        {
            Potion potion = _potionsMenuItems[item];
            _player.DrinkPotion(potion);

            _playerItemsMenu.Visible = false;
            _playerMenu.Visible = false;

            return item;
        }
        #endregion

        private void OnPlayerStatsChanged()
        {
            _playerHealthBar.Value = _player.Health;
            _playerManaBar.Value = _player.Mana;
        }
        private void OnEnemyStatsChanged()
        {
            _enemyHealthBar.Value = _enemy.Health;
            _enemyManaBar.Value = _enemy.Mana;
        }
        private void GoToWaitingScene(EventResult result)
        {
            _currentEvent.Result = result;
            SetCurrentScene(ProjectSceneKeys.WaitingScene);
        }
    }


}
