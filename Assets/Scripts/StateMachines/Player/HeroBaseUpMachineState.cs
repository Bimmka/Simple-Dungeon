using System;
using System.Collections.Generic;
using Hero;
using Services;
using UnityEngine;

namespace StateMachines.Player
{
  public abstract class HeroBaseUpMachineState<TSubState> : IHeroBaseUpMachineState where TSubState : IHeroBaseSubStateMachineState
  {
    protected Coroutine _changeCoroutine;
    
    protected readonly HeroStateMachine hero;
    protected readonly ICoroutineRunner _coroutineRunner;
    
    private TSubState currentState;
    
    private readonly StateMachineWithSubstates _stateMachine;
    private readonly Dictionary<Type, TSubState> _subStates = new Dictionary<Type, TSubState>(10);

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

    public virtual void Exit() { }

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
      currentState.Exit();

      UpdateState(to);
    }

    public void InterruptState(IHeroBaseSubStateMachineState to)
    {
      InterruptState();
      
      UpdateState(to);
    }

    public float ClipLength(PlayerActionsType actionsType) => 
      hero.ClipLength(actionsType);

    public void InterruptState() => 
      currentState.Interrupt();

    public bool IsSameState(IHeroBaseSubStateMachineState state) => 
      false;

    private void UpdateState(IHeroBaseSubStateMachineState to)
    {
      if (_subStates.ContainsKey(to.GetType()))
        SetNewSubstate(to);
      else
        _stateMachine.ChangeState(hero.GetUpStateForSubstate(to), to);
    }

    private void SetNewSubstate(IHeroBaseSubStateMachineState to)
    {
      currentState = (TSubState) to;
      currentState.Enter();
    }
  }
}