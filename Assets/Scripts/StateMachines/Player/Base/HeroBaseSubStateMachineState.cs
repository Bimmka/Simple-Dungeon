using Animations;
using Hero;
using StaticData.Hero.States.Base;
using UnityEngine;

namespace StateMachines.Player.Base
{
  public class HeroBaseSubStateMachineState <TUpState, TDownState, TStateData> : IHeroBaseSubStateMachineState where TUpState : HeroBaseUpMachineState<TDownState> where TDownState : IHeroBaseSubStateMachineState where TStateData: HeroBaseStateData
  {
    protected readonly HeroStateMachine hero;
    protected readonly TStateData stateData;
    protected readonly TUpState upState;


    private readonly BattleAnimator _animator;
    private readonly int _animationNameHash;
    
    public int Weight => stateData.Weight;

    public HeroBaseSubStateMachineState(TUpState upState, HeroStateMachine hero, BattleAnimator animator, string animationName, TStateData stateData)
    {
      this.hero = hero;
      this.stateData = stateData;
      this.upState = upState;
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

    public virtual void Interrupt() => 
      _animator.SetBool(_animationNameHash, false);


    public virtual void Exit() => 
      _animator.SetBool(_animationNameHash, false);

    public virtual void AnimationTriggered() {}

    protected virtual void ChangeState(IHeroBaseSubStateMachineState to) => 
      upState.ChangeState(to);

    protected virtual void InterruptState(IHeroBaseSubStateMachineState to)=>  
      upState.InterruptState(to);

    protected void SetFloat(int hash, float value) => 
      _animator.SetFloat(hash, value);

    protected float ClipLength(PlayerActionsType actionsType) => 
      upState.ClipLength(actionsType);
  }
}