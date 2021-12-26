using Animations;
using Enemies;
using StaticData.Enemies;
using UnityEngine;

namespace StateMachines.Enemies
{
  public class EnemyAttackState : EnemyBaseMachineState
  {
    private readonly EnemyStateMachine enemy;
    private readonly EnemyAttack enemyAttack;
    private readonly float attackCooldown;
    
    private float lastAttackTime;

    public EnemyAttackState(StateMachine stateMachine, string animationName, BattleAnimator animator,
      EnemyStateMachine enemy, EnemyAttack enemyAttack, EnemyAttackData attackData) : base(stateMachine, animationName, animator)
    {
      this.enemy = enemy;
      this.enemyAttack = enemyAttack;
      attackCooldown = attackData.AttackCooldown;
      UpdateAttackTime();
      this.animator.Attacked += Attack;
    }

    public void Cleanup()
    {
     animator.Attacked -= Attack;
    }

    public override bool IsCanBeInterapted() => 
      true;

    public override void Enter()
    {
      base.Enter();
      UpdateAttackTime();
    }

    public override void TriggerAnimation()
    {
      base.TriggerAnimation();
      ChangeState(enemy.IdleState);
    }

    public bool IsCanAttack() =>
      Time.time >= lastAttackTime + attackCooldown;

    private void Attack() => 
      enemyAttack.Attack();

    private void UpdateAttackTime() => 
      lastAttackTime = Time.time;
  }
}