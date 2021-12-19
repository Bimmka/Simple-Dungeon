using Animations;
using Hero;
using UnityEngine;

namespace StateMachines.Player
{
  public class PlayerRollState : PlayerBaseMachineState
  {
    private readonly HeroStateMachine hero;
    private readonly HeroMove heroMove;

    public PlayerRollState(StateMachine stateMachine, string animationName, SimpleAnimator animator,
      HeroStateMachine hero, HeroMove heroMove) : base(stateMachine, animationName, animator)
    {
      this.hero = hero;
      this.heroMove = heroMove;
    }

    public override void LogicUpdate()
    {
      base.LogicUpdate();
      heroMove.Roll();
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