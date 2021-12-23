using GameStates.States.Interfaces;
using SceneLoading;
using Services.Progress;
using Services.SaveLoad;

namespace GameStates.States
{
  public class LoadProgressState : IState
  {
    private readonly IGameStateMachine gameStateMachine;
    private readonly ISceneLoader sceneLoader;
    private readonly IPersistentProgressService progressService;
    private readonly ISaveLoadService saveLoadProgress;
 
    public LoadProgressState(IGameStateMachine gameStateMachine, ISceneLoader sceneLoader, IPersistentProgressService progressService, ISaveLoadService saveLoadProgress)
    {
      this.gameStateMachine = gameStateMachine;
      this.sceneLoader = sceneLoader;
      this.progressService = progressService;
      this.saveLoadProgress = saveLoadProgress;
    }

    public void Enter()
    {
      LoadData();
      gameStateMachine.Enter<LoadGameLevelState>();
    }

    public void Exit()
    {
      
    }

    private void LoadData()
    {
      
    }
  }
}