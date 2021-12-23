using CodeBase.CameraLogic;
using ConstantsValue;
using GameStates.States.Interfaces;
using SceneLoading;
using Services.Factories.GameFactories;
using Services.Progress;
using Services.StaticData;
using Services.UI.Factory;
using UnityEngine;

namespace GameStates.States
{
  public class LoadGameLevelState : IState
  {
    private readonly ISceneLoader sceneLoader;
    private readonly IGameStateMachine gameStateMachine;
    private readonly IGameFactory gameFactory;
    private readonly IPersistentProgressService progressService;
    private readonly IUIFactory uiFactory;

    public LoadGameLevelState(ISceneLoader sceneLoader, IGameStateMachine gameStateMachine, IGameFactory gameFactory, IPersistentProgressService progressService, IUIFactory uiFactory)
    {
      this.sceneLoader = sceneLoader;
      this.gameStateMachine = gameStateMachine;
      this.gameFactory = gameFactory;
      this.progressService = progressService;
      this.uiFactory = uiFactory;
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
      GameObject hero = gameFactory.CreateHero();
      //InitHud(hero);
      CameraFollow(hero);
    }

    private void InitHud(GameObject hero)
    {
      GameObject hud = gameFactory.CreateHud();
    }

    private void InitUIRoot() => 
      uiFactory.CreateUIRoot();
    
    private void CameraFollow(GameObject hero) =>
      Camera.main.GetComponent<CameraFollow>().Follow(hero);
  }
}