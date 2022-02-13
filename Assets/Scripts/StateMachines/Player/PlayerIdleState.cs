using Animations;
using Hero;
using UnityEngine;

namespace StateMachines.Player
{
  public class PlayerIdleState : PlayerBaseMachineState
  {
    private readonly int floatValueHash;
    private readonly HeroRotate heroRotate;

    public PlayerIdleState(StateMachine stateMachine, string animationName, string floatValueName,
      BattleAnimator animator, HeroStateMachine hero, HeroRotate heroRotate) : base(stateMachine, animationName, animator, hero)
    {
      floatValueHash = Animator.StringToHash(floatValueName);
      this.heroRotate = heroRotate;
    }

    public override void LogicUpdate()
    {
      base.LogicUpdate();
      if (hero.IsBlockingPressed)
      {
        if (IsStayHorizontal() == false)
          ChangeState(hero.State<PlayerShieldMoveState>());
        else
          ChangeState(hero.State<PlayerIdleState>());
      }
      else
        if (IsStayVertical() == false)
          ChangeState(hero.State<PlayerMoveState>());
        else
        {
          if (Mathf.Approximately(hero.RotateAngle, 0) == false)
          {
            heroRotate.Rotate(hero.RotateAngle);
            SetFloat(floatValueHash, Mathf.Sign(hero.RotateAngle));
          }
          else
            SetFloat(floatValueHash, 0);
        }
    }

    public override bool IsCanBeInterapted() => 
      true;
  }
}