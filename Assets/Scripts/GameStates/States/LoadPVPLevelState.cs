using GameStates.States.Interfaces;
using SceneLoading;
using Services.UI.Factory;
using Services.UI.Windows;

namespace GameStates.States
{
  public class LoadPVPLevelState : IPayloadedState<string>
  {
    private readonly GameStateMachine gameStateMachine;
    private readonly ISceneLoader sceneLoader;
    private readonly IWindowsService windowsService;
    private readonly UIFactory uiFactory;


    public LoadPVPLevelState(GameStateMachine gameStateMachine, ISceneLoader sceneLoader, IWindowsService windowsService, UIFactory uiFactory)
    {
      this.gameStateMachine = gameStateMachine;
      this.sceneLoader = sceneLoader;
      this.windowsService = windowsService;
      this.uiFactory = uiFactory;
    }

    public void Enter(string sceneName)
    {
      sceneLoader.Load(sceneName, OnLoaded);
    }

    public void Exit()
    {
      
    }

    private void OnLoaded()
    {
      
    }
    
  }
}