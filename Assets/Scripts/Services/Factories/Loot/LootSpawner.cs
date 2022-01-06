using System.Collections;
using System.Collections.Generic;
using ConstantsValue;
using Interfaces;
using Loots;
using Services.Assets;
using StaticData.Loot.Items;
using UnityEngine;

namespace Services.Factories.Loot
{
  public class LootSpawner : ILootSpawner
  {
    private readonly IAssetProvider assetProvider;

    private readonly Queue<Money> monies;
    private readonly Queue<DroppedLoot> droppedLoots;

    public LootSpawner(IAssetProvider assetProvider)
    {
      this.assetProvider = assetProvider;
      monies = new Queue<Money>(30);
      droppedLoots = new Queue<DroppedLoot>(30);
    }

    public void Cleanup()
    {
      Money money;
      while (monies.Count > 0)
      {
        money = monies.Dequeue();
        money.PickedUp -= OnMoneyPickedUp;
      }

      DroppedLoot loot;
      while (droppedLoots.Count > 0)
      {
        loot = droppedLoots.Dequeue();
        loot.PickedUp -= OnLootPickedUp;
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

    public void SpawnLoot(ItemStaticData droppedLoot, Vector3 position)
    {
      DroppedLoot loot = droppedLoots.Count > 0 ? droppedLoots.Dequeue() : CreateLoot();
      loot.SetPosition(position);
      loot.SetItem(droppedLoot);
      loot.Show();
    }

    private Money CreateMoney()
    {
      Money money = assetProvider.Instantiate<Money>(AssetsPath.MoneyPrefabPath);
      money.Hide();
      money.PickedUp += OnMoneyPickedUp;
      return money;
    }

    private DroppedLoot CreateLoot()
    {
      DroppedLoot loot = assetProvider.Instantiate<DroppedLoot>(AssetsPath.DroppedLootPrefabPath);
      loot.Hide();
      loot.PickedUp += OnLootPickedUp;
      return loot;
    }

    private void OnMoneyPickedUp(Money money)
    {
      money.Hide();
      monies.Enqueue(money);
    }

    private void OnLootPickedUp(DroppedLoot loot)
    {
      loot.Hide();
      droppedLoots.Enqueue(loot);
    }
  }
}