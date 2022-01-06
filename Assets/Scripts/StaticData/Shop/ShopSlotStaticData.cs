using System;
using StaticData.Loot.Items;

namespace StaticData.Shop
{
  [Serializable]
  public class ShopSlotStaticData
  {
    public ItemStaticData Item;
    public int Count;
  }
}