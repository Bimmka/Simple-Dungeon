using Animations;
using Hero;
using StateMachines.Player.AnimationStatesBehaviour;
using StateMachines.Player.Base;
using StaticData.Hero.States.Base;
using UnityEngine;

namespace StateMachines.Player.Move
{
  public abstract class HeroMoveSubState : HeroBaseMoveSubState
  {
    public HeroMoveSubState(HeroMoveUpMachineState upState, HeroStateMachine hero, BattleAnimator animator, string animationName, HeroMoveStateData stateData, MoveBehaviour behaviour) : base(upState, hero, animator, animationName, stateData, behaviour)
    {
    }
  }
}