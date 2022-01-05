using System;

namespace Services.PlayerData
{
  public class PlayerMoney
  {
    public int Count { get; private set; }

    public event Action Changed;

    public void AddMoney(int addedMoney)
    {
      Count += addedMoney;
      NotifyAboutChange();
    }

    public void ReduceMoney(int decedMoney)
    {
      Count -= decedMoney;
      NotifyAboutChange();
    }

    private void NotifyAboutChange() => 
      Changed?.Invoke();
  }
}