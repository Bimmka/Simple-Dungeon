using Animations;
using Hero;
using StaticData.Hero.States;
using UnityEngine;

namespace StateMachines.Player
{
  public abstract class HeroMoveSubState : HeroBaseSubStateMachineState<HeroMoveUpMachineState, HeroMoveSubState>
  {
    public AnimationCurve EnterCurve => stateData.EnterCurve;
    public AnimationCurve ExitCurve => stateData.ExitCurve;
    
    public HeroMoveSubState(HeroMoveUpMachineState upState, HeroStateMachine hero, BattleAnimator animator, string animationName, HeroStateData stateData) : base(upState, hero, animator, animationName, stateData)
    {
    }
    
    protected Vector3 MoveAxis() => 
      new Vector3(hero.MoveAxis.x, 0 , hero.MoveAxis.y);
    
    protected bool IsLowAngle(Vector2 moveAxis, float maxAngle) => 
      Vector3.Angle(hero.transform.forward, new Vector3(moveAxis.x, 0, moveAxis.y)) < maxAngle;
  }
}