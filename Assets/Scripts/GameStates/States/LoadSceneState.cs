using System;
using GameStates.States.Interfaces;
using SceneLoading;

namespace GameStates.States
{
  public class LoadSceneState : IPayloadedCallbackState<string, Action>
  {
    private readonly ISceneLoader sceneLoader;
    public LoadSceneState(ISceneLoader sceneLoader)
    {
      this.sceneLoader = sceneLoader;
    }


    public void Enter(string payload)
    {
      sceneLoader.Load(payload);
    }

    public void Enter(string payload, Action loadedCallback, Action curtainCallback)
    {
      sceneLoader.Load(payload, loadedCallback, curtainCallback);
    }

    public void Exit()
    {
      
    }
  }
}