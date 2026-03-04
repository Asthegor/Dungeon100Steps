using DinaCSharp.Core.Utils;
using DinaCSharp.Events;
using DinaCSharp.Graphics;
using DinaCSharp.Inputs;
using DinaCSharp.Resources;
using DinaCSharp.Services;
using DinaCSharp.Services.Scenes;

using Dungeon100Steps.Core;
using Dungeon100Steps.Core.Datas.Characters;
using Dungeon100Steps.Core.Datas.Dungeons;
using Dungeon100Steps.Core.Datas.Items;
using Dungeon100Steps.Core.Keys;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;


namespace Dungeon100Steps.GameMechanics.Scenes
{
    // Note : Utilisez la propriété 'SceneManager' (héritée) pour accéder au moteur.
    // Ne capturez pas le paramètre 'sceneManager' dans les méthodes pour éviter l'erreur CS9107.
    public class CityScene(SceneManager sceneManager) : Scene(sceneManager)
    {
        private readonly ResourceManager _resourceManager = ServiceLocator.Get<ResourceManager>(ProjectServiceKeys.AssetsResourceManager);

        private readonly Vector2 INVENTORY_OFFSET = new Vector2(50, 30);
        private readonly Vector2 INVENTORY_DIMENSIONS = new Vector2(128, 128);

        private Player _player;
        private Panel _playerPanel;
        private Panel _inventoryPanel;

        private Panel _background;
        private Panel _backgroundNoSelection;

        private Panel _blacksmithPanel;
        private Polygon _blacksmithPolygon;
        private Panel _dungeonPanel;
        private Polygon _dungeonPolygon;
        private Panel _herboristPanel;
        private Polygon _herboristPolygon;
        private Panel _tavernPanel;
        private Polygon _tavernPolygon;
        public override void Load()
        {
            _player = ServiceLocator.Get<Player>(ProjectServiceKeys.Player)
                ?? throw new InvalidOperationException("Player service not found in ServiceLocator.");

            var playerPanelDimensions = new Vector2(_player.Texture.Width, _player.Texture.Height);
            var posPlayerPanel = new Vector2((ScreenDimensions.X - UIScaler.Scale(_player.Texture.Width)) / 2,
                                             ScreenDimensions.Y - UIScaler.Scale(_player.Texture.Height));
            _playerPanel = new Panel(position: posPlayerPanel, dimensions: UIScaler.Scale(playerPanelDimensions),
                                     image: _player.Texture);


            _background = new Panel(position: Vector2.Zero, dimensions: ScreenDimensions,
                                    image: _resourceManager.Load<Texture2D>(GameResourceKeys.City_Background));
            _backgroundNoSelection = new Panel(position: Vector2.Zero, dimensions: ScreenDimensions,
                                               image: _resourceManager.Load<Texture2D>(GameResourceKeys.City_Background_NoSelection));

            GenerateBlacksmithControls();
            GenerateDungeonControls();
            GenerateHerboristControls();
            GenerateTavernControls();

#if DEBUG
            _player.EquipBag(BagFactory.Bag8Slots);
            _player.Inventory.Add(ArmorFactory.Get(Rarity.Junk));
            _player.Inventory.Add(ArmorFactory.Get(Rarity.Junk));
            _player.Inventory.Add(ArmorFactory.Get(Rarity.Junk));
            _player.Inventory.Add(WeaponFactory.Get(Rarity.Junk));
            _player.Inventory.Add(WeaponFactory.Get(Rarity.Junk));
            _player.Inventory.Add(WeaponFactory.Get(Rarity.Junk));
#endif
        }
        public override void Reset()
        {
            GenerateInventoryPanel();

            _backgroundNoSelection.Visible = true;
            _background.Visible = false;
            _blacksmithPanel.Visible = false;
            _dungeonPanel.Visible = false;
            _herboristPanel.Visible = false;
            _tavernPanel.Visible = false;
        }
        public override void Update(GameTime gametime)
        {
            if (InputManager.IsReleasedByAny(PlayerInputKeys.Cancel))
            {
                SceneManager.AddResource("PreviousScene", ProjectSceneKeys.CityScene);
                SetCurrentScene(ProjectSceneKeys.PauseScene);
            }

            _blacksmithPolygon?.Update(gametime);
            _dungeonPolygon?.Update(gametime);
            _herboristPolygon?.Update(gametime);
            _tavernPolygon?.Update(gametime);

            _inventoryPanel.Update(gametime);
        }
        public override void Draw(SpriteBatch spritebatch)
        {
            _background?.Draw(spritebatch);
            _backgroundNoSelection?.Draw(spritebatch);

            _blacksmithPanel?.Draw(spritebatch);
            _dungeonPanel?.Draw(spritebatch);
            _herboristPanel?.Draw(spritebatch);
            _tavernPanel?.Draw(spritebatch);

            _playerPanel?.Draw(spritebatch);

            _inventoryPanel?.Draw(spritebatch);
        }

        public void Dispose()
        {
            _background?.Dispose();
            _backgroundNoSelection?.Dispose();
            _blacksmithPanel?.Dispose();
            _blacksmithPolygon?.Dispose();
            _dungeonPanel?.Dispose();
            _dungeonPolygon?.Dispose();
            _herboristPanel?.Dispose();
            _herboristPolygon?.Dispose();
            _tavernPanel?.Dispose();
            _tavernPolygon?.Dispose();
        }

        #region Blacksmith
        private void GenerateBlacksmithControls()
        {
            var blacksmithTexture = _resourceManager.Load<Texture2D>(GameResourceKeys.City_Blacksmith);
            _blacksmithPolygon = CreateBlacksmithPolygon(new Vector2(blacksmithTexture.Width, blacksmithTexture.Height),
                                                         OnBlacksmithClicked, OnBlacksmithHovered);
            _blacksmithPanel = new Panel(position: Vector2.Zero, dimensions: ScreenDimensions,
                                         image: blacksmithTexture);
        }
        private Polygon CreateBlacksmithPolygon(Vector2 originalResolution, EventHandler<PolygonEventArgs> onClicked, EventHandler<PolygonEventArgs> onHovered)
        {
            Vector2[] vertices =
            [
                new Vector2(2820, 172),
                new Vector2(3072, 172),
                new Vector2(3072, 1670),
                new Vector2(2058, 1625),
                new Vector2(2050, 1394),
                new Vector2(2265, 1383),
                new Vector2(2247, 958)
            ];
            Polygon polygon = new Polygon(vertices);
            polygon.ResizeForResolution(originalResolution, ScreenDimensions);
            polygon.OnHovered += onHovered;
            polygon.OnClicked += onClicked;
            return polygon;
        }
        private void OnBlacksmithHovered(object sender, PolygonEventArgs e)
        {
            _backgroundNoSelection.Visible = false;
            _background.Visible = true;
            _blacksmithPanel.Visible = true;
            _dungeonPanel.Visible = false;
            _herboristPanel.Visible = false;
            _tavernPanel.Visible = false;
        }
        private void OnBlacksmithClicked(object sender, PolygonEventArgs e)
        {
            SetCurrentScene(ProjectSceneKeys.BlacksmithScene);
        }
        #endregion

        #region Dungeon
        private void GenerateDungeonControls()
        {
            var dungeonTexture = _resourceManager.Load<Texture2D>(GameResourceKeys.City_Dungeon);
            _dungeonPolygon = CreateDungeonPolygon(new Vector2(dungeonTexture.Width, dungeonTexture.Height),
                                                   OnDungeonClicked, OnDungeonHovered);
            _dungeonPanel = new Panel(position: Vector2.Zero, dimensions: ScreenDimensions,
                                      image: dungeonTexture);
        }
        private Polygon CreateDungeonPolygon(Vector2 originalResolution, EventHandler<PolygonEventArgs> onClicked, EventHandler<PolygonEventArgs> onHovered)
        {
            Vector2[] vertices =
            [
                new Vector2(1188, 0),
                new Vector2(1290, 855),
                new Vector2(1296, 1408),
                new Vector2(712, 1412),
                new Vector2(730, 0)
            ];
            Polygon polygon = new Polygon(vertices);
            polygon.ResizeForResolution(originalResolution, ScreenDimensions);
            polygon.OnHovered += onHovered;
            polygon.OnClicked += onClicked;
            return polygon;
        }
        private void OnDungeonHovered(object sender, PolygonEventArgs e)
        {
            _backgroundNoSelection.Visible = false;
            _background.Visible = true;
            _blacksmithPanel.Visible = false;
            _dungeonPanel.Visible = true;
            _herboristPanel.Visible = false;
            _tavernPanel.Visible = false;
        }
        private void OnDungeonClicked(object sender, PolygonEventArgs e)
        {
#if DEBUG
            var dungeon = DungeonFactory.GenerateDebugDungeon();
#else
            var dungeon = DungeonFactory.Generate(10);
#endif
            ServiceLocator.Register(ProjectServiceKeys.CurrentDungeon, dungeon);
            SetCurrentScene(ProjectSceneKeys.WaitingScene);
        }
#endregion

        #region Herborist
        private void GenerateHerboristControls()
        {
            var herboristTexture = _resourceManager.Load<Texture2D>(GameResourceKeys.City_Herborist);
            _herboristPolygon = CreateHerboristPolygon(new Vector2(herboristTexture.Width, herboristTexture.Height),
                                                       OnHerboristClicked, OnHerboristHovered);
            _herboristPanel = new Panel(position: Vector2.Zero, dimensions: ScreenDimensions,
                                        image: herboristTexture);
        }
        private Polygon CreateHerboristPolygon(Vector2 originalResolution, EventHandler<PolygonEventArgs> onClicked, EventHandler<PolygonEventArgs> onHovered)
        {
            Vector2[] vertices =
            [
                new Vector2(0, 0),
                new Vector2(15, 0),
                new Vector2(488, 1000),
                new Vector2(300, 990),
                new Vector2(279, 1455),
                new Vector2(480, 1544),
                new Vector2(644, 2048),
                new Vector2(0, 2048)
            ];
            Polygon polygon = new Polygon(vertices);
            polygon.ResizeForResolution(originalResolution, ScreenDimensions);
            polygon.OnHovered += onHovered;
            polygon.OnClicked += onClicked;
            return polygon;
        }
        private void OnHerboristHovered(object sender, PolygonEventArgs e)
        {
            _backgroundNoSelection.Visible = false;
            _background.Visible = true;
            _blacksmithPanel.Visible = false;
            _dungeonPanel.Visible = false;
            _herboristPanel.Visible = true;
            _tavernPanel.Visible = false;
        }
        private void OnHerboristClicked(object sender, PolygonEventArgs e)
        {
            SetCurrentScene(ProjectSceneKeys.HerboristScene);
        }
        #endregion

        #region Tavern
        private void GenerateTavernControls()
        {
            var tavernTexture = _resourceManager.Load<Texture2D>(GameResourceKeys.City_Tavern);
            _tavernPolygon = CreateTavernPolygon(new Vector2(tavernTexture.Width, tavernTexture.Height),
                                                 OnTavernClicked, OnTavernHovered);
            _tavernPanel = new Panel(position: Vector2.Zero, dimensions: ScreenDimensions,
                                     image: tavernTexture);
        }
        public Polygon CreateTavernPolygon(Vector2 originalResolution, EventHandler<PolygonEventArgs> onClicked, EventHandler<PolygonEventArgs> onHovered)
        {
            Vector2[] vertices =
            [
                new Vector2(2353, 0),
                new Vector2(2506, 155),
                new Vector2(2550, 640),
                new Vector2(2234, 970),
                new Vector2(2240, 1390),
                new Vector2(1315, 1414),
                new Vector2(1321, 1046),
                new Vector2(1722, 667),
                new Vector2(2022, 627),
                new Vector2(2042, 169),
                new Vector2(2200, 0)
            ];
            Polygon polygon = new Polygon(vertices);
            polygon.ResizeForResolution(originalResolution, ScreenDimensions);
            polygon.OnHovered += onHovered;
            polygon.OnClicked += onClicked;
            return polygon;
        }
        private void OnTavernHovered(object sender, PolygonEventArgs e)
        {
            _backgroundNoSelection.Visible = false;
            _background.Visible = true;
            _blacksmithPanel.Visible = false;
            _dungeonPanel.Visible = false;
            _herboristPanel.Visible = false;
            _tavernPanel.Visible = true;
        }
        private void OnTavernClicked(object sender, PolygonEventArgs e)
        {
            SetCurrentScene(ProjectSceneKeys.TavernScene);
        }
        #endregion

        private void GenerateInventoryPanel()
        {
            var posInventoryPanel = ScreenDimensions - UIScaler.Scale(INVENTORY_DIMENSIONS + INVENTORY_OFFSET);
            _inventoryPanel = new Panel(posInventoryPanel, UIScaler.Scale(INVENTORY_DIMENSIONS), _player.Inventory.Texture);
            _inventoryPanel.OnHovered += (s, e) =>
            {
                _backgroundNoSelection.Visible = true;
                _background.Visible = false;
                _blacksmithPanel.Visible = false;
                _dungeonPanel.Visible = false;
                _herboristPanel.Visible = false;
                _tavernPanel.Visible = false;
            };
            _inventoryPanel.OnClicked += (s, e) => {
                SceneManager.AddResource("PreviousScene", ProjectSceneKeys.CityScene);
                SetCurrentScene(ProjectSceneKeys.InventoryScene);
            };
        }
    }
}
