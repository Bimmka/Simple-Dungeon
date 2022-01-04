using StaticData.Loot.Items;
using UnityEngine;

namespace Services.Factories.Loot
{
  public interface ILootSpawner : ICleanupService
  {
    void SpawnMoney(int moneyCount, Vector3 position);
    void SpawnLoot(ItemStaticData droppedLoot, Vector3 position);
  }
}