using Animations;
using Hero;
using UnityEngine;

namespace StateMachines.Player
{
  public class PlayerIdleShieldState : PlayerBaseMachineState
  {
    private readonly int floatValueHash;
    private readonly HeroStateMachine hero;
    private readonly HeroRotate heroRotate;

    public PlayerIdleShieldState(StateMachine stateMachine, string animationName, string floatValueName,
      BattleAnimator animator, HeroStateMachine hero, HeroRotate heroRotate) : base(stateMachine, animationName, animator)
    {
      floatValueHash = Animator.StringToHash(floatValueName);
      this.hero = hero;
      this.heroRotate = heroRotate;
    }

    public override void LogicUpdate()
    {
      base.LogicUpdate();
      if (hero.IsBlockingPressed)
      {
        if (hero.MoveAxis != Vector2.zero)
          ChangeState(hero.ShieldMoveState);
        else
        if (Mathf.Approximately(hero.RotateAngle, 0) == false)
        {
          heroRotate.Rotate(hero.RotateAngle);
          SetFloat(floatValueHash, Mathf.Sign(hero.RotateAngle));
        }
        else
          SetFloat(floatValueHash, 0);
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