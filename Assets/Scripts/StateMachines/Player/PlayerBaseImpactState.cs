using Animations;
using Hero;
using UnityEngine;

namespace StateMachines.Player
{
  public class PlayerBaseImpactState : PlayerBaseMachineState
  {
    private readonly float knockbackCooldown;
    private float lastImpactTime;

    public override int Weight { get; }

    protected PlayerBaseImpactState(StateMachine stateMachine, string animationName, BattleAnimator animator,
      HeroStateMachine hero, float cooldown) : base(stateMachine, animationName, animator, hero)
    {
      knockbackCooldown = cooldown;
      UpdateImpactTime();
    }

    public override bool IsCanBeInterapted(int weight) =>
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
        if (IsStayHorizontal() == false)
          ChangeState(hero.State<PlayerShieldMoveState>());
        else
          ChangeState(hero.State<PlayerIdleShieldState>());
      }
      else
      {
        if (IsStayVertical())
          ChangeState(hero.State<PlayerIdleState>());
        else
          ChangeState(hero.State<PlayerMoveState>());
      }
    }

    public bool IsKnockbackCooldown() => 
      Time.time >= lastImpactTime + knockbackCooldown;

    private void UpdateImpactTime() => 
      lastImpactTime = Time.time;
  }
}