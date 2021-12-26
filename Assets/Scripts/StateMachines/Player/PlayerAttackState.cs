using Animations;
using Hero;
using StaticData.Hero.Components;
using UnityEngine;

namespace StateMachines.Player
{
  public class PlayerAttackState : PlayerBaseMachineState
  {
    private readonly HeroStateMachine hero;
    private readonly HeroAttack heroAttack;
    private readonly float attackCooldown;
    
    private float lastAttackTime;

    private bool isAttackEnded;

    public PlayerAttackState(StateMachine stateMachine, string animationName, BattleAnimator animator,
      HeroStateMachine hero, HeroAttack heroAttack, HeroAttackStaticData attackData) : base(stateMachine, animationName, animator)
    {
      this.hero = hero;
      this.heroAttack = heroAttack;
      this.animator.Attacked += Attack;
      attackCooldown = attackData.AttackCooldown;
      UpdateAttackTime();
    }

    public void Cleanup()
    {
      animator.Attacked -= Attack;
    }

    public bool IsCanAttack() => 
      Time.time >= lastAttackTime + attackCooldown;

    public override bool IsCanBeInterapted() => 
      isAttackEnded;

    public override void TriggerAnimation()
    {
      base.TriggerAnimation();
      isAttackEnded = true;
      if (hero.IsBlockingPressed)
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

    private void Attack()
    {
      heroAttack.Attack();
      isAttackEnded = false;
    }

    private void UpdateAttackTime() => 
      lastAttackTime = Time.time;
  }
}