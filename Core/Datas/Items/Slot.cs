namespace Dungeon100Steps.Core.Datas.Items
{
    public class Slot(Item item, int quantity = 1)
    {
        public Item Item { get; private set; } = item;
        public int Quantity { get; private set; } = quantity;

        public bool Add(Item item)
        {
            if (Quantity < Item.StackLimit && item.GetType() == Item.GetType())
            {
                Quantity++;
                return true;
            }
            return false;
        }
        public void Remove(Item item)
        {
            if (Quantity > 0 && item.GetType() == Item.GetType())
                Quantity--;
        }
    }
}
