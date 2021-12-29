using System.Collections.Generic;
using Enemies;
using Enemies.Spawn;
using UnityEngine;

namespace StaticData.Level
{
  [CreateAssetMenu(fileName = "LevelStaticData", menuName = "Static Data/Levels/Create Level Data", order = 55)]
  public class LevelStaticData : ScriptableObject
  {
    public string LevelKey;
    public List<EnemySpawnerStaticData> EnemySpawners;
    public LevelWaveStaticData LevelWaves;

    public SpawnPoint SpawnPointPrefab;
    public EnemySpawner EnemySpawner;
  }
}