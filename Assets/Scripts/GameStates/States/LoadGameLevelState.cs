using System.Collections.Generic;
using Systems.Healths;
using CodeBase.CameraLogic;
using ConstantsValue;
using Enemies;
using Enemies.Spawn;
using GameStates.States.Interfaces;
using SceneLoading;
using Services.Factories.GameFactories;
using Services.Factories.Loot;
using Services.Loot;
using Services.Progress;
using Services.StaticData;
using Services.UI.Factory;
using Services.Waves;
using StaticData.Level;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameStates.States
{
  public class LoadGameLevelState : IState
  {
    private readonly ISceneLoader sceneLoader;
    private readonly IGameStateMachine gameStateMachine;
    private readonly IGameFactory gameFactory;
    private readonly IUIFactory uiFactory;
    private readonly IStaticDataService staticData;
    private readonly IWaveServices waveServices;
    private readonly ILootService lootService;
    private readonly ILootSpawner lootSpawner;

    public LoadGameLevelState(ISceneLoader sceneLoader, IGameStateMachine gameStateMachine, IGameFactory gameFactory, IUIFactory uiFactory, IStaticDataService staticData, IWaveServices waveServices, ILootService lootService, ILootSpawner lootSpawner)
    {
      this.sceneLoader = sceneLoader;
      this.gameStateMachine = gameStateMachine;
      this.gameFactory = gameFactory;
      this.uiFactory = uiFactory;
      this.staticData = staticData;
      this.waveServices = waveServices;
      this.lootService = lootService;
      this.lootSpawner = lootSpawner;
    }


    public void Enter() => 
      sceneLoader.Load(Constants.GameScene, OnLoaded);

    public void Exit() { }

    private void OnLoaded()
    {
      InitGameWorld();
      gameStateMachine.Enter<GameLoopState>();
      
    }

    private void InitGameWorld()
    {
      InitUIRoot();
      LevelStaticData levelData = GetLevelData();
      InitSpawners(levelData.EnemySpawners, levelData.SpawnPointPrefab);
      InitWaves(levelData.LevelWaves);
      GameObject hero = gameFactory.CreateHero();
      InitHud(hero);
      CleanupLootSpawner();
      InitLootService(levelData.LevelKey);
      CameraFollow(hero);
    }

    private LevelStaticData GetLevelData()
    {
      string sceneKey = SceneManager.GetActiveScene().name;
      return staticData.ForLevel(sceneKey);
    }

    private void InitSpawners(List<EnemySpawnerStaticData> spawners, SpawnPoint pointPrefab) => 
      gameFactory.CreateEnemySpawnPoints(spawners, pointPrefab);

    private void CleanupLootSpawner() => 
      lootSpawner.Cleanup();

    private void InitWaves(LevelWaveStaticData waves) => 
      waveServices.SetLevelWaves(waves);

    private void InitHud(GameObject hero) => 
      gameFactory.CreateHud(hero);

    private void InitUIRoot() => 
      uiFactory.CreateUIRoot();

    private void InitLootService(string sceneName) => 
      lootService.SetSceneName(sceneName);

    private void CameraFollow(GameObject hero) =>
      Camera.main.GetComponent<CameraFollow>().Follow(hero);
  }
}