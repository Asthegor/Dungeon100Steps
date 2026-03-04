using Microsoft.Xna.Framework.Graphics;

namespace Dungeon100Steps.Core.Datas.Items
{
    public class Inventory(Texture2D texture)
    {
        public List<Slot> Slots { get; private set; } = [];
        public int Capacity { get; set; } = 2;
        public Texture2D Texture { get; set; } = texture;

        public AddToInventoryResult Add(Item item)
        {
            Slot? slot = Slots.FirstOrDefault(s => s.Item == item);
            if (slot == null)
            {
                if (Slots.Count >= Capacity)
                    return AddToInventoryResult.InventoryFull;
                
                // Ajout d'une nouvelle pile de Item
                Slots.Add(new Slot(item));
                return AddToInventoryResult.Added;
            }

            if (slot.Quantity >= item.StackLimit)
                return AddToInventoryResult.StackLimitExcedeed;

            if (slot.Add(item))
                return AddToInventoryResult.Added;

            // Erreur inattendue
            throw new Exception("Unexpected error while adding item to inventory.");
        }
        public void Remove(Item item)
        {
            Slot? slot = Slots.FirstOrDefault(s => s.Item == item);
            if (slot == null)
                return;

            // Suppression d'un item de la pile
            slot.Remove(item);
            if (slot.Quantity == 0)
                Slots.Remove(slot);
        }
    }
}