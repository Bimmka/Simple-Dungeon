using Animations;
using Hero;
using UnityEngine;

namespace StateMachines.Player
{
  public class PlayerShieldImpactState : PlayerBaseMachineState
  {
    private readonly HeroStateMachine hero;

    public PlayerShieldImpactState(StateMachine stateMachine, string animationName, SimpleAnimator animator, HeroStateMachine hero) : base(stateMachine, animationName, animator)
    {
      this.hero = hero;
    }

    public override bool IsCanBeInterapted() => 
      true;

    public override void AnimationTrigger()
    {
      base.AnimationTrigger();
      if (hero.IsBlocking)
      {
        if (hero.MoveAxis != Vector2.zero)
          ChangeState(hero.ShieldMoveState);
        else
          ChangeState(hero.IdleShieldState);
      }
      else
      {
        if (hero.MoveAxis == Vector2.zero)
          ChangeState(hero.IdleState);
        else
          ChangeState(hero.MoveState);
      }
    }
  }
}