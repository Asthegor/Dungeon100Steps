using Microsoft.Xna.Framework;

namespace Dungeon100Steps.Core.Keys
{
    public class PaletteColors
    {

        // Définit les couleurs utilisées dans l'application
        // Valeur par défaut pour le titre, l'ombre du titre
        // et les items du menu désactivés

        public static readonly Color Transparent = Color.Transparent;

        public static readonly Color MainMenu_Title = Color.MonoGameOrange;
        public static readonly Color MainMenu_Title_Shadow = Color.DarkGray;
        public static readonly Color MenuItem_Disabled = Color.DarkSlateGray;
        public static readonly Color MenuItem = Color.White;
        public static readonly Color MenuItem_Hovered = Color.Yellow;

        public static readonly Color Message = Color.Wheat;
        public static readonly Color Message_Panel_Background = Color.Blue;
        public static readonly Color Message_Panel_Border = Color.White;
        public static readonly Color Message_Continue = Color.Wheat;


        // Écran de chargement
        public static readonly Color Loading_Title = Color.MonoGameOrange;
        public static readonly Color Loading_Message = Color.Orange;
        public static readonly Color Loading_Progress_Front = Color.DarkOrange;
        public static readonly Color Loading_Progress_Border = Color.Orange;
        public static readonly Color Loading_Progress_Back = Color.OrangeRed;

        #region Écran des options
        public static readonly Color Options_Title = Color.MonoGameOrange;
        public static readonly Color Options_Title_Shadow = Color.DarkGray;
        public static readonly Color Options_Category = Color.LightGray;
        public static readonly Color Options_Label = Color.White;
        public static readonly Color Options_Button_Text = Color.White;
        public static readonly Color Options_Button_Background = Color.White * 0.25f;
        public static readonly Color Options_Button_Border = Color.White;
        public static readonly Color Options_Button_Back_Border = Color.White;
        public static readonly Color Options_Button_Back_Background = Color.White * 0.25f;
        public static readonly Color Options_Button_Back_Hovered = Color.Yellow;
        public static readonly Color Options_Button_Reset_Border = Color.DarkRed;
        public static readonly Color Options_Button_Reset_Background = Color.Red * 0.25f;
        public static readonly Color Options_Button_Reset_Hovered = Color.Orange;
        #endregion

        #region Écran du jeu
        public static readonly Color TimeLine_Front = Color.LimeGreen;
        public static readonly Color TimeLine_Border = Color.White;
        public static readonly Color TimeLine_Back = Color.DarkGray;
        public static readonly Color TimeLine_Label = Color.White;

        // Écran de chargement
        public static readonly Color Loading_Game_Title = Color.Gold;
        public static readonly Color Loading_Game_Message = Color.Yellow;
        public static readonly Color Loading_Game_Progress_Front = Color.Gold;
        public static readonly Color Loading_Game_Progress_Border = Color.Goldenrod;
        public static readonly Color Loading_Game_Progress_Back = Color.DarkGoldenrod;

        #region Écran de sélection du personnage
        public static readonly Color SelectionPlayer_Label = Color.White;
        public static readonly Color SelectionPlayer_Background_Selected = Color.Goldenrod;
        public static readonly Color SelectionPlayer_Background_Hover = Color.DarkSlateGray;
        public static readonly Color SelectionPlayer_Border_Selected = Color.Gold;
        public static readonly Color SelectionPlayer_Border_Hover = Color.White;

        public static readonly Color SelectionPlayer_Genre_Border = Color.White;
        public static readonly Color SelectionPlayer_Genre_Background = Color.White * 0.25f;
        public static readonly Color SelectionPlayer_Genre_Background_Selected = Color.Goldenrod;
        public static readonly Color SelectionPlayer_Genre_Background_Hover = Color.DarkSlateGray;
        public static readonly Color SelectionPlayer_Genre_Border_Selected = Color.Gold;
        public static readonly Color SelectionPlayer_Genre_Border_Hover = Color.DarkGray;

        public static readonly Color SelectionPlayer_Hero_Border = Color.White;
        public static readonly Color SelectionPlayer_Hero_Background = Color.White;
        public static readonly Color SelectionPlayer_Hero_Background_Hover = Color.White;
        public static readonly Color SelectionPlayer_Hero_Background_Selected = Color.White;
        public static readonly Color SelectionPlayer_Hero_Border_Hover = Color.Wheat;
        public static readonly Color SelectionPlayer_Hero_Border_Selected = Color.Gold;

        public static readonly Color SelectionPlayer_Buttons_Background = Color.White * 0.25f;
        public static readonly Color SelectionPlayer_Buttons_Border = Color.White;
        #endregion

        #region Écran d'attente
        // Player
        public static readonly Color Player_Label = Color.Gold;
        public static readonly Color Player_Value = Color.Wheat;

        // Item
        public static readonly Color Equipment_Name_Junk = Color.Gray;
        public static readonly Color Equipment_Name_Common = Color.White;
        public static readonly Color Equipment_Name_Uncommon = Color.LawnGreen;
        public static readonly Color Equipment_Name_Rare = Color.Gold;
        public static readonly Color Equipment_Name_Elite = Color.Purple;
        public static readonly Color Equipment_Border = Color.DarkGoldenrod;
        public static readonly Color Equipment_Bonus_Label = Color.Gray;
        public static readonly Color Equipment_Bonus_Text = Color.Gray;
        #endregion

        #endregion

        #region Écran pour passer le tutoriel
        public static readonly Color TutorialSkip_Panel_Background = Color.DarkSlateGray * 0.9f;
        public static readonly Color TutorialSkip_Panel_Border = Color.OrangeRed;
        public static readonly Color TutorialSkip_Label = Color.White;
        public static readonly Color TutorialSkip_NoButton_Background = Color.Firebrick;
        public static readonly Color TutorialSkip_NoButton_Border = Color.DarkRed;
        public static readonly Color TutorialSkip_NoButton_Hovered = Color.Orange;
        public static readonly Color TutorialSkip_YesButton_Background = Color.ForestGreen;
        public static readonly Color TutorialSkip_YesButton_Border = Color.DarkGreen;
        public static readonly Color TutorialSkip_YesButton_Hovered = Color.LimeGreen;
        #endregion

        #region Écran du combat
        public static readonly Color Combat_Menu_Background = Color.Blue;
        public static readonly Color Combat_Menu_Border = Color.White;
        public static readonly Color Combat_HealthBar_Front = Color.Firebrick;
        public static readonly Color Combat_HealthBar_Border = Color.Red;
        public static readonly Color Combat_HealthBar_Back = Color.DarkRed;
        public static readonly Color Combat_ManaBar_Front = Color.DodgerBlue;
        public static readonly Color Combat_ManaBar_Border = Color.Blue;
        public static readonly Color Combat_ManaBar_Back = Color.DarkBlue;
        #endregion

        #region Écran de l'inventaire
        public static readonly Color Inventory_Item_Selected_Background = Color.Transparent;
        public static readonly Color Inventory_Item_Selected_Border = Color.Gold;
        public static readonly Color Inventory_Item_Background = Color.Transparent;
        public static readonly Color Inventory_Item_Border = Color.Blue;
        public static readonly Color Inventory_NoItem_Background = Color.White * 0.5f;
        public static readonly Color Inventory_NoItem_Border = Color.White;
        public static readonly Color Inventory_ItemMenu_Text = Color.White;
        public static readonly Color Inventory_ItemMenu_Text_Selected = Color.Gold;
        public static readonly Color Inventory_ItemMenu_Background = Color.Blue;
        public static readonly Color Inventory_ItemMenu_Border = Color.White;
        #endregion

        #region Écran de pause
        public static readonly Color Pause_Title = Color.Gold;
        public static readonly Color Pause_Title_Shadow = Color.Orange;
        public static readonly Color Pause_Text = Color.White;
        public static readonly Color Pause_Text_Selected = Color.Gold;
        #endregion

        #region Bouton Retour
        public static readonly Color BackButton_Text = Color.Black;
        public static readonly Color BackButton_Text_Hovered = Color.Orange;
        #endregion

    }
}
