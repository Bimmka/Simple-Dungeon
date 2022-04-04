using Animations;
using Hero;
using StateMachines.Player.AnimationStatesBehaviour;
using StaticData.Hero.States.Base;
using UnityEngine;

namespace StateMachines.Player.Base
{
  public class HeroBaseSubStateMachineState <TUpState, TDownState, TStateData, TStateBehaviour> : IHeroBaseSubStateMachineState where TUpState : HeroBaseUpMachineState<TDownState> where TDownState : IHeroBaseSubStateMachineState where TStateData: HeroBaseStateData where TStateBehaviour: BaseStateBehaviour
  {
    protected readonly HeroStateMachine hero;
    protected readonly TStateData stateData;
    protected readonly TUpState upState;
    protected readonly TStateBehaviour behaviour;
    protected readonly BattleAnimator animator;

    private readonly int _animationNameHash;
    
    public int Weight => stateData.Weight;
    public bool IsAnimationInit => behaviour.IsPlaying;

    public HeroBaseSubStateMachineState(TUpState upState, HeroStateMachine hero, BattleAnimator animator, string animationName, TStateData stateData, TStateBehaviour behaviour)
    {
      this.hero = hero;
      this.stateData = stateData;
      this.upState = upState;
      this.behaviour = behaviour;
      this.animator = animator;
      _animationNameHash = Animator.StringToHash(animationName);
    }

    public virtual bool IsCanBeInterrupted(int weight)
    {
      if (stateData.IsInteraptedBySameWeight)
        return weight >= Weight;
      return weight > Weight;
    }

    public virtual void Enter() => 
      animator.SetBool(_animationNameHash, true);

    public virtual void LogicUpdate() { }

    public virtual void Interrupt() => 
      animator.SetBool(_animationNameHash, false);


    public virtual void Exit() => 
      animator.SetBool(_animationNameHash, false);

    public virtual void AnimationTriggered() {}

    protected virtual void ChangeState(IHeroBaseSubStateMachineState to) => 
      upState.ChangeState(to);

    protected virtual void InterruptState(IHeroBaseSubStateMachineState to)=>  
      upState.InterruptState(to);

    protected void SetFloat(int hash, float value) => 
      animator.SetFloat(hash, value);

    protected float ClipLength(PlayerActionsType actionsType) => 
      upState.ClipLength(actionsType);
  }
}