using Animations;
using Hero;
using StateMachines.Player.AnimationStatesBehaviour;
using StaticData.Hero.States.Base;
using UnityEngine;

namespace StateMachines.Player.Move
{
  public abstract class HeroMoveSubState : HeroBaseMoveSubState
  {
    private readonly int speedTriggerID;
    
    
    public HeroMoveSubState(HeroMoveUpMachineState upState, HeroStateMachine hero, BattleAnimator animator, string animationName, string speedName, HeroMoveStateData stateData, MoveBehaviour behaviour) : base(upState, hero, animator, animationName, stateData, behaviour)
    {
      speedTriggerID = Animator.StringToHash(speedName);
    }

    protected void SetTriggerSpeedValue(float speed) => 
      animator.SetFloat(speedTriggerID, speed);
  }
}