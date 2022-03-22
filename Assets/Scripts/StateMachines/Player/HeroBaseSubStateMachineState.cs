using Animations;
using Hero;
using StaticData.Hero.States;
using UnityEngine;

namespace StateMachines.Player
{
  public class HeroBaseSubStateMachineState <TUpState, TDownState> : IHeroBaseSubStateMachineState where TUpState : HeroBaseUpMachineState<TDownState> where TDownState : IHeroBaseSubStateMachineState
  {
    protected readonly HeroStateMachine hero;
    protected readonly HeroStateData stateData;

    private TUpState _upState;
    
    private readonly BattleAnimator _animator;
    private readonly int _animationNameHash;
    
    public int Weight => stateData.Weight;

    public HeroBaseSubStateMachineState(TUpState upState, HeroStateMachine hero, BattleAnimator animator, string animationName, HeroStateData stateData)
    {
      this.hero = hero;
      this.stateData = stateData;
      _upState = upState;
      _animator = animator;
      _animationNameHash = Animator.StringToHash(animationName);
    }

    public bool IsCanBeInterrupted(int weight)
    {
      if (stateData.IsInteraptedBySameWeight)
        return weight >= Weight;
      return weight > Weight;
    }

    public virtual void Enter()
    {
      _animator.SetBool(_animationNameHash, true);
    }

    public virtual void LogicUpdate() { }
    public virtual void Interrupt() { }


    public virtual void Exit()
    {
      _animator.SetBool(_animationNameHash, false);
    }
    
    public virtual void AnimationTriggered() {}

    protected void ChangeState(IHeroBaseSubStateMachineState to) => 
      _upState.ChangeState(to);

    protected void InterruptState(IHeroBaseSubStateMachineState to)=>  
      _upState.InterruptState(to);

    protected void SetFloat(int hash, float value) => 
      _animator.SetFloat(hash, value);

    protected float ClipLength(PlayerActionsType actionsType) => 
      _upState.ClipLength(actionsType);
  }
}