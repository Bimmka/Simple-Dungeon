using Enemies.Entity;
using Enemies.Spawn;
using Services.Factories.GameFactories;
using StaticData.Level;
using UnityEngine;

namespace Services.Waves
{
  public class WaveServices : IWaveServices
  {
    private readonly IEnemySpawner enemiesSpawner;
    private LevelWaveStaticData waves;

    private int currentEnemiesCount;
    private int currentWaveIndex;

    public WaveServices(IEnemySpawner spawner)
    {
      enemiesSpawner = spawner;
      enemiesSpawner.Spawned += OnEnemySpawned;
    }

    public void Start()
    {
      currentWaveIndex = 0;
      StartWave();
    }
    
    public void SetLevelWaves(LevelWaveStaticData wavesData) => 
      waves = wavesData;

    private void OnEnemySpawned(GameObject enemy) => 
      enemy.GetComponent<EnemyDeath>().Happened += OnEnemyDead;

    private void OnEnemyDead(EnemyTypeId enemyTypeId, GameObject enemy)
    {
      enemy.GetComponent<EnemyDeath>().Happened -= OnEnemyDead;
      currentEnemiesCount--;
      if (currentEnemiesCount <= 0)
        CompleteWave();
    }

    private void CompleteWave()
    {
      currentWaveIndex++;
      currentWaveIndex = Mathf.Clamp(currentWaveIndex, 0, waves.Waves.Length);
      StartWave();
    }

    private void StartWave()
    {
      enemiesSpawner.Spawn(waves.Waves[currentWaveIndex].Enemies);
      currentEnemiesCount = waves.Waves[currentWaveIndex].Enemies.Length;
    }
  }
}