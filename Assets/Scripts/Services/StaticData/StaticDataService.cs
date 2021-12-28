using System.Collections.Generic;
using System.Linq;
using ConstantsValue;
using Enemies;
using Enemies.Spawn;
using Services.UI.Factory;
using StaticData.Enemies;
using StaticData.Hero;
using StaticData.Level;
using StaticData.UI;
using UnityEngine;

namespace Services.StaticData
{
  public class StaticDataService : IStaticDataService
  {
    private Dictionary<WindowId, WindowInstantiateData> windows;
    private Dictionary<EnemyTypeId, EnemyStaticData> enemies;
    private Dictionary<string, LevelStaticData> levels;
    private HeroSpawnStaticData heroData;
    public void Load()
    {
      heroData = Resources.Load<HeroSpawnStaticData>(AssetsPath.HeroDataPath);
      enemies = Resources
        .LoadAll<EnemyStaticData>(AssetsPath.EnemiesDataPath)
        .ToDictionary(x => x.Id, x => x);
      
      levels = Resources
        .LoadAll<LevelStaticData>(AssetsPath.LevelsDataPath)
        .ToDictionary(x => x.LevelKey, x => x);
    }
    
    public WindowInstantiateData ForWindow(WindowId windowId) =>
      windows.TryGetValue(windowId, out WindowInstantiateData staticData)
        ? staticData 
        : new WindowInstantiateData();

    public HeroSpawnStaticData ForHero() => 
      heroData;

    public EnemyStaticData ForMonster(EnemyTypeId typeId) =>
    enemies.TryGetValue(typeId, out EnemyStaticData staticData)
        ? staticData 
        : null;

    public LevelStaticData ForLevel(string sceneKey) => 
      levels.TryGetValue(sceneKey, out LevelStaticData staticData)
        ? staticData 
        : null;
  }
}