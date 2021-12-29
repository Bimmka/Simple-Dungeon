using System;
using System.Collections.Generic;
using Services.Factories.GameFactories;
using UnityEngine;

namespace Enemies.Spawn
{
  public class EnemySpawner : IEnemySpawner
  {
    private readonly IEnemiesFactory enemiesFactory;
    private readonly List<SpawnPoint> spawnPoints = new List<SpawnPoint>(20);

    public event Action<GameObject> Spawned;

    public EnemySpawner(IEnemiesFactory enemiesFactory) => 
      this.enemiesFactory = enemiesFactory;

    public void AddPoint(SpawnPoint spawnPoint) => 
      spawnPoints.Add(spawnPoint);

    public void Spawn(EnemyTypeId[] enemies)
    {
      for (int i = 0; i < enemies.Length; i++)
      {
        Spawned?.Invoke(enemiesFactory.SpawnMonster(enemies[i], spawnPoints[0].transform));
      }
    }
  }
}