using System;
using System.Collections.Generic;
using StaticData.Loot.Items;

namespace Services.Shop
{
  public interface IShopService : IService
  {
    IEnumerable<ShopSlot> Slots { get; }
    event Action Changed;
    void InitSlots();
    bool IsCanBuyItem(ItemStaticData item);
    void BuyItem(ItemStaticData item);
  }
}