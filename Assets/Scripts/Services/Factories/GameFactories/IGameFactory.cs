using System.Collections.Generic;
using Enemies;
using Enemies.Spawn;
using StaticData.Level;
using UnityEngine;

namespace Services.Factories.GameFactories
{
  public interface IGameFactory : IService
  {
    GameObject CreateHero();
    GameObject CreateHud(GameObject gameObject);
    void CreateEnemySpawner(List<EnemySpawnerStaticData> spawnPoints, EnemySpawner spawnerPrefab, SpawnPoint pointPrefab);
  }
}