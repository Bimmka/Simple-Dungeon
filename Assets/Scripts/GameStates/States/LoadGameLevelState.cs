using System.Collections.Generic;
using Systems.Healths;
using CodeBase.CameraLogic;
using ConstantsValue;
using Enemies;
using Enemies.Spawn;
using GameStates.States.Interfaces;
using SceneLoading;
using Services.Factories.GameFactories;
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

    public LoadGameLevelState(ISceneLoader sceneLoader, IGameStateMachine gameStateMachine, IGameFactory gameFactory, IUIFactory uiFactory, IStaticDataService staticData, IWaveServices waveServices)
    {
      this.sceneLoader = sceneLoader;
      this.gameStateMachine = gameStateMachine;
      this.gameFactory = gameFactory;
      this.uiFactory = uiFactory;
      this.staticData = staticData;
      this.waveServices = waveServices;
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
      CameraFollow(hero);
    }

    private LevelStaticData GetLevelData()
    {
      string sceneKey = SceneManager.GetActiveScene().name;
      return staticData.ForLevel(sceneKey);
    }

    private void InitSpawners(List<EnemySpawnerStaticData> spawners, SpawnPoint pointPrefab) => 
      gameFactory.CreateEnemySpawnPoints(spawners, pointPrefab);

    private void InitWaves(LevelWaveStaticData waves) => 
      waveServices.SetLevelWaves(waves);

    private void InitHud(GameObject hero) => 
      gameFactory.CreateHud(hero);

    private void InitUIRoot() => 
      uiFactory.CreateUIRoot();
    
    private void CameraFollow(GameObject hero) =>
      Camera.main.GetComponent<CameraFollow>().Follow(hero);
  }
}