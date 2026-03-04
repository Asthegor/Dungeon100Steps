using DinaCSharp.Core;
using DinaCSharp.Core.Utils;
using DinaCSharp.Enums;
using DinaCSharp.Graphics;
using DinaCSharp.Inputs;
using DinaCSharp.Resources;
using DinaCSharp.Services;
using DinaCSharp.Services.Fonts;
using DinaCSharp.Services.Localization;
using DinaCSharp.Services.Menus;
using DinaCSharp.Services.Scenes;

using Dungeon100Steps.Core;
using Dungeon100Steps.Core.Datas.Characters;
using Dungeon100Steps.Core.Datas.Dungeons;
using Dungeon100Steps.Core.Datas.Events;
using Dungeon100Steps.Core.Keys;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

namespace Dungeon100Steps.GameMechanics.Scenes.Events
{
    // Note : Utilisez la propriété 'SceneManager' (héritée) pour accéder au moteur.
    // Ne capturez pas le paramètre 'sceneManager' dans les méthodes pour éviter l'erreur CS9107.
    public class TrapScene(SceneManager sceneManager) : Scene(sceneManager)
    {
        private const float MESSAGE_PANEL_OFFSET_Y = 15f;
        private const int MESSAGE_PANEL_BORDER_THICKNESS = 5;

        private readonly FontManager _fontManager = ServiceLocator.Get<FontManager>(ServiceKeys.FontManager);
        private SpriteFont _messageFont;

        private DiceRoller _diceRoller;

        private Player _player;
        private Dungeon _currentDungeon;
        private TrapEvent _currentEvent;

        private MenuManager _menu;

        private Texture2D _backgroundTrapActivated;
        private Panel _background;
        private EventState _currentState;

        //private Group _messageGroup;
        private MessageGroup _messageGroup;
        //private Text _messageText;
        //private Panel _messagePanel;
        //private Text _continueText;

        private CheckResult _disarmResult;

        private bool _withDamage;
        private int _damageReceived;

        public override void Load()
        {
            _diceRoller = new DiceRoller(new Random());

            var menuItemFont = _fontManager.Load(FontKeys.Game_Texts);
            _messageFont = _fontManager.Load(FontKeys.Messages);

            var texture = GetRandomBackground();
            _background = new Panel(Vector2.Zero, ScreenDimensions, texture);


            _player = ServiceLocator.Get<Player>(ProjectServiceKeys.Player);

            _menu = new MenuManager();

            _menu.AddItem(menuItemFont, "TRAP_QUIT_DUNGEON", PaletteColors.MenuItem, OnMenuItemSelection, OnMenuItemDeselection, QuitDungeon);
            _menu.AddItem(menuItemFont, "TRAP_ATTEMPT_DISARM", PaletteColors.MenuItem, OnMenuItemSelection, OnMenuItemDeselection, OnAttemptDisarm);
            _menu.AddItem(menuItemFont, "TRAP_FORCE_THROUGH", PaletteColors.MenuItem, OnMenuItemSelection, OnMenuItemDeselection, OnForceThrough);

            //_messageGroup = CreateMessageGroup();
            var messageFont = _fontManager.Load(FontKeys.Messages);
            var continueFont = _fontManager.Load(FontKeys.Messages);
            //_messageGroup = EventUtils.CreateMessageGroup(messageFont, "TREASURE_DEFAULT", continueFont, "PRESS_TO_CONTINUE",
            //                                              ScreenDimensions, MESSAGE_PANEL_BORDER_THICKNESS,
            //                                              out _messageText, out _messagePanel, out _continueText);
            var thickness = UIScaler.Scale(MESSAGE_PANEL_BORDER_THICKNESS);
            _messageGroup = new MessageGroup(messageFont, "TRAP_DEFAULT", PaletteColors.Message,
                                             continueFont, "PRESS_TO_CONTINUE", PaletteColors.Message_Continue,
                                             ScreenDimensions, PaletteColors.Message_Panel_Background, PaletteColors.Message_Panel_Border,
                                             thickness);
            _messageGroup.Position = new Vector2(thickness, ScreenDimensions.Y - _messageGroup.Dimensions.Y - thickness * 2);

            _currentState = EventState.ShowingMessage;
        }
        public override void Reset()
        {
            _currentDungeon = ServiceLocator.Get<Dungeon>(ProjectServiceKeys.CurrentDungeon)
                ?? throw new InvalidOperationException("Aucun donjon trouvé.");
            _currentEvent = ServiceLocator.Get<TrapEvent>(ProjectServiceKeys.CurrentEvent)
                ?? throw new InvalidOperationException($"CurrentEvent n'existe pas ou n'est pas de type '{_currentEvent.GetType().Name}' dans le ServiceLocator.");

            _currentState = EventState.ShowingMessage;

            //_messageText.Content = "TRAP_DEFAULT";
            //UpdateMessageGroupElements();
            _messageGroup.SetMessage("TRAP_DEFAULT");

            _background.SetImage(GetRandomBackground());

            _withDamage = false;
            _damageReceived = 0;
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
                    _menu?.Update(gametime);
                    break;

                case EventState.ShowingResult:
                    if (InputManager.IsPressedByAny(PlayerInputKeys.Activate))
                    {
                        if (_withDamage)
                        {
                            _player.TakeDamage(_damageReceived);

                            //_messageText.Content = LocalizationManager.GetTranslation("TRAP_RECEIVE_DAMAGE", _damageReceived.ToString());
                            //// On met à jour la position et la dimension du MessagePanel en fonction du message
                            //UpdateMessageGroupElements();
                            _messageGroup.SetMessage(LocalizationManager.GetTranslation("TRAP_RECEIVE_DAMAGE", _damageReceived.ToString()));

                            _currentState = EventState.ShowingDamage;
                        }
                        else if (_currentEvent.Result == EventResult.Fled)
                        {
                            GoToWaitingScene(EventResult.Fled);
                        }
                        else
                        {
                            GoToWaitingScene(EventResult.Victory);
                        }
                    }
                    break;

                case EventState.ShowingDamage:
                    if (InputManager.IsPressedByAny(PlayerInputKeys.Activate))
                    {
                        if (_player.IsDead)
                            GoToWaitingScene(EventResult.Defeat);
                        else
                            GoToWaitingScene(EventResult.Victory);
                    }
                    break;
            }

        }
        public override void Draw(SpriteBatch spritebatch)
        {
            _background?.Draw(spritebatch);
            switch (_currentState)
            {
                case EventState.ShowingMessage:
                    _messageGroup?.Draw(spritebatch);
                    break;

                case EventState.Action:
                    _menu?.Draw(spritebatch);
                    break;

                case EventState.ShowingResult:
                    _messageGroup?.Draw(spritebatch);
                    break;

                case EventState.ShowingDamage:
                    _messageGroup?.Draw(spritebatch);
                    break;
            }
        }


        private void GoToWaitingScene(EventResult result)
        {
            _currentEvent.Result = result;
            SetCurrentScene(ProjectSceneKeys.WaitingScene);
        }

        private Texture2D GetRandomBackground()
        {
            var rnd = new Random();
            switch (rnd.Next(0, 2))
            {
                case 0:
                    _backgroundTrapActivated = RetreiveTextureFromKey(BackgroundKeys.TrapRoom1);
                    return RetreiveTextureFromKey(BackgroundKeys.TrapRoom1_Default);
                case 1:
                    _backgroundTrapActivated = RetreiveTextureFromKey(BackgroundKeys.TrapRoom2);
                    return RetreiveTextureFromKey(BackgroundKeys.TrapRoom2_Default);
                default:
                    throw new NotImplementedException();
            }
        }
        private static Texture2D RetreiveTextureFromKey(Key<ResourceTag> key)
        {
            var resourceManager = ServiceLocator.Get<ResourceManager>(ProjectServiceKeys.AssetsResourceManager);
            return resourceManager.Load<Texture2D>(key);
        }

        private MenuItem OnMenuItemSelection(MenuItem menuItem)
        {
            menuItem.Color = PaletteColors.MenuItem_Hovered;
            return menuItem;
        }
        private MenuItem OnMenuItemDeselection(MenuItem menuItem)
        {
            menuItem.Color = PaletteColors.MenuItem;
            return menuItem;
        }

        private MenuItem QuitDungeon(MenuItem menuItem)
        {
            _currentEvent.Result = EventResult.Fled;
            //_messageText.Content = "TRAP_RUN_AWAY";
            //UpdateMessageGroupElements();
            _messageGroup.SetMessage("TRAP_RUN_AWAY");
            _currentState = EventState.ShowingResult;
            return menuItem;
        }
        private MenuItem OnAttemptDisarm(MenuItem menuItem)
        {
            _disarmResult = _diceRoller.RollCheckDetailed(_player.Dexterity, _currentEvent.Difficulty);

            if (_disarmResult.Success)
            {
                // Succès : pas de dégâts
                //_messageText.Content = _currentEvent.TrapType switch
                //{
                //    TrapType.None => "TRAP_NONE_DISARM_SUCCESS",
                //    TrapType.Pit => "TRAP_PIT_DISARM_SUCCESS",
                //    TrapType.Darts => "TRAP_DART_DISARM_SUCCESS",
                //    TrapType.PoisonGas => "TRAP_POISON_DISARM_SUCCESS",
                //    TrapType.PressurePlate => "TRAP_PRESSUREPLATE_DISARM_SUCCESS",
                //    _ => "TRAP_NONE_DISARM_SUCCESS"
                //};
                //UpdateMessageGroupElements();
                _messageGroup.SetMessage(_currentEvent.TrapType switch
                {
                    TrapType.None => "TRAP_NONE_DISARM_SUCCESS",
                    TrapType.Pit => "TRAP_PIT_DISARM_SUCCESS",
                    TrapType.Darts => "TRAP_DART_DISARM_SUCCESS",
                    TrapType.PoisonGas => "TRAP_POISON_DISARM_SUCCESS",
                    TrapType.PressurePlate => "TRAP_PRESSUREPLATE_DISARM_SUCCESS",
                    _ => "TRAP_NONE_DISARM_SUCCESS"
                });

                _withDamage = false;
                _currentEvent.Result = EventResult.Victory;
            }
            else
            {
                // Échec : le piège se déclenche
                var zone = EventUtils.GetZoneFromLevel(_currentDungeon.CurrentLevel);
                int percentage = TrapScaling.GetPercentage(zone, _currentEvent.TrapType);
                var damage = _player.MaxHealth * (percentage / 100f);
                if (_disarmResult.Critical)
                    damage *= 1.5f;

                _damageReceived = (int)damage;
                if (_damageReceived <= 0)
                {
                    _damageReceived = 0;
                    _withDamage = false;
                }
                else
                {
                    _withDamage = true;
                }


                //_messageText.Content = _currentEvent.TrapType switch
                //{
                //    TrapType.None => "TRAP_NONE_DISARM_FAILED",
                //    TrapType.Pit => "TRAP_PIT_DISARM_FAILED",
                //    TrapType.Darts => "TRAP_DART_DISARM_FAILED",
                //    TrapType.PoisonGas => "TRAP_POISON_DISARM_FAILED",
                //    TrapType.PressurePlate => "TRAP_PRESSUREPLATE_DISARM_FAILED",
                //    _ => "TRAP_NONE_DISARM_FAILED"
                //};
                //UpdateMessageGroupElements();
                _messageGroup.SetMessage(_currentEvent.TrapType switch
                {
                    TrapType.None => "TRAP_NONE_DISARM_FAILED",
                    TrapType.Pit => "TRAP_PIT_DISARM_FAILED",
                    TrapType.Darts => "TRAP_DART_DISARM_FAILED",
                    TrapType.PoisonGas => "TRAP_POISON_DISARM_FAILED",
                    TrapType.PressurePlate => "TRAP_PRESSUREPLATE_DISARM_FAILED",
                    _ => "TRAP_NONE_DISARM_FAILED"
                });

                _background.SetImage(_backgroundTrapActivated);
            }

            _currentState = EventState.ShowingResult;
            return menuItem;
        }
        private MenuItem OnForceThrough(MenuItem menuItem)
        {
            var result = _diceRoller.RollCheckDetailed(_player.Dexterity, _currentEvent.Difficulty);

            var zone = EventUtils.GetZoneFromLevel(_currentDungeon.CurrentLevel);
            int percentage = TrapScaling.GetPercentage(zone, _currentEvent.TrapType);
            var damage = _player.MaxHealth * (percentage / 100f);

            if (result.Critical)
                damage = result.Success ? damage / 2f : damage * 2f;

            _damageReceived = (int)damage;

            if (_damageReceived <= 0)
            {
                _damageReceived = 0;
                _withDamage = false;
            }
            else
            {
                _withDamage = true;
                _background.SetImage(_backgroundTrapActivated);
            }

            //_messageText.Content = _currentEvent.TrapType switch
            //{
            //    TrapType.None => "TRAP_NONE_ACTIVATED",
            //    TrapType.Pit => "TRAP_PIT_ACTIVATED",
            //    TrapType.Darts => "TRAP_DART_ACTIVATED",
            //    TrapType.PoisonGas => "TRAP_POISON_ACTIVATED",
            //    TrapType.PressurePlate => "TRAP_PRESSUREPLATE_ACTIVATED",
            //    _ => "TRAP_NONE_ACTIVATED"
            //};
            //UpdateMessageGroupElements();
            _messageGroup.SetMessage(_currentEvent.TrapType switch
            {
                TrapType.None => "TRAP_NONE_ACTIVATED",
                TrapType.Pit => "TRAP_PIT_ACTIVATED",
                TrapType.Darts => "TRAP_DART_ACTIVATED",
                TrapType.PoisonGas => "TRAP_POISON_ACTIVATED",
                TrapType.PressurePlate => "TRAP_PRESSUREPLATE_ACTIVATED",
                _ => "TRAP_NONE_ACTIVATED"
            });

            _currentState = EventState.ShowingResult;
            return menuItem;
        }


        //private Group CreateMessageGroup()
        //{
        //    var group = new Group();

        //    _messageText = new Text(_messageFont, "TRAP_DEFAULT", PaletteColors.Message,
        //                        position: new Vector2(0, _messageFont.LineSpacing),
        //                        horizontalalignment: HorizontalAlignment.Center, verticalalignment: VerticalAlignment.Center);
        //    _messageText.Wrap = true;
        //    _messageText.Dimensions = new Vector2(ScreenDimensions.X, _messageText.TextDimensions.Y);

        //    var panelPos = new Vector2(UIScaler.Scale(MESSAGE_PANEL_BORDER_THICKNESS), 0);
        //    var panelDim = new Vector2(ScreenDimensions.X - UIScaler.Scale(MESSAGE_PANEL_BORDER_THICKNESS) * 2, _messageFont.LineSpacing * 2 + _messageText.Dimensions.Y);
        //    _messagePanel = new Panel(panelPos, panelDim, PaletteColors.Message_Panel_Background)
        //    {
        //        BorderColor = PaletteColors.Message_Panel_Border,
        //        Thickness = UIScaler.Scale(MESSAGE_PANEL_BORDER_THICKNESS)
        //    };

        //    _continueText = new Text(_messageFont, "PRESS_TO_CONTINUE", PaletteColors.Message, zorder: 20);
        //    _continueText.Position = _messagePanel.Dimensions - _continueText.TextDimensions - new Vector2(UIScaler.Scale(MESSAGE_PANEL_BORDER_THICKNESS) * 2);

        //    group.Add(_messagePanel);
        //    group.Add(_messageText);
        //    group.Add(_continueText);
        //    return group;
        //}

        //private void UpdateMessageGroupElements()
        //{
        //    var panelDim = new Vector2(_messagePanel.Dimensions.X, _messageFont.LineSpacing * 2 + _messageText.Dimensions.Y);
        //    _messagePanel.Dimensions = panelDim;
        //    _messagePanel.Position = new Vector2(_messagePanel.Position.X, ScreenDimensions.Y - _messagePanel.Dimensions.Y - UIScaler.Scale(MESSAGE_PANEL_OFFSET_Y));
        //    _messageText.Position = _messagePanel.Position + new Vector2(0, _messageFont.LineSpacing);
        //    _continueText.Position = _messagePanel.Position + _messagePanel.Dimensions - _continueText.TextDimensions - new Vector2(UIScaler.Scale(MESSAGE_PANEL_BORDER_THICKNESS));
        //}
    }
}
