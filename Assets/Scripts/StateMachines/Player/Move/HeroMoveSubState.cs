using Animations;
using Hero;
using StateMachines.Player.AnimationStatesBehaviour;
using StateMachines.Player.Base;
using StaticData.Hero.States.Base;
using UnityEngine;

namespace StateMachines.Player.Move
{
  public abstract class HeroMoveSubState : HeroBaseSubStateMachineState<HeroMoveUpMachineState, HeroMoveSubState, HeroMoveStateData, MoveBehaviour>
  {
    public AnimationCurve EnterCurve => stateData.EnterCurve;
    public AnimationCurve ExitCurve => stateData.ExitCurve; 
    public AnimationCurve UpStateEnterCurve => stateData.UpStateEnterCurve;
    public AnimationCurve DownStateEnterCurve => stateData.DownStateEnterCurve;
    
    public HeroMoveSubState(HeroMoveUpMachineState upState, HeroStateMachine hero, BattleAnimator animator, string animationName, HeroMoveStateData stateData, MoveBehaviour behaviour) : base(upState, hero, animator, animationName, stateData, behaviour)
    {
    }
    
    protected Vector3 MoveAxis() => 
      new Vector3(hero.MoveAxis.x, 0 , hero.MoveAxis.y);
    
    protected bool IsLowAngle(Vector2 moveAxis, float maxAngle) => 
      Vector3.Angle(hero.transform.forward, new Vector3(moveAxis.x, 0, moveAxis.y)) < maxAngle;
  }
}