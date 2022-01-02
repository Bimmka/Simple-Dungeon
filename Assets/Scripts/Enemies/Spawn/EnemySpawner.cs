using System;
using System.Collections.Generic;
using Services.Factories.GameFactories;
using StaticData.Level;
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

    public void Spawn(WaveEnemy[] enemies)
    {
      for (int i = 0; i < enemies.Length; i++)
      {
        SpawnEnemy(enemies[i]);
      }
    }

    private void SpawnEnemy(WaveEnemy waveEnemy)
    {
      for (int i = 0; i < waveEnemy.Count; i++)
      {
        Spawned?.Invoke(enemiesFactory.SpawnMonster(waveEnemy.Id, spawnPoints[0].transform, waveEnemy.DamageCoeff, waveEnemy.HpCoeff));
      }
      
    }
  }
}