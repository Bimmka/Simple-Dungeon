using System;
using Services;
using UnityEngine;

namespace Enemies.Spawn
{
  public interface IEnemySpawner : IService
  {
    event Action<GameObject> Spawned;
    void AddPoint(SpawnPoint spawnPoint);
    void Spawn(EnemyTypeId[] enemies);
  }
}