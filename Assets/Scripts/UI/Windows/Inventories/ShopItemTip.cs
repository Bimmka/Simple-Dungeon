using Loots;
using TMPro;
using UnityEngine;

namespace UI.Windows.Inventories
{
  public class ShopItemTip : InventoryItemTip
  {
    [SerializeField] private TextMeshProUGUI costText;

    public override void RemoveTip()
    {
      base.RemoveTip();
      SetText(costText, "");
    }

    public void DisplayTip(string itemName, Characteristic[] characteristics, int cost)
    {
      base.DisplayTip(itemName, characteristics);
      SetText(costText, cost.ToString());
    }
  }
}