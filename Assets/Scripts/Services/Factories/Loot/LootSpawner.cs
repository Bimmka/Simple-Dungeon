using System.Collections;
using System.Collections.Generic;
using ConstantsValue;
using Loots;
using Services.Assets;
using UnityEngine;

namespace Services.Factories.Loot
{
  public class LootSpawner : ILootSpawner
  {
    private readonly IAssetProvider assetProvider;

    private readonly Queue<Money> monies;

    public LootSpawner(IAssetProvider assetProvider)
    {
      this.assetProvider = assetProvider;
      monies = new Queue<Money>(30);
    }

    public void Cleanup()
    {
      Money money;
      while (monies.Count > 0)
      {
        money = monies.Dequeue();
        money.PickedUp -= OnMoneyPickedUp;
      }
    }

    public void SpawnMoney(int moneyCount, Vector3 position)
    {
      Money money;
      for (int i = 0; i < moneyCount; i++)
      {
        money = monies.Count > 0 ? monies.Dequeue() : CreateMoney();
        money.SetPosition(position);
        money.Show();
      }
    }

    private void CreateMonies(int count)
    {
      for (int i = 0; i < count; i++)
      {
        monies.Enqueue(CreateMoney());
      }
    }

    private Money CreateMoney()
    {
      Money money = assetProvider.Instantiate<Money>(AssetsPath.MoneyPrefabPath);
      money.Hide();
      money.PickedUp += OnMoneyPickedUp;
      return money;
    }

    private void OnMoneyPickedUp(Money money)
    {
      money.Hide();
      monies.Enqueue(money);
    }
  }
}