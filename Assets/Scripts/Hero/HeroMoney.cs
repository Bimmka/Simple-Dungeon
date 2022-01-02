using System;
using UnityEngine;

namespace Hero
{
  public class HeroMoney : MonoBehaviour
  {
    private int moneyCount;
    public event Action<int> Changed;

    private void Start() => 
      Display();

    public void AddMoney(int addedMoney)
    {
      moneyCount += addedMoney;
      Display();
    }

    public void ReduceMoney(int decedMoney)
    {
      moneyCount -= decedMoney;
      Display();
    }

    public bool IsEnoughMoney(int neededCount) => 
      moneyCount >= neededCount;

    private void Display() => 
      Changed?.Invoke(moneyCount);
  }
}