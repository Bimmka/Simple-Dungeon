﻿using System.Collections.Generic;
using Systems.Healths;
using Enemies;
using Enemies.Entity;
using Enemies.Spawn;
using Services.Assets;
using Services.StaticData;
using StaticData.Enemies;
using UI.Displaying;
using UnityEngine;

namespace Services.Factories.GameFactories
{
  public class EnemiesFactory : IEnemiesFactory
  {
    private readonly IAssetProvider assets;
    private readonly IStaticDataService staticData;

    private List<SlainedEnemy> enemiesPool;
    
   
    public EnemiesFactory(IAssetProvider assets, IStaticDataService staticData)
    {
      this.assets = assets;
      this.staticData = staticData;
      enemiesPool = new List<SlainedEnemy>(10);
    }

    public void Cleanup()
    {
      for (int i = 0; i < enemiesPool.Count; i++)
      {
        enemiesPool[i].Enemy.GetComponent<EnemyDeath>().Happened -= OnMonsterSlained;
      }

      enemiesPool.Clear();
    }
    
    public GameObject SpawnMonster(EnemyTypeId typeId, Transform parent)
    {
      if (IsContainsInPool(typeId))
        return RecreateMonster(typeId, parent);

      return CreateMonster(typeId, parent);
    }

    private GameObject RecreateMonster(EnemyTypeId typeId, Transform parent)
    {
      EnemyStaticData enemyData = staticData.ForMonster(typeId);
      SlainedEnemy slainedEnemy = PooledMonster(typeId);
      
      GameObject monster = slainedEnemy.Enemy;
      
      IHealth health = monster.GetComponentInChildren<IHealth>();
      health.SetHp(enemyData.Hp, enemyData.Hp);
      EnemyDeath death = monster.GetComponent<EnemyDeath>();
      death.Revive();
      death.Happened += OnMonsterSlained;
      RemoveFromPool(slainedEnemy);
      monster.transform.position = parent.position;
      return monster;
    }

    private GameObject CreateMonster(EnemyTypeId typeId, Transform parent)
    {
      EnemyStaticData enemyData = staticData.ForMonster(typeId);
      GameObject monster = assets.Instantiate(enemyData.Prefab, parent.position, Quaternion.identity, parent);

      IHealth health = monster.GetComponentInChildren<IHealth>();
      health.SetHp(enemyData.Hp, enemyData.Hp);

      monster.GetComponentInChildren<HPDisplayer>().Construct(health);
      monster.GetComponent<EnemyStateMachine>().Construct(enemyData.MoveData, enemyData.AttackData, health);
      EnemyDeath death = monster.GetComponent<EnemyDeath>();
      death.Construct(enemyData.Id);
      death.Happened += OnMonsterSlained;

      return monster;
    }

    private void OnMonsterSlained(EnemyTypeId typeId, GameObject enemy)
    {
      enemiesPool.Add(new SlainedEnemy(typeId, enemy));
      enemy.GetComponent<EnemyDeath>().Happened -= OnMonsterSlained;
    }

    private bool IsContainsInPool(EnemyTypeId typeId)
    {
      for (int i = 0; i < enemiesPool.Count; i++)
      {
        if (enemiesPool[i].Id == typeId)
          return true;
      }

      return false;
    }

    private SlainedEnemy PooledMonster(EnemyTypeId typeId)
    {
      for (int i = 0; i < enemiesPool.Count; i++)
      {
        if (enemiesPool[i].Id == typeId)
          return enemiesPool[i];
      }

      return new SlainedEnemy();
    }

    private void RemoveFromPool(SlainedEnemy slainedEnemy) => 
      enemiesPool.Remove(slainedEnemy);
  }
}