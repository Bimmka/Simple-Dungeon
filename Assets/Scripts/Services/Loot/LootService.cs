using Enemies.Entity;
using Enemies.Spawn;
using Services.Factories.Loot;
using Services.Random;
using Services.StaticData;
using StaticData.Loot;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Services.Loot
{
  public class LootService : ILootService
  {
    private readonly ILootSpawner lootSpawner;
    private readonly IRandomService randomService;
    private readonly IStaticDataService staticDataService;
    private readonly IEnemySpawner enemySpawner;

    private string levelName; 

    public LootService(ILootSpawner lootSpawner, IRandomService randomService, IStaticDataService staticDataService, IEnemySpawner enemySpawner)
    {
      this.lootSpawner = lootSpawner;
      this.randomService = randomService;
      this.staticDataService = staticDataService;
      this.enemySpawner = enemySpawner;
      this.enemySpawner.Spawned += SubscribeForEnemy;
    }

    public void Cleanup() => 
      enemySpawner.Spawned -= SubscribeForEnemy;

    public void SetSceneName(string name) => 
      levelName = name;

    private void SubscribeForEnemy(GameObject enemy) => 
      enemy.GetComponent<EnemyDeath>().Happened += OnEnemyDeath;

    private void OnEnemyDeath(EnemyTypeId enemyTypeId, GameObject enemyObject)
    {
      enemyObject.GetComponent<EnemyDeath>().Happened -= OnEnemyDeath;
      CreateLoot(enemyTypeId, enemyObject.transform.position);
    }

    private void CreateLoot(EnemyTypeId enemyTypeId, Vector3 position)
    {
      EnemyLoot loot = staticDataService.ForLoot(levelName, enemyTypeId);
      lootSpawner.SpawnMoney(randomService.Next(loot.MoneyCountRange.x, loot.MoneyCountRange.y), position);
    }
  }
}