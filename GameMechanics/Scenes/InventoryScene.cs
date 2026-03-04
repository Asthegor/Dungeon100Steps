using DinaCSharp.Core;
using DinaCSharp.Core.Utils;
using DinaCSharp.Events;
using DinaCSharp.Graphics;
using DinaCSharp.Resources;
using DinaCSharp.Services;
using DinaCSharp.Services.Fonts;
using DinaCSharp.Services.Menus;
using DinaCSharp.Services.Scenes;

using Dungeon100Steps.Core;
using Dungeon100Steps.Core.Datas.Characters;
using Dungeon100Steps.Core.Datas.Items;
using Dungeon100Steps.Core.Keys;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;

namespace Dungeon100Steps.GameMechanics.Scenes
{
    public class InventoryScene(SceneManager sceneManager) : Scene(sceneManager)
    {
        private const float PLAYER_LABEL_OFFSET_Y = 15f;
        private const float PLAYER_LABEL_OFFSET_X = 15f;
        private const float PLAYER_GROUP_OFFSET_X = 25f;
        private readonly Vector2 PLAYER_PANEL_DIMENSIONS = new Vector2(192, 384);

        private const float EQUIPMENT_OFFSET_X = 10f;
        private readonly Vector2 EQUIPMENT_DIMENSIONS = new Vector2(96, 96);
        private readonly Vector2 EQUIPMENT_MARGIN = new Vector2(25, 25);
        private const int EQUIPMENT_BORDER_THICKNESS = 4;

        private readonly Vector2 BUTTON_NEXT_DIMENSIONS = new Vector2(136, 80);

        private readonly Vector2 ITEMMENU_BACKGROUND_OFFSET = new Vector2(10, 10);

        private readonly FontManager _fontManager = ServiceLocator.Get<FontManager>(ServiceKeys.FontManager);
        private readonly ResourceManager _resourceManager = ServiceLocator.Get<ResourceManager>(ProjectServiceKeys.AssetsResourceManager);

        private Player _player;
        private Group _playerGroup;

        private Text _attackText;
        private Text _defenseText;
        private Text _healthText;
        private Text _manaText;
        private Text _goldText;

        private Group _equipmentGroup;
        private Group _inventoryGroup;

        private Button _backButton;

        private MenuManager _weaponAndArmorMenu;
        private MenuManager _potionMenu;
        private bool _isItemMenuDisplayed;

        private enum ItemType { Weapon, Armor, Potion }
        private readonly Dictionary<ItemType, MenuItem> _itemMenus = [];
        private readonly Dictionary<Panel, Item> _itemPanels = [];

        private Item _selectedItem;

        private Key<SceneTag>? _previousScene;
        public override void Load()
        {

            _player = ServiceLocator.Get<Player>(ProjectServiceKeys.Player);
            _player.OnWeaponChanged += OnWeaponChanged;
            _player.OnArmorChanged += OnArmorChanged;
            _player.OnStatsChanged += OnStatsChanged;

            _playerGroup = CreatePlayerGroup();
            _playerGroup.Position = new Vector2(ScreenDimensions.X * 1 / 6, ScreenDimensions.Y * 1 / 4);

            CreateEquipmentGroup();

            _backButton = new Button(position: new Vector2(ScreenDimensions.X * 4 / 5, ScreenDimensions.Y * 7 / 8),
                                     dimensions: UIScaler.Scale(BUTTON_NEXT_DIMENSIONS),
                                     font: _fontManager.Load(FontKeys.BackButton_Text),
                                     content: "UI_BACK",
                                     textColor: PaletteColors.BackButton_Text,
                                     backgroundImage: _resourceManager.Load<Texture2D>(GameResourceKeys.Button_Next),
                                     onClick: ReturnToPreviousScene, onHover: OnButtonNextHovered);

            CreateWeaponAndArmorMenu();
            CreatePotionMenu();



        }

        public override void Reset()
        {
            _previousScene = SceneManager.GetResource<Key<SceneTag>>("PreviousScene");
            if (!_previousScene.HasValue)
                throw new ArgumentNullException("PreviousScene must not be null.");

            SceneManager.RemoveResource("PreviousScene");

            CreateInventoryGroup();
        }
        public override void Update(GameTime gametime)
        {
            if (_isItemMenuDisplayed)
            {
                switch (_selectedItem)
                {
                    case Weapon:
                    case Armor:
                        _weaponAndArmorMenu?.Update(gametime);
                        break;
                    case Potion:
                        _potionMenu?.Update(gametime);
                        break;
                }
                return;
            }

            _backButton?.Update(gametime);
            _inventoryGroup?.Update(gametime);

        }
        public override void Draw(SpriteBatch spritebatch)
        {
            _playerGroup?.Draw(spritebatch);
            _equipmentGroup?.Draw(spritebatch);
            _inventoryGroup?.Draw(spritebatch);
            _backButton?.Draw(spritebatch);

            if (_isItemMenuDisplayed)
            {
                _weaponAndArmorMenu?.Draw(spritebatch);
                _potionMenu?.Draw(spritebatch);
            }
        }


        private void OnWeaponChanged(Weapon weapon)
        {
            CreateEquipmentGroup();
        }
        private void OnArmorChanged(Armor armor)
        {
            CreateEquipmentGroup();
        }
        private void OnStatsChanged()
        {
            _healthText.Content = $"{_player.Health} / {_player.MaxHealth}";
            _attackText.Content = $"{_player.AttackAmount}";
            _defenseText.Content = $"{_player.Defense}";
            _manaText.Content = $"{_player.Mana} / {_player.MaxMana}";
            _goldText.Content = $"{_player.Gold}";
        }
        private Group CreatePlayerGroup()
        {
            Group group = new();

            Panel playerPanel = new Panel(position: default, dimensions: UIScaler.Scale(PLAYER_PANEL_DIMENSIONS), image: _player.Texture);
            group.Add(playerPanel);

            var pos = new Vector2(0, playerPanel.Dimensions.Y + UIScaler.Scale(PLAYER_LABEL_OFFSET_Y));

            var classGroup = CreatePlayerStatGroup("PLAYER_CLASS_LABEL", _player.Name, UIScaler.Scale(PLAYER_LABEL_OFFSET_X), out _);
            var levelText = new Text(_fontManager.Load(FontKeys.Player_Value), $" ({_player.Level})", PaletteColors.Player_Value,
                                     new Vector2(classGroup.Dimensions.X, 0));
            classGroup.Add(levelText);
            classGroup.Position = pos;
            group.Add(classGroup);

            // Health
            pos += new Vector2(0, classGroup.Dimensions.Y + UIScaler.Scale(PLAYER_LABEL_OFFSET_Y));
            var strHealth = $"{_player.Health} / {_player.MaxHealth}";
            var healthGroup = CreatePlayerStatGroup("PLAYER_HEALTH_LABEL", strHealth, UIScaler.Scale(PLAYER_LABEL_OFFSET_X), out _healthText);
            healthGroup.Position = pos;
            group.Add(healthGroup);

            // Mana
            pos += new Vector2(0, healthGroup.Dimensions.Y + UIScaler.Scale(PLAYER_LABEL_OFFSET_Y));
            var strMana = $"{_player.Mana} / {_player.MaxMana}";
            var manaGroup = CreatePlayerStatGroup("PLAYER_MANA_LABEL", strMana, UIScaler.Scale(PLAYER_LABEL_OFFSET_X), out _manaText);
            manaGroup.Position = pos;
            group.Add(manaGroup);

            // AttackAmount
            pos += new Vector2(0, manaGroup.Dimensions.Y + UIScaler.Scale(PLAYER_LABEL_OFFSET_Y));
            var attackGroup = CreatePlayerStatGroup("PLAYER_ATTACK_LABEL", _player.AttackAmount.ToString(), UIScaler.Scale(PLAYER_LABEL_OFFSET_X), out _attackText);
            attackGroup.Position = pos;
            group.Add(attackGroup);

            // Defense
            pos += new Vector2(0, attackGroup.Dimensions.Y + UIScaler.Scale(PLAYER_LABEL_OFFSET_Y));
            var defenseGroup = CreatePlayerStatGroup("PLAYER_DEFENSE_LABEL", _player.Defense.ToString(), UIScaler.Scale(PLAYER_LABEL_OFFSET_X), out _defenseText);
            defenseGroup.Position = pos;
            group.Add(defenseGroup);

            // Gold
            pos += new Vector2(0, defenseGroup.Dimensions.Y + UIScaler.Scale(PLAYER_LABEL_OFFSET_Y));
            var goldGroup = CreatePlayerStatGroup("PLAYER_GOLD_LABEL", _player.Gold.ToString(), UIScaler.Scale(PLAYER_LABEL_OFFSET_X), out _goldText);
            goldGroup.Position = pos;
            group.Add(goldGroup);

            return group;
        }
        private Group CreatePlayerStatGroup(string strLabel, string strText, float offsetX, out Text valueText)
        {
            var labelFont = _fontManager.Load(FontKeys.Player_Label);
            var valueFont = _fontManager.Load(FontKeys.Player_Value);

            return CreateLabelAndText(strLabel, PaletteColors.Player_Label, labelFont, strText, PaletteColors.Player_Value, valueFont, offsetX, out valueText);
        }
        private void CreateEquipmentGroup()
        {
            _equipmentGroup?.Dispose();
            _equipmentGroup = CreateWeaponAndArmorGroup();
            _equipmentGroup.Position = _playerGroup.Position + new Vector2(_playerGroup.Dimensions.X + UIScaler.Scale(PLAYER_GROUP_OFFSET_X),
                                                                           UIScaler.Scale(PLAYER_PANEL_DIMENSIONS.Y) - _equipmentGroup.Dimensions.Y);
        }
        private Group CreateWeaponAndArmorGroup()
        {
            Group group = new();

            var weapon = _player.Weapon;
            if (_player.Weapon == null)
            {
                weapon = new Weapon(name: nameof(WeaponKeys.DefaultWeapon).ToUpperInvariant(),
                                    texture: _resourceManager.Load<Texture2D>(WeaponKeys.DefaultWeapon),
                                    bonuses: []);
            }
            var weaponGroup = CreateEquipmentGroup(weapon);
            group.Add(weaponGroup);

            var armor = _player.Armor;
            if (_player.Armor == null)
            {
                armor = new Armor(name: nameof(ArmorKeys.DefaultArmor).ToUpperInvariant(),
                                  texture: _resourceManager.Load<Texture2D>(ArmorKeys.DefaultArmor),
                                  bonuses: []);
            }
            var armorGroup = CreateEquipmentGroup(armor);
            armorGroup.Position = new Vector2(0, weaponGroup.Dimensions.Y + UIScaler.Scale(EQUIPMENT_OFFSET_X));
            group.Add(armorGroup);

            return group;
        }
        private Group CreateEquipmentGroup(Item equipment)
        {
            Group group = new();

            var panel = new Panel(default, UIScaler.Scale(EQUIPMENT_DIMENSIONS), equipment.Texture, 3, true, 5)
            { BorderColor = PaletteColors.Equipment_Border };
            group.Add(panel);

            var bonusFont = _fontManager.Load(FontKeys.Equipment_Bonus_Text);
            var equipmentNameFont = _fontManager.Load(FontKeys.Equipment_Name);

            Color equipmentColor = equipment.Rarity switch
            {
                Rarity.Common => PaletteColors.Equipment_Name_Common,
                Rarity.Uncommon => PaletteColors.Equipment_Name_Uncommon,
                Rarity.Rare => PaletteColors.Equipment_Name_Rare,
                Rarity.Elite => PaletteColors.Equipment_Name_Elite,
                _ => PaletteColors.Equipment_Name_Junk
            };
            var nameText = new Text(equipmentNameFont, equipment.Name, equipmentColor,
                                    new Vector2(panel.Dimensions.X + UIScaler.Scale(EQUIPMENT_OFFSET_X), 0));
            group.Add(nameText);

            // TODO: Ajouter les bonus
            var pos = nameText.Position;
            foreach (var bonus in equipment.Bonuses)
            {
                pos += new Vector2(0, bonusFont.LineSpacing);
                var bonusText = new Text(bonusFont, bonus.GetDescription(_player.AttackAmount), PaletteColors.Equipment_Bonus_Text, pos);
                group.Add(bonusText);
            }
            return group;
        }
        private static Group CreateLabelAndText(string strLabel, Color labelColor, SpriteFont labelFont,
                                                string strText, Color textColor, SpriteFont textFont,
                                                float offsetX, out Text valueText)
        {
            Group group = new();

            var labelText = new Text(labelFont, strLabel, labelColor);
            group.Add(labelText);

            valueText = new Text(textFont, strText, textColor);
            valueText.Position = new Vector2(labelText.Dimensions.X + offsetX, (labelText.Dimensions.Y - valueText.Dimensions.Y) / 2);
            group.Add(valueText);

            return group;
        }
        private void ReturnToPreviousScene(Button button)
        {
            SetCurrentScene((Key<SceneTag>)_previousScene);
        }
        private void OnButtonNextHovered(Button button)
        {
            button.TextColor = PaletteColors.BackButton_Text_Hovered;
        }

        private void CreateInventoryGroup()
        {
            _inventoryGroup?.Dispose();
            _inventoryGroup = new Group();

            for (int index = 0; index < _player.Inventory.Capacity; index++)
            {
                //var temp = _player.Inventory.Slots.GetRange(index, 1);
                var slot = _player.Inventory.Slots != null && _player.Inventory.Slots.Count > index ? _player.Inventory.Slots[index] : null;
                var slotGroup = CreateSlotGroup(slot);
                slotGroup.Position = new Vector2((index % 5) * UIScaler.Scale(EQUIPMENT_DIMENSIONS.X + EQUIPMENT_MARGIN.X),
                                                 (index / 5) * UIScaler.Scale(EQUIPMENT_DIMENSIONS.Y + EQUIPMENT_MARGIN.Y));
                _inventoryGroup.Add(slotGroup);
            }
            _inventoryGroup.Position = _equipmentGroup.Position + new Vector2(0, _equipmentGroup.Dimensions.Y + UIScaler.Scale(EQUIPMENT_MARGIN.Y));
        }
        private Group CreateSlotGroup(Slot slot)
        {
            var group = new Group();
            bool isItemPresent = slot != null && slot.Item != null;
            if (isItemPresent)
            {
                var itemPanel = new Panel(position: default, dimensions: UIScaler.Scale(EQUIPMENT_DIMENSIONS),
                                          image: slot.Item.Texture);
                itemPanel.OnRightClicked += DisplayItemMenu;
                itemPanel.OnClicked += DisplayItemInfos;
                _itemPanels[itemPanel] = slot.Item;

                group.Add(itemPanel);

                var selectionPanel = new Panel(position: default, dimensions: UIScaler.Scale(EQUIPMENT_DIMENSIONS),
                                        backgroundcolor: PaletteColors.Inventory_Item_Selected_Background,
                                        bordercolor: PaletteColors.Inventory_Item_Selected_Border,
                                        thickness: 0)
                {
                    Visible = false
                };

                group.Add(selectionPanel);
            }
            var borderPanel = new Panel(position: default, dimensions: UIScaler.Scale(EQUIPMENT_DIMENSIONS),
                                        backgroundcolor: isItemPresent ? PaletteColors.Inventory_Item_Background : PaletteColors.Inventory_NoItem_Background,
                                        bordercolor: isItemPresent ? PaletteColors.Inventory_Item_Border : PaletteColors.Inventory_NoItem_Border,
                                        thickness: UIScaler.Scale(EQUIPMENT_BORDER_THICKNESS));
            group.Add(borderPanel);

            return group;
        }

        private void DisplayItemInfos(object sender, PanelEventArgs e)
        {
            var panel = e.Panel;
            _selectedItem = _itemPanels[panel];


        }

        private void DisplayItemMenu(object sender, PanelEventArgs e)
        {
            var panel = e.Panel;
            _selectedItem = _itemPanels[panel];

            _weaponAndArmorMenu.Visible = false;
            _potionMenu.Visible = false;

            switch (_selectedItem)
            {
                case Weapon:
                case Armor:
                    _weaponAndArmorMenu.Visible = true;
                    _weaponAndArmorMenu.Reset();
                    _weaponAndArmorMenu.ItemsPosition = panel.Position + new Vector2(panel.Dimensions.X, 0);
                    break;
                case Potion:
                    _potionMenu.Visible = true;
                    _potionMenu.Reset();
                    _potionMenu.ItemsPosition = panel.Position + new Vector2(panel.Dimensions.X, 0);
                    break;
            }
            _isItemMenuDisplayed = true;
        }

        private void CancelItemMenu()
        {
            _isItemMenuDisplayed = false;
            _weaponAndArmorMenu.Visible = false;
            _potionMenu.Visible = false;
            _selectedItem = null;
        }
        private void CreateWeaponAndArmorMenu()
        {
            _weaponAndArmorMenu = new MenuManager(cancellation: CancelItemMenu);

            var font = _fontManager.Load(FontKeys.Inventory_Item_Menu);
            var weaponMenuItem = _weaponAndArmorMenu.AddItem(font, "INVENTORY_EQUIP", PaletteColors.MenuItem,
                                                             selection: OnMenuItemSelection, deselection: OnMenuItemDeselection,
                                                             activation: EquipItem);
            _itemMenus[ItemType.Weapon] = weaponMenuItem;
            _itemMenus[ItemType.Armor] = weaponMenuItem;

            _weaponAndArmorMenu.AddItem(font, "INVENTORY_DROP", PaletteColors.MenuItem,
                                        selection: OnMenuItemSelection, deselection: OnMenuItemDeselection,
                                        activation: DropItem);

            var panel = new Panel(position: UIScaler.Scale(ITEMMENU_BACKGROUND_OFFSET) * -1,
                                  dimensions: _weaponAndArmorMenu.ItemsDimensions + UIScaler.Scale(ITEMMENU_BACKGROUND_OFFSET) * 2,
                                  backgroundcolor: PaletteColors.Inventory_ItemMenu_Background,
                                  bordercolor: PaletteColors.Inventory_ItemMenu_Border, thickness: 5,
                                  withroundcorner: true, radius: 15);
            _weaponAndArmorMenu.SetItemsBackground(panel, UIScaler.Scale(ITEMMENU_BACKGROUND_OFFSET));
        }
        private void CreatePotionMenu()
        {
            _potionMenu = new MenuManager(cancellation: CancelItemMenu);
            var font = _fontManager.Load(FontKeys.Inventory_Item_Menu);

            var potionMenuItem = _potionMenu.AddItem(font, "INVENTORY_DRINK", PaletteColors.MenuItem,
                                                     selection: OnMenuItemSelection, deselection: OnMenuItemDeselection,
                                                     activation: DrinkPotion);
            _itemMenus[ItemType.Potion] = potionMenuItem;

            _potionMenu.AddItem(font, "INVENTORY_DROP", PaletteColors.MenuItem,
                                selection: OnMenuItemSelection, deselection: OnMenuItemDeselection,
                                activation: DropItem);

            var panel = new Panel(position: UIScaler.Scale(ITEMMENU_BACKGROUND_OFFSET) * -1,
                                  dimensions: _weaponAndArmorMenu.ItemsDimensions + UIScaler.Scale(ITEMMENU_BACKGROUND_OFFSET) * 2,
                                  backgroundcolor: PaletteColors.Inventory_ItemMenu_Background,
                                  bordercolor: PaletteColors.Inventory_ItemMenu_Border, thickness: 5,
                                  withroundcorner: true, radius: 15);
            _potionMenu.SetItemsBackground(panel, UIScaler.Scale(ITEMMENU_BACKGROUND_OFFSET));
        }

        private MenuItem EquipItem(MenuItem menuItem)
        {
            switch (_selectedItem)
            {
                case Weapon weapon:
                    _player.EquipWeapon(weapon);
                    break;
                case Armor armor:
                    _player.EquipArmor(armor);
                    break;
            }
            UpdateInventory();
            return menuItem;
        }
        private MenuItem DrinkPotion(MenuItem menuItem)
        {
            Potion potion = _selectedItem as Potion;
            _player.DrinkPotion(potion);
            UpdateInventory();
            return menuItem;
        }
        private MenuItem DropItem(MenuItem menuItem)
        {
            Item item = _selectedItem;
            _player.Inventory.Remove(item);
            UpdateInventory();
            return menuItem;
        }
        private void UpdateInventory()
        {
            _selectedItem = null;

            CreateInventoryGroup();
            CancelItemMenu();
        }
        private MenuItem OnMenuItemDeselection(MenuItem menuItem)
        {
            menuItem.Color = PaletteColors.MenuItem;
            return menuItem;
        }

        private MenuItem OnMenuItemSelection(MenuItem menuItem)
        {
            menuItem.Color = PaletteColors.MenuItem_Hovered;
            return menuItem;
        }
    }
}
