using UnityEngine;

namespace Hero
{
  public class HeroMoney : MonoBehaviour
  {
    private int moneyCount;
    
    public void AddMoney(int addedMoney)
    {
      moneyCount += addedMoney;
      Debug.Log(moneyCount);
    }

    public void ReduceMoney(int decedMoney)
    {
      moneyCount -= decedMoney;
      Debug.Log(moneyCount);
    }

    public bool IsEnoughMoney(int neededCount) => 
      moneyCount >= neededCount;
  }
}