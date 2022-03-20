using Animations;
using Hero;
using StaticData.Hero.States;
using UnityEngine;

namespace StateMachines.Player
{
  public class HeroBaseImpactState : HeroBaseMachineState
  {
    private readonly float knockbackCooldown;
    private float lastImpactTime;

    protected HeroBaseImpactState(StateMachine stateMachine, string triggerName, BattleAnimator animator,
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
          ChangeState(hero.State<HeroShieldMoveState>());
        else
          ChangeState(hero.State<HeroIdleShieldState>());
      }
      else
      {
        if (IsStayVertical())
          ChangeState(hero.State<HeroIdleState>());
        else
          ChangeState(hero.State<HeroWalkState>());
      }
    }

    public bool IsKnockbackCooldown() => 
      Time.time >= lastImpactTime + knockbackCooldown;

    private void UpdateImpactTime() => 
      lastImpactTime = Time.time;
  }
}