using System;
using System.Collections.Generic;
using Services.PlayerData;
using Services.Random;
using StaticData.Loot.Items;
using StaticData.Shop;

namespace Services.Shop
{
  public class ShopService : IShopService
  {
    private readonly PlayerMoney playerMoney;
    private readonly Inventory inventory;
    private readonly IRandomService randomService;
    private readonly ShopStaticData shopData;

    private List<ShopSlot> shopSlots;

    public IEnumerable<ShopSlot> Slots => shopSlots;

    public event Action Changed;

    public ShopService(PlayerMoney playerMoney, Inventory inventory, IRandomService randomService, ShopStaticData shopData)
    {
      this.playerMoney = playerMoney;
      this.inventory = inventory;
      this.shopData = shopData;
      this.randomService = randomService;
    }

    public void InitSlots()
    {
      int itemsCount = randomService.Next(shopData.ItemCountRange.x, shopData.ItemCountRange.y);
      shopSlots = new List<ShopSlot>(itemsCount);
      for (int i = 0; i < itemsCount; i++)
      {
        shopSlots.Add(new ShopSlot(RandomItem(), 1));
      }
    }

    public bool IsCanBuyItem(ItemStaticData item) => 
      playerMoney.IsEnoughMoney(item.BuyCost) && inventory.IsCanAddItem(item);

    public void BuyItem(ItemStaticData item)
    {
      playerMoney.ReduceMoney(item.BuyCost);
      inventory.AddItem(item);
      
      GetSlot(item).BuyItem();
      Changed?.Invoke();
    }

    private ShopSlot GetSlot(ItemStaticData item)
    {
      for (int i = 0; i < shopSlots.Count; i++)
      {
        if (shopSlots[i].Item == item)
          return shopSlots[i];
      }

      return null;
    }

    private ItemStaticData RandomItem() => 
      shopData.Items[randomService.Next(shopData.Items.Length)];
  }
}