using UnityEngine;

namespace Services.Factories.Loot
{
  public interface ILootSpawner : ICleanupService
  {
    void SpawnMoney(int moneyCount, Vector3 position);
  }
}