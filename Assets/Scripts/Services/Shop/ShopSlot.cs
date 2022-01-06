using StaticData.Loot.Items;
using StaticData.Shop;

namespace Services.Shop
{
  public class ShopSlot : ShopSlotStaticData
  {
    public ShopSlot(ItemStaticData item, int count)
    {
      Item = item;
      Count = count;
    }

    public void BuyItem()
    {
      Count--;

      if (Count == 0)
        Item = null;
    }
  }
}