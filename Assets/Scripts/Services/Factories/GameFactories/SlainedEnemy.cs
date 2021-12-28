using Enemies;
using Enemies.Spawn;
using UnityEngine;

namespace Services.Factories.GameFactories
{
  public struct SlainedEnemy
  {
    public EnemyTypeId Id;
    public GameObject Enemy;

    public SlainedEnemy(EnemyTypeId id, GameObject enemy)
    {
      Id = id;
      Enemy = enemy;
    }
  }
}