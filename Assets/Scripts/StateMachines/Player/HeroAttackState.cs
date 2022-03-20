using Animations;
using Hero;
using StaticData.Hero.Components;
using StaticData.Hero.States;
using UnityEngine;

namespace StateMachines.Player
{
  public class HeroAttackState : HeroBaseMachineState
  {
    private readonly HeroAttack heroAttack;
    private readonly HeroStamina heroStamina;
    private readonly float attackCooldown;
    
    private float lastAttackTime;

    private bool isAttackEnded;

    public HeroAttackState(StateMachine stateMachine, string triggerName, BattleAnimator animator,
      HeroStateMachine hero, HeroAttack heroAttack, HeroAttackStaticData attackData, HeroStamina heroStamina, HeroStateData stateData) : base(stateMachine, triggerName, animator, hero, stateData)
    {
      this.heroAttack = heroAttack;
      this.heroStamina = heroStamina;
      this.animator.Attacked += Attack;
      attackCooldown = attackData.AttackCooldown;
      UpdateAttackTime();
    }

    public void Cleanup()
    {
      animator.Attacked -= Attack;
    }

    public bool IsCanAttack() => 
      Time.time >= lastAttackTime + attackCooldown && heroStamina.IsCanAttack();

    public override void Enter()
    {
      base.Enter();
      isAttackEnded = false;
    }

    public override bool IsCanBeInterrupted(int weight) =>
      base.IsCanBeInterrupted(weight) && isAttackEnded;

    public override void TriggerAnimation()
    {
      base.TriggerAnimation();
      isAttackEnded = true;
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

    private void Attack()
    {
      heroAttack.Attack();
      heroStamina.WasteToAttack();
    }

    private void UpdateAttackTime() => 
      lastAttackTime = Time.time;
  }
}