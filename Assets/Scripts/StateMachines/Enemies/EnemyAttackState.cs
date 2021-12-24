using Animations;
using Enemies;
using UnityEngine;

namespace StateMachines.Enemies
{
  public class EnemyAttackState : EnemyBaseMachineState
  {
    private readonly EnemyStateMachine enemy;
    private float lastAttackTime;
    private float attackCooldown = 2f;

    public EnemyAttackState(StateMachine stateMachine, string animationName, SimpleAnimator animator, EnemyStateMachine enemy) : base(stateMachine, animationName, animator)
    {
      this.enemy = enemy;
      UpdateAttackTime();
    }

    public override bool IsCanBeInterapted()
    {
      return true;
    }

    public override void Enter()
    {
      base.Enter();
      UpdateAttackTime();
    }

    public override void AnimationTrigger()
    {
      base.AnimationTrigger();
      ChangeState(enemy.IdleState);
    }

    public bool IsCanAttack() =>
      Time.time >= lastAttackTime + attackCooldown;

    private void UpdateAttackTime() => 
      lastAttackTime = Time.time;
  }
}