using DinaCSharp.Core;
using DinaCSharp.Core.Utils;
using DinaCSharp.Enums;
using DinaCSharp.Graphics;
using DinaCSharp.Resources;
using DinaCSharp.Services;
using DinaCSharp.Services.Fonts;
using DinaCSharp.Services.Scenes;

using Dungeon100Steps.Core;
using Dungeon100Steps.Core.Datas.Characters;
using Dungeon100Steps.Core.Keys;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;

namespace Dungeon100Steps.GameMechanics.Scenes
{
    public class SelectPlayerScene(SceneManager sceneManager) : Scene(sceneManager)
    {
        private const float BUTTONS_OFFSET = 20f;
        private readonly Vector2 BUTTON_MARGIN = new Vector2(3, 3);
        private const float HERO_LABEL_OFFSET_Y = 15f;
        private const float TITLE_OFFSET_Y = 30f;

        private readonly Vector2 BUTTONS_DIMENSIONS = new Vector2(150, 50);

        private readonly FontManager _fontManager = ServiceLocator.Get<FontManager>(ServiceKeys.FontManager);
        private readonly ResourceManager _resourceManager = ServiceLocator.Get<ResourceManager>(ProjectServiceKeys.AssetsResourceManager);

        private readonly Random _random = new Random();

        private Text _title;
        private Group _genreGroup;
        private Dictionary<(Genre, HeroClass), Key<ResourceTag>> _resourceKeys;
        private Dictionary<(Genre, HeroClass), Group> _heroGroups;
        private Dictionary<Genre, Button> _genreButtons;
        private Dictionary<(Genre, HeroClass), Button> _heroButtons;

        private Genre _selectedGenre;
        private HeroClass _selectedHeroClass;

        private Group _buttonsGroup;
        private Button _back;
        private Button _continue;

        public override void Load()
        {
            // Créer le titre
            _title = CreateTitle();

            // Créer le dictionnaire des clés de ressources
            _resourceKeys = new Dictionary<(Genre, HeroClass), Key<ResourceTag>>
            {
                { (Genre.Female, HeroClass.Warrior), GameResourceKeys.Warrior_Female },
                { (Genre.Male, HeroClass.Warrior), GameResourceKeys.Warrior_Male },
                { (Genre.Female, HeroClass.Mage), GameResourceKeys.Mage_Female },
                { (Genre.Male, HeroClass.Mage), GameResourceKeys.Mage_Male },
                { (Genre.Female, HeroClass.Thief), GameResourceKeys.Thief_Female },
                { (Genre.Male, HeroClass.Thief), GameResourceKeys.Thief_Male },
            };

            // Créer les groupes de genre
            _genreGroup = CreateGenreGroup();
            UpdateGenreGroupPosition();

            // Créer tous les groupes de héros
            _heroGroups = new Dictionary<(Genre, HeroClass), Group>
            {
                { (Genre.Female, HeroClass.Warrior), CreateHeroGroup(GameResourceKeys.Warrior_Female, Genre.Female, HeroClass.Warrior) },
                { (Genre.Female, HeroClass.Mage), CreateHeroGroup(GameResourceKeys.Mage_Female, Genre.Female, HeroClass.Mage) },
                { (Genre.Female, HeroClass.Thief), CreateHeroGroup(GameResourceKeys.Thief_Female, Genre.Female, HeroClass.Thief) },
                { (Genre.Male, HeroClass.Warrior), CreateHeroGroup(GameResourceKeys.Warrior_Male, Genre.Male, HeroClass.Warrior) },
                { (Genre.Male, HeroClass.Mage), CreateHeroGroup(GameResourceKeys.Mage_Male, Genre.Male, HeroClass.Mage) },
                { (Genre.Male, HeroClass.Thief), CreateHeroGroup(GameResourceKeys.Thief_Male, Genre.Male, HeroClass.Thief) }
            };

            UpdateHeroGroupsPositions();

            // Créer les boutons Back et Continue
            _buttonsGroup = CreateButtonsGroup();
            _buttonsGroup.Position = new Vector2((ScreenDimensions.X - _buttonsGroup.Dimensions.X) / 2, ScreenDimensions.Y - _buttonsGroup.Dimensions.Y * 2);
        }

        public override void Reset()
        {
            // Sélection aléatoire du genre et de la classe
            _selectedGenre = (Genre)_random.Next(0, 2);
            _selectedHeroClass = (HeroClass)_random.Next(0, 3);

            // Appliquer la sélection visuelle
            UpdateGenreSelection();
            UpdateHeroSelection();
        }

        public override void Update(GameTime gametime)
        {
            _genreGroup?.Update(gametime);

            // Mettre à jour uniquement les groupes du genre sélectionné
            foreach (var heroClass in Enum.GetValues<HeroClass>())
            {
                _heroGroups[(_selectedGenre, heroClass)]?.Update(gametime);
            }

            _buttonsGroup?.Update(gametime);
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            _title?.Draw(spritebatch);
            _genreGroup?.Draw(spritebatch);

            // Dessiner uniquement les groupes du genre sélectionné
            foreach (var heroClass in Enum.GetValues<HeroClass>())
            {
                _heroGroups[(_selectedGenre, heroClass)]?.Draw(spritebatch);
            }

            _buttonsGroup?.Draw(spritebatch);
        }

        private Text CreateTitle()
        {
            var font = _fontManager.Load(FontKeys.SelectPlayer_Label);
            var title = new Text(font, "SELECTIONPLAYER_TITLE", PaletteColors.SelectionPlayer_Label,
                               horizontalalignment: HorizontalAlignment.Center)
            {
                Dimensions = new Vector2(ScreenDimensions.X, font.MeasureString("SELECTIONPLAYER_TITLE").Y),
                Position = new Vector2(0, UIScaler.Scale(TITLE_OFFSET_Y))
            };
            return title;
        }

        private Group CreateGenreGroup()
        {
            Group group = [];
            _genreButtons = new Dictionary<Genre, Button>();

            var font = _fontManager.Load(FontKeys.SelectPlayer_Label);
            var buttonDimensions = UIScaler.Scale(BUTTONS_DIMENSIONS);

            var maleButton = new Button(Vector2.Zero, buttonDimensions,
                                        font, "SELECTIONPLAYER_MALE", PaletteColors.SelectionPlayer_Label,
                                        onClick: (btn) => OnGenreSelected(Genre.Male))
            {
                BackgroundColor = PaletteColors.SelectionPlayer_Genre_Background,
                BorderColor = PaletteColors.SelectionPlayer_Genre_Border
            };
            group.Add(maleButton);
            _genreButtons[Genre.Male] = maleButton;

            var femaleButton = new Button(maleButton.Position + new Vector2(maleButton.Dimensions.X, 0) + UIScaler.Scale(new Vector2(BUTTONS_OFFSET, 0)),
                                          buttonDimensions,
                                          font, "SELECTIONPLAYER_FEMALE", PaletteColors.SelectionPlayer_Label,
                                          onClick: (btn) => OnGenreSelected(Genre.Female))
            {
                BackgroundColor = PaletteColors.SelectionPlayer_Genre_Background,
                BorderColor = PaletteColors.SelectionPlayer_Genre_Border
            };
            group.Add(femaleButton);
            _genreButtons[Genre.Female] = femaleButton;

            return group;
        }
        private void OnGenreSelected(Genre genre)
        {
            if (_selectedGenre == genre)
                return;

            _selectedGenre = genre;
            UpdateGenreSelection();
            UpdateHeroSelection();
        }
        private void UpdateGenreSelection()
        {
            // Réinitialiser tous les boutons de genre
            foreach (var kvp in _genreButtons)
            {
                ResetGenreButtonColors(kvp.Value);
            }

            // Mettre en surbrillance le bouton sélectionné
            SetSelectedGenreButtonColors(_genreButtons[_selectedGenre]);
        }
        private void UpdateGenreGroupPosition()
        {
            var totalWidth = _genreButtons[Genre.Male].Dimensions.X + _genreButtons[Genre.Female].Dimensions.X + UIScaler.Scale(BUTTONS_OFFSET);
            var startX = (ScreenDimensions.X - totalWidth) / 2;
            var startY = _title.Position.Y + _title.Dimensions.Y + UIScaler.Scale(30);

            _genreGroup.Position = new Vector2(startX, startY);
        }
        private static void SetSelectedGenreButtonColors(Button button)
        {
            button.BorderColor = PaletteColors.SelectionPlayer_Genre_Border_Selected;
            button.BackgroundColor = PaletteColors.SelectionPlayer_Genre_Background_Selected;
        }
        private static void ResetGenreButtonColors(Button button)
        {
            button.BorderColor = PaletteColors.SelectionPlayer_Genre_Border;
            button.BackgroundColor = PaletteColors.SelectionPlayer_Genre_Background;
        }

        private Group CreateHeroGroup(Key<ResourceTag> playerKey, Genre genre, HeroClass heroClass)
        {
            Group group = [];

            var font = _fontManager.Load(FontKeys.SelectPlayer_Label);

            var texture = _resourceManager.Load<Texture2D>(playerKey);
            var button = new Button(default, texture.Bounds.Size.ToVector2() + UIScaler.Scale(BUTTON_MARGIN),
                                    font, string.Empty, PaletteColors.Transparent, texture)
            {
                BorderColor = PaletteColors.SelectionPlayer_Buttons_Border,
                BorderThickness = UIScaler.Scale(3)
            };

            button.OnClicked += (s, e) => OnHeroSelected(heroClass);

            // Initialiser le dictionnaire de boutons si nécessaire
            _heroButtons ??= new Dictionary<(Genre, HeroClass), Button>();
            _heroButtons[(genre, heroClass)] = button;

            group.Add(button);
            string content = heroClass.ToString().ToUpper() + "_" + genre.ToString().ToUpper();
            var label = new Text(font, content, PaletteColors.SelectionPlayer_Label,
                                 horizontalalignment: HorizontalAlignment.Center)
            {
                Dimensions = new Vector2(button.Dimensions.X, font.MeasureString(content).Y),
                Position = button.Position + new Vector2(0, button.Dimensions.Y + UIScaler.Scale(HERO_LABEL_OFFSET_Y))
            };
            group.Add(label);

            return group;
        }
        private void OnHeroSelected(HeroClass heroClass)
        {
            _selectedHeroClass = heroClass;
            UpdateHeroSelection();
        }
        private void UpdateHeroSelection()
        {
            // Réinitialiser tous les boutons de héros
            foreach (var kvp in _heroButtons)
            {
                ResetHeroButtonColors(kvp.Value);
            }

            // Mettre en surbrillance le bouton sélectionné
            var selectedButton = _heroButtons[(_selectedGenre, _selectedHeroClass)];
            SetSelectedHeroButtonColors(selectedButton);
        }
        private void UpdateHeroGroupsPositions()
        {
            var warriorGroup = _heroGroups[(Genre.Female, HeroClass.Warrior)];
            var mageGroup = _heroGroups[(Genre.Female, HeroClass.Mage)];
            var thiefGroup = _heroGroups[(Genre.Female, HeroClass.Thief)];

            var totalDimensions = warriorGroup.Dimensions + mageGroup.Dimensions + thiefGroup.Dimensions;
            var offsetX = (ScreenDimensions.X - totalDimensions.X) / 4;

            var position = new Vector2(offsetX, (ScreenDimensions.Y - warriorGroup.Dimensions.Y) / 2);

            // Positionner tous les groupes de héros
            foreach (var heroClass in Enum.GetValues<HeroClass>())
            {
                foreach (var genre in Enum.GetValues<Genre>())
                {
                    _heroGroups[(genre, heroClass)].Position = position;
                }

                position.X += _heroGroups[(Genre.Female, heroClass)].Dimensions.X + offsetX;
            }
        }
        private static void SetSelectedHeroButtonColors(Button button)
        {
            button.BorderColor = PaletteColors.SelectionPlayer_Border_Selected;
        }
        private static void ResetHeroButtonColors(Button button)
        {
            button.BorderColor = PaletteColors.SelectionPlayer_Buttons_Border;
        }

        private Group CreateButtonsGroup()
        {
            Group group = [];
            var font = _fontManager.Load(FontKeys.SelectPlayer_Label);
            var buttonDimensions = UIScaler.Scale(BUTTONS_DIMENSIONS);

            _back = new Button(Vector2.Zero, buttonDimensions,
                               font, "SELECTIONPLAYER_BACK", PaletteColors.SelectionPlayer_Label,
                               onClick: BackToMainMenu)
            {
                BackgroundColor = PaletteColors.SelectionPlayer_Buttons_Background,
                BorderColor = PaletteColors.SelectionPlayer_Buttons_Border
            };
            group.Add(_back);

            _continue = new Button(_back.Position + new Vector2(_back.Dimensions.X, 0) + UIScaler.Scale(new Vector2(BUTTONS_OFFSET, 0)), buttonDimensions,
                                   font, "SELECTIONPLAYER_NEXT", PaletteColors.SelectionPlayer_Label,
                                   onClick: ContinueToGame)
            {
                BackgroundColor = PaletteColors.SelectionPlayer_Buttons_Background,
                BorderColor = PaletteColors.SelectionPlayer_Buttons_Border
            };
            group.Add(_continue);

            return group;
        }
        private void BackToMainMenu(Button button)
        {
            SetCurrentScene(ProjectSceneKeys.GameScene);
        }
        private void ContinueToGame(Button button)
        {
            // Récupérer les informations de sélection
            var selectedGenre = _selectedGenre;
            var selectedHeroClass = _selectedHeroClass;

            var selectedHeroKey = _resourceKeys[(selectedGenre, selectedHeroClass)];

            var playerTexture = _resourceManager.Load<Texture2D>(selectedHeroKey);
            var bagTexture = _resourceManager.Load<Texture2D>(GameResourceKeys.Bag2Slots);
            Player player = PlayerFactory.CreatePlayer(selectedHeroClass, selectedGenre, playerTexture, bagTexture);
            ServiceLocator.Register(ProjectServiceKeys.Player, player);

            SetCurrentScene(ProjectSceneKeys.CityScene);
        }
    }
}