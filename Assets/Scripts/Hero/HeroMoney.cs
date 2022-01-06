using System;
using Services.PlayerData;
using UnityEngine;

namespace Hero
{
  public class HeroMoney : MonoBehaviour
  {
    private PlayerMoney money;
    public event Action<int> Changed;

    public void Construct(PlayerMoney money)
    {
      this.money = money;
      this.money.Changed += Display;
      Display();
    }

    private void OnDestroy() => 
      money.Changed -= Display;

    public void AddMoney(int addedMoney) => 
      money.AddMoney(addedMoney);

    public void ReduceMoney(int decedMoney) => 
      money.ReduceMoney(decedMoney);

    private void Display() => 
      Changed?.Invoke(money.Count);
  }
}