using Systems.Healths;
using CodeBase.CameraLogic;
using ConstantsValue;
using Enemies;
using GameStates.States.Interfaces;
using SceneLoading;
using Services.Factories.GameFactories;
using Services.Progress;
using Services.StaticData;
using Services.UI.Factory;
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
    private readonly IPersistentProgressService progressService;
    private readonly IUIFactory uiFactory;
    private readonly IStaticDataService staticData;

    public LoadGameLevelState(ISceneLoader sceneLoader, IGameStateMachine gameStateMachine, IGameFactory gameFactory, IPersistentProgressService progressService, IUIFactory uiFactory, IStaticDataService staticData)
    {
      this.sceneLoader = sceneLoader;
      this.gameStateMachine = gameStateMachine;
      this.gameFactory = gameFactory;
      this.progressService = progressService;
      this.uiFactory = uiFactory;
      this.staticData = staticData;
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
      InitSpawners();
      GameObject hero = gameFactory.CreateHero();
      InitHud(hero);
      CameraFollow(hero);
    }

    private void InitSpawners()
    {
      string sceneKey = SceneManager.GetActiveScene().name;
      LevelStaticData levelData = staticData.ForLevel(sceneKey);
      
      gameFactory.CreateEnemySpawner(levelData.EnemySpawners, levelData.EnemySpawner, levelData.SpawnPointPrefab);
    }

    private void InitHud(GameObject hero) => 
      gameFactory.CreateHud(hero);

    private void InitUIRoot() => 
      uiFactory.CreateUIRoot();
    
    private void CameraFollow(GameObject hero) =>
      Camera.main.GetComponent<CameraFollow>().Follow(hero);
  }
}