using Animations;
using Hero;
using StateMachines.Player.AnimationStatesBehaviour;
using StateMachines.Player.Base;
using StateMachines.Player.Move;
using StaticData.Hero.Components;
using StaticData.Hero.States.Base;
using UnityEngine;

namespace StateMachines.Player
{
  public class HeroAttackState : HeroAttackSubState
  {
    private readonly HeroAttack heroAttack;
    private readonly HeroStamina heroStamina;
    private readonly float attackCooldown;
    
    private float lastAttackTime;

    private bool isAttackEnded;

    public HeroAttackState(HeroAttackUpMachineState upState, HeroStateMachine hero, BattleAnimator animator, string animationName, HeroBaseStateData stateData, AttackBehaviour behaviour, HeroAttack heroAttack, HeroAttackStaticData attackData, HeroStamina heroStamina) : base(upState, hero, animator, animationName, stateData, behaviour)
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
      base.IsCanBeInterrupted(weight) && behaviour.IsCanBeInterrupted;

    public override void AnimationTriggered()
    {
      base.AnimationTriggered();
      isAttackEnded = true;
      {
        if (hero.IsStayVertical())
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