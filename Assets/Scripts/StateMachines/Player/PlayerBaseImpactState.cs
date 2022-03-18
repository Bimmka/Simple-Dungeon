using Animations;
using Hero;
using StaticData.Hero.States;
using UnityEngine;

namespace StateMachines.Player
{
  public class PlayerBaseImpactState : PlayerBaseMachineState
  {
    private readonly float knockbackCooldown;
    private float lastImpactTime;

    public override int Weight { get; }

    protected PlayerBaseImpactState(StateMachine stateMachine, string triggerName, BattleAnimator animator,
      HeroStateMachine hero, float cooldown, HeroStateData stateData) : base(stateMachine, triggerName, animator, hero, stateData)
    {
      knockbackCooldown = cooldown;
      UpdateImpactTime();
    }

    public override bool IsCanBeInterrupted(int weight) =>
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
          ChangeState(hero.State<PlayerWalkState>());
      }
    }

    public bool IsKnockbackCooldown() => 
      Time.time >= lastImpactTime + knockbackCooldown;

    private void UpdateImpactTime() => 
      lastImpactTime = Time.time;
  }
}