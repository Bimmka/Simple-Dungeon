using Animations;
using Hero;
using StateMachines.Player.AnimationStatesBehaviour;
using StateMachines.Player.Base;
using StaticData.Hero.States.Base;
using UnityEngine;

namespace StateMachines.Player.Move
{
  public abstract class HeroBaseMoveSubState : HeroBaseSubStateMachineState<HeroBaseMoveUpMachineState, HeroBaseMoveSubState, HeroMoveStateData, BaseMoveBehaviour>
  {
    public AnimationCurve EnterCurve => stateData.EnterCurve;
    public AnimationCurve ExitCurve => stateData.ExitCurve; 
    public AnimationCurve UpStateEnterCurve => stateData.UpStateEnterCurve;
    public AnimationCurve DownStateEnterCurve => stateData.DownStateEnterCurve;
    
    public HeroBaseMoveSubState(HeroBaseMoveUpMachineState upState, HeroStateMachine hero, BattleAnimator animator, string animationName, HeroMoveStateData stateData, BaseMoveBehaviour behaviour) : base(upState, hero, animator, animationName, stateData, behaviour)
    {
    }

    protected Vector3 MoveAxis() => 
      new Vector3(hero.MoveAxis.x, 0 , hero.MoveAxis.y);

    protected bool IsNeedTurnAround(Vector2 moveAxis, float maxAngle) =>
      upState.IsNeedTurnAround(moveAxis, maxAngle);
  }
}