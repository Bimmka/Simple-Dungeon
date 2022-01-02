using Enemies.Spawn;
using UnityEngine;

namespace Services.Factories.Enemy
{
  public interface IEnemiesFactory : IService
  {
    void Cleanup();
    GameObject SpawnMonster(EnemyTypeId typeId, Transform transform, float damageCoeff = 1f, float hpCoeff = 1f);
  }
}
