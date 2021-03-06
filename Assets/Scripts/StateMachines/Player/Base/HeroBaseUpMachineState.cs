using System;
using System.Collections.Generic;
using Hero;
using Services;
using UnityEngine;

namespace StateMachines.Player.Base
{
  public abstract class HeroBaseUpMachineState<TSubState> : IHeroBaseUpMachineState where TSubState : IHeroBaseSubStateMachineState
  {
    protected Coroutine _changeCoroutine;
    
    protected readonly HeroStateMachine hero;
    protected readonly ICoroutineRunner _coroutineRunner;

    protected TSubState currentState;
    
    private readonly StateMachineWithSubstates _stateMachine;
    private readonly Dictionary<Type, TSubState> _subStates = new Dictionary<Type, TSubState>(10);

    public bool IsAnimationInit => currentState.IsAnimationInit;

    protected HeroBaseUpMachineState(StateMachineWithSubstates stateMachine,HeroStateMachine hero, ICoroutineRunner coroutineRunner)
    {
      _stateMachine = stateMachine;
      _coroutineRunner = coroutineRunner;
      this.hero = hero;
    }

    public void AddSubstate(IHeroBaseSubStateMachineState state)
    {
      if (_subStates.ContainsKey(state.GetType()))
        return;
      
      _subStates.Add(state.GetType(), (TSubState) state);
    }

    public virtual void Exit()
    {
      currentState.Exit();
    }

    public void Initialize(IHeroBaseSubStateMachineState state) => 
      SetNewSubstate(state);

    public virtual void LogicUpdate() => 
      currentState.LogicUpdate();

    public virtual void AnimationTriggered() => 
      currentState.AnimationTriggered();

    public bool IsCanBeInterrupted(int weight) => 
      currentState.IsCanBeInterrupted(weight);


    public void ChangeState(IHeroBaseSubStateMachineState to)
    {
      UpdateState(to, ExitCurrentState);
    }

    public void InterruptState(IHeroBaseSubStateMachineState to)
    {
      UpdateState(to, InterruptState);
    }

    public float ClipLength(PlayerActionsType actionsType) => 
      hero.ClipLength(actionsType);

    public void InterruptState() => 
      currentState.Interrupt();

    public bool IsSameState(IHeroBaseSubStateMachineState state) => 
      currentState as IHeroBaseSubStateMachineState == state;

    protected virtual void SetNewSubstate(IHeroBaseSubStateMachineState to)
    {
      currentState = (TSubState) to;
      currentState.Enter();
    }

    protected TSubState SubState<TState>()
    {
      return _subStates[typeof(TState)];
    }

    private void UpdateState(IHeroBaseSubStateMachineState to, Action actionWithPreviousState)
    {
      if (_subStates.ContainsKey(to.GetType()))
      {
        actionWithPreviousState?.Invoke();
        SetNewSubstate(to);
      }
      else
        _stateMachine.ChangeState(hero.GetUpStateForSubstate(to), to);
    }
    private void ExitCurrentState() => 
      currentState.Exit();
  }
}