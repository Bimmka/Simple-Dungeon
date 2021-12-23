using System;
using System.Collections.Generic;
using GameStates.States;
using GameStates.States.Interfaces;
using SceneLoading;
using Services;
using Services.Factories.GameFactories;
using Services.Progress;
using Services.SaveLoad;
using Services.UI.Factory;

namespace GameStates
{
  public class GameStateMachine : IGameStateMachine
  {
    private readonly Dictionary<Type, IExitableState> _states;
    private IExitableState _activeState;

    public GameStateMachine(ISceneLoader sceneLoader, ref AllServices services)
    {
      _states = new Dictionary<Type, IExitableState>
      {
        [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader,ref services),
        [typeof(LoadProgressState)] = new LoadProgressState(this, sceneLoader, services.Single<IPersistentProgressService>(), services.Single<ISaveLoadService>()),
        [typeof(GameLoopState)] = new GameLoopState(this),
        [typeof(LoadGameLevelState)] = new LoadGameLevelState(sceneLoader, this, services.Single<IGameFactory>(), services.Single<IPersistentProgressService>(), services.Single<IUIFactory>())
      };
    }
    
    public void Enter<TState>() where TState : class, IState
    {
      IState state = ChangeState<TState>();
      state.Enter();
    }

    public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
    {
      TState state = ChangeState<TState>();
      state.Enter(payload);
    }

    public void Enter<TState, TPayload, TCallback>(TPayload payload, TCallback loadedCallback, TCallback curtainHideCallback) where TState : class, IPayloadedCallbackState<TPayload, TCallback>
    {
      TState state = ChangeState<TState>();
      state.Enter(payload, loadedCallback, curtainHideCallback);
    }

    private TState ChangeState<TState>() where TState : class, IExitableState
    {
      _activeState?.Exit();
      
      TState state = GetState<TState>();
      _activeState = state;
      
      return state;
    }

    private TState GetState<TState>() where TState : class, IExitableState => 
      _states[typeof(TState)] as TState;
  }
}