using System.Collections.Generic;
using Systems.Healths;
using ConstantsValue;
using Enemies.Spawn;
using Hero;
using Services.Assets;
using Services.Factories.Enemy;
using Services.Input;
using Services.StaticData;
using Services.UI.Buttons;
using Services.UI.Windows;
using StaticData.Hero;
using StaticData.Level;
using UI.Base;
using UI.Displaying;
using UnityEngine;

namespace Services.Factories.GameFactories
{
  public class GameFactory : IGameFactory
  {
    private readonly IAssetProvider assets;
    private readonly IStaticDataService staticData;
    private readonly IInputService inputService;
    private readonly IEnemiesFactory enemiesFactory;
    private readonly IEnemySpawner spawner;
    private readonly IWindowsService windowsService;
    private GameObject heroGameObject;
    
    public GameFactory(IAssetProvider assets, IStaticDataService staticData, IInputService inputService, IEnemiesFactory enemiesFactory, IEnemySpawner spawner, IWindowsService windowsService)
    {
      this.assets = assets;
      this.staticData = staticData;
      this.inputService = inputService;
      this.enemiesFactory = enemiesFactory;
      this.spawner = spawner;
      this.windowsService = windowsService;
    }
    
    public GameObject CreateHero()
    {
      HeroSpawnStaticData spawnData = staticData.ForHero();
      heroGameObject = InstantiateObject(spawnData.HeroPrefab, spawnData.SpawnPoint);
      heroGameObject.GetComponent<HeroInput>().Construct(inputService);
      heroGameObject.GetComponent<HeroStateMachine>().Construct(spawnData.AttackData, spawnData.ImpactsData, heroGameObject.GetComponentInChildren<IHealth>());
      return heroGameObject;
    }

    public GameObject CreateHud(GameObject hero)
    {
      GameObject hud = assets.Instantiate<GameObject>(AssetsPath.Hud);
      hud.GetComponentInChildren<HPDisplayer>().Construct(hero.GetComponentInChildren<IHealth>());
      hud.GetComponentInChildren<StaminaDisplayer>().Construct(hero.GetComponentInChildren<IStamina>());
      hud.GetComponentInChildren<MoneyDisplayer>().Construct(hero.GetComponent<HeroMoney>());
      InitButtons(hud);
      return hud;
    }

    private void InitButtons(GameObject hud)
    {
      OpenWindowButton[] buttons = hud.GetComponentsInChildren<OpenWindowButton>(true);
      for (int i = 0; i < buttons.Length; i++)
      {
        buttons[i].Construct(windowsService);
      }
    }

    public void CreateEnemySpawnPoints(List<EnemySpawnerStaticData> spawnPoints, SpawnPoint pointPrefab)
    {
      for (int i = 0; i < spawnPoints.Count; i++)
      {
        spawner.AddPoint(CreateEnemySpawnPoint(spawnPoints[i], pointPrefab));
      }
    }

    private SpawnPoint CreateEnemySpawnPoint(EnemySpawnerStaticData data, SpawnPoint prefab)
    {
      SpawnPoint spawner = assets.Instantiate(prefab, data.Position).GetComponent<SpawnPoint>();
      spawner.Construct(data.Id);
      return spawner;
    }

    private GameObject InstantiateObject(GameObject prefab, Vector3 at) => 
      assets.Instantiate(prefab, at);

    private GameObject InstantiateObject(GameObject prefab) => 
      assets.Instantiate(prefab);

    private GameObject InstantiateObject(GameObject prefab, Transform parent) => 
      assets.Instantiate(prefab, parent);
  }
}