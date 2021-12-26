using Animations;
using Hero;
using UnityEngine;

namespace StateMachines.Player
{
  public class PlayerBaseImpactState : PlayerBaseMachineState
  {
    private readonly HeroStateMachine hero;
    private readonly float knockbackCooldown;
    private float lastImpactTime;

    protected PlayerBaseImpactState(StateMachine stateMachine, string animationName, BattleAnimator animator,
      HeroStateMachine hero, float cooldown) : base(stateMachine, animationName, animator)
    {
      this.hero = hero;
      knockbackCooldown = cooldown;
      UpdateImpactTime();
    }

    public override bool IsCanBeInterapted() => 
      false;

    public override void Enter()
    {
      base.Enter();
      UpdateImpactTime();
    }

    public override void TriggerAnimation()
    {
      base.TriggerAnimation();

      if (hero.IsBlockingPressed)
      {
        if (IsMoveHorizontal())
          ChangeState(hero.ShieldMoveState);
        else
          ChangeState(hero.IdleShieldState);
      }
      else
      {
        if (IsVerticalStay())
          ChangeState(hero.IdleState);
        else
          ChangeState(hero.MoveState);
      }
    }

    public bool IsKnockbackCooldown() => 
      Time.time >= lastImpactTime + knockbackCooldown;

    private void UpdateImpactTime() => 
      lastImpactTime = Time.time;

    private bool IsMoveHorizontal() => 
      hero.MoveAxis.x != 0;

    private bool IsVerticalStay() => 
      Mathf.Approximately(hero.MoveAxis.y,0);
  }
}