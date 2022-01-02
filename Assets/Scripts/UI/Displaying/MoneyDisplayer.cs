using System;
using Hero;
using TMPro;
using UnityEngine;

namespace UI.Displaying
{
  public class MoneyDisplayer : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI moneyText;
    
    private HeroMoney heroMoney;

    public void Construct(HeroMoney heroMoney)
    {
      this.heroMoney = heroMoney;
      this.heroMoney.Changed += Display;
    }

    private void OnDestroy() => 
      heroMoney.Changed -= Display;

    private void Display(int money) => 
      moneyText.text = money.ToString();
  }
}