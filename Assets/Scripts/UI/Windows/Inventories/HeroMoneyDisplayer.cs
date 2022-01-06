using Services.PlayerData;
using TMPro;
using UnityEngine;

namespace UI.Windows.Inventories
{
  public class HeroMoneyDisplayer : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI moneyCountText;
    
    private PlayerMoney money;

    public void Construct(PlayerMoney money)
    {
      this.money = money;
      this.money.Changed += DisplayMoneyCount;
      DisplayMoneyCount();
    }

    public void Cleanup() => 
      money.Changed -= DisplayMoneyCount;

    private void DisplayMoneyCount() => 
      moneyCountText.text = money.Count.ToString();
  }
}