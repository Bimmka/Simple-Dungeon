using Enemies;
using Enemies.Spawn;
using UnityEngine;

namespace Services.Factories.GameFactories
{
  public interface IEnemiesFactory : IService
  {
    void Cleanup();
    GameObject SpawnMonster(EnemyTypeId typeId, Transform parent);
  }
}