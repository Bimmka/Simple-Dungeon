﻿using System.Collections.Generic;
using System.Linq;
using ConstantsValue;
using Enemies;
using Enemies.Spawn;
using Services.UI.Factory;
using StaticData.Enemies;
using StaticData.Hero;
using StaticData.Level;
using StaticData.Loot;
using StaticData.UI;
using UnityEngine;

namespace Services.StaticData
{
  public class StaticDataService : IStaticDataService
  {
    private Dictionary<WindowId, WindowInstantiateData> windows;
    private Dictionary<EnemyTypeId, EnemyStaticData> enemies;
    private Dictionary<string, LevelStaticData> levels;
    private Dictionary<string, EnemyLoot[]> loots;
    
    private HeroSpawnStaticData heroSpawnData;
    private HeroBaseStaticData heroCharacteristics;
    
    public void Load()
    {
      heroSpawnData = Resources.Load<HeroSpawnStaticData>(AssetsPath.HeroSpawnDataPath);

      heroCharacteristics = Resources.Load<HeroBaseStaticData>(AssetsPath.HeroCharacteristicsDataPath);

      enemies = Resources
        .LoadAll<EnemyStaticData>(AssetsPath.EnemiesDataPath)
        .ToDictionary(x => x.Id, x => x);
      
      levels = Resources
        .LoadAll<LevelStaticData>(AssetsPath.LevelsDataPath)
        .ToDictionary(x => x.LevelKey, x => x);
      
      loots = Resources
        .LoadAll<LevelLootStaticData>(AssetsPath.LootsDataPath)
        .ToDictionary(x => x.LevelKey, x => x.Loots);

      windows = Resources
        .Load<WindowsStaticData>(AssetsPath.WindowsDataPath)
        .InstantiateData
        .ToDictionary(x => x.ID, x => x);
    }
    
    public WindowInstantiateData ForWindow(WindowId windowId) =>
      windows.TryGetValue(windowId, out WindowInstantiateData staticData)
        ? staticData 
        : new WindowInstantiateData();

    public HeroSpawnStaticData ForHero() => 
      heroSpawnData;

    public HeroBaseStaticData ForHeroCharacteristics() => 
      heroCharacteristics;

    public EnemyStaticData ForMonster(EnemyTypeId typeId) =>
    enemies.TryGetValue(typeId, out EnemyStaticData staticData)
        ? staticData 
        : null;

    public LevelStaticData ForLevel(string sceneKey) => 
      levels.TryGetValue(sceneKey, out LevelStaticData staticData)
        ? staticData 
        : null;

    public EnemyLoot ForLoot(string levelKey, EnemyTypeId typeId)
    {
      loots.TryGetValue(levelKey, out EnemyLoot[] enemyLoots);
      for (int i = 0; i < enemyLoots.Length; i++)
      {
        if (enemyLoots[i].TypeIds.Contains(typeId))
          return enemyLoots[i];
      }
      return new EnemyLoot();
    }
  }
}