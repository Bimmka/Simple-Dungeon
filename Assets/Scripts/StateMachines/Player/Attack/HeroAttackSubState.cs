using Animations;
using Hero;
using StateMachines.Player.AnimationStatesBehaviour;
using StateMachines.Player.Base;
using StateMachines.Player.Move;
using StaticData.Hero.Attacks;
using StaticData.Hero.States.Base;
using UnityEngine;

namespace StateMachines.Player.Attack
{
  public class HeroAttackSubState : HeroBaseSubStateMachineState<HeroAttackUpMachineState, HeroAttackSubState, HeroBaseStateData, BaseAttackBehaviour>
  {
    private readonly HeroAttack _heroAttack;
    private readonly HeroStamina _heroStamina;

    private readonly AttackStaticData _attackData;
    
    private float _lastAttackTime;
    private readonly float attackCooldown;

    private bool isAttackEnded;
    
    public HeroAttackSubState(HeroAttackUpMachineState upState, HeroStateMachine hero, BattleAnimator animator, string animationName, HeroBaseStateData stateData, BaseAttackBehaviour behaviour, HeroAttack heroAttack, AttackStaticData attackData, HeroStamina heroStamina) : base(upState, hero, animator, animationName, stateData, behaviour)
    {
      _heroAttack = heroAttack;
      _heroStamina = heroStamina;
      this.animator.Attacked += Attack;
      _attackData = attackData;
      attackCooldown = attackData.AttackCooldown;
      UpdateAttackTime();
    }
    
    public bool IsCanAttack() => 
      Time.time >= _lastAttackTime + attackCooldown && _heroStamina.IsCanAttack(_attackData.Cost);
    
    public void Cleanup()
    {
      animator.Attacked -= Attack;
    }
    
    public override void Enter()
    {
      base.Enter();
      isAttackEnded = false;
    }

    public override bool IsCanBeInterrupted(int weight) =>
      base.IsCanBeInterrupted(weight) && behaviour.IsCanBeInterrupted;

    public override void Exit()
    {
      base.Exit();
      hero.FinishAttack();
    }

    public override void Interrupt()
    {
      base.Interrupt();
      hero.FinishAttack();
    }

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
      if (IsAnimationInit == false)
        return;
      
      _heroAttack.Attack(_attackData.Attack);
      _heroStamina.WasteToAttack(_attackData.Cost);
    }

    private void UpdateAttackTime() => 
      _lastAttackTime = Time.time;
  }
}