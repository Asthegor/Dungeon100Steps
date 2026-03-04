using DinaCSharp.Resources;
using DinaCSharp.Services;

namespace Dungeon100Steps.Core.Keys
{
    public static class GameResourceKeys
    {
        #region Player
        public static readonly Key<ResourceTag> Warrior_Female = Key<ResourceTag>.FromString("SelectionPlayer/Warrior_Female");
        public static readonly Key<ResourceTag> Mage_Female = Key<ResourceTag>.FromString("SelectionPlayer/Mage_Female");
        public static readonly Key<ResourceTag> Thief_Female = Key<ResourceTag>.FromString("SelectionPlayer/Thief_Female");

        public static readonly Key<ResourceTag> Warrior_Male = Key<ResourceTag>.FromString("SelectionPlayer/Warrior_Male");
        public static readonly Key<ResourceTag> Mage_Male = Key<ResourceTag>.FromString("SelectionPlayer/Mage_Male");
        public static readonly Key<ResourceTag> Thief_Male = Key<ResourceTag>.FromString("SelectionPlayer/Thief_Male");
        #endregion

        public static readonly Key<ResourceTag> Button_Next = Key<ResourceTag>.FromString("Button_Next");
        public static readonly Key<ResourceTag> Slot_Template_Default = Key<ResourceTag>.FromString("Inventory/Slot_Template_Default");

        #region Cité
        public static readonly Key<ResourceTag> City_Background = Key<ResourceTag>.FromString("City/Background");
        public static readonly Key<ResourceTag> City_Background_NoSelection = Key<ResourceTag>.FromString("City/Background_NoSelection");
        public static readonly Key<ResourceTag> City_Blacksmith = Key<ResourceTag>.FromString("City/Blacksmith");
        public static readonly Key<ResourceTag> City_Dungeon = Key<ResourceTag>.FromString("City/Dungeon");
        public static readonly Key<ResourceTag> City_Herborist = Key<ResourceTag>.FromString("City/Herborist");
        public static readonly Key<ResourceTag> City_Tavern = Key<ResourceTag>.FromString("City/Tavern");
        #endregion

        #region Bags
        public static readonly Key<ResourceTag> Bag2Slots = Key<ResourceTag>.FromString("Bags/Bag2Slots");
        public static readonly Key<ResourceTag> Bag4Slots = Key<ResourceTag>.FromString("Bags/Bag4Slots");
        public static readonly Key<ResourceTag> Bag6Slots = Key<ResourceTag>.FromString("Bags/Bag6Slots");
        public static readonly Key<ResourceTag> Bag8Slots = Key<ResourceTag>.FromString("Bags/Bag8Slots");
        public static readonly Key<ResourceTag> Bag10Slots = Key<ResourceTag>.FromString("Bags/Bag10Slots");
        #endregion
    }
}
