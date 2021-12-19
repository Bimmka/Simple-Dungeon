using Animations;
using Hero;
using UnityEngine;

namespace StateMachines.Player
{
  public class PlayerIdleShieldState : PlayerBaseMachineState
  {
    private readonly int floatValueHash;
    private readonly HeroStateMachine hero;

    public PlayerIdleShieldState(StateMachine stateMachine, string animationName, string floatValueName,
      SimpleAnimator animator, HeroStateMachine hero) : base(stateMachine, animationName, animator)
    {
      floatValueHash = Animator.StringToHash(floatValueName);
      this.hero = hero;
    }

    public override void LogicUpdate()
    {
      base.LogicUpdate();
      if (hero.IsBlocking)
      {
        if (hero.MoveAxis != Vector2.zero)
          ChangeState(hero.ShieldMoveState);
        else
          SetFloat(floatValueHash, hero.MouseRotation);
      }
      else
      {
        if (hero.MoveAxis == Vector2.zero)
          ChangeState(hero.IdleState);
        else
          ChangeState(hero.MoveState);
      }
    }

    public override bool IsCanBeInterapted() => 
      true;
  }
}