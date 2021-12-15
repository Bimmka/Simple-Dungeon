using Animations;
using Hero;
using UnityEngine;

namespace StateMachines.Player
{
  public class PlayerMoveState : PlayerBaseMachineState
  {
    private readonly HeroStateMachine hero;

    public PlayerMoveState(StateMachine stateMachine, string animationName, SimpleAnimator animator, HeroStateMachine hero) : base(stateMachine, animationName, animator)
    {
      this.hero = hero;
    }

    public override void LogicUpdate()
    {
      base.LogicUpdate();
      if (hero.MoveAxis == Vector2.zero)
        ChangeState(hero.IdleState);
    }

    public override bool IsCanBeInterapted() => 
      true;
  }
}