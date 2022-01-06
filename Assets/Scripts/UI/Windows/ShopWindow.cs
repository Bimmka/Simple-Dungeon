using Services.PlayerData;
using Services.Shop;
using StaticData.Loot.Items;
using UI.Base;
using UI.Windows.Inventories;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
  public class ShopWindow : BaseWindow
  {
    [SerializeField] private Button closeButton;
    [SerializeField] private Button buyButton;
    [SerializeField] private UIInventorySlot[] slots;
    [SerializeField] private ShopItemTip tip;
    [SerializeField] private HeroMoneyDisplayer moneyDisplayer;

    private IShopService shopService;
    private ItemStaticData lastClickedItem;

    public void Construct(IShopService shopService, PlayerMoney money)
    {
      this.shopService = shopService;
      this.shopService.Changed += UpdateSlots;
      
      moneyDisplayer.Construct(money);
      
      for (int i = 0; i < slots.Length; i++)
      {
        slots[i].Clicked += OnSlotClicked;
      }
    }

    public override void Open()
    {
      UpdateSlots();
      RemoveTip();
      ResetLastClickedItem();
      base.Open();
    }

    protected override void Subscribe()
    {
      base.Subscribe();
      buyButton.onClick.AddListener(TryBuyItem);
      closeButton.onClick.AddListener(Close);
    }

    protected override void Cleanup()
    {
      base.Cleanup();
      buyButton.onClick.RemoveListener(TryBuyItem);
      closeButton.onClick.RemoveListener(Close);
      shopService.Changed -= UpdateSlots;
      for (int i = 0; i < slots.Length; i++)
      {
        slots[i].Clicked -= OnSlotClicked;
      }
    }

    private void TryBuyItem()
    {
      if (lastClickedItem != null && shopService.IsCanBuyItem(lastClickedItem))
      {
        shopService.BuyItem(lastClickedItem);
        ChangeBuyButtonActiveState(false);
        ResetLastClickedItem();
        RemoveTip();
      }
    }

    private void UpdateSlots()
    {
      ResetSlots();
      int index = 0;
      foreach (ShopSlot shopSlot in shopService.Slots)
      {
        slots[index].SetItem(shopSlot.Item, shopSlot.Count);
        index++;
      }
    }

    private void OnSlotClicked(ItemStaticData item)
    {
      if (item != null)
      {
        lastClickedItem = item;
        DisplayTip(item);
        ChangeBuyButtonActiveState(true);
      }
    }

    private void DisplayTip(ItemStaticData item) => 
      tip.DisplayTip(item.Name, item.Characteristics, item.BuyCost);

    private void RemoveTip() => 
      tip.RemoveTip();

    private void ResetSlots()
    {
      for (int i = 0; i < slots.Length; i++)
      {
        slots[i].SetItem(null,0);
      }
    }

    private void ChangeBuyButtonActiveState(bool isActive)
    {
      if (closeButton.gameObject.activeSelf != isActive)
        closeButton.gameObject.SetActive(isActive);
    }

    private void ResetLastClickedItem() => 
      lastClickedItem = null;
  }
}