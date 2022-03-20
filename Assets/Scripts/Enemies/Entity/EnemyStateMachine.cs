using Systems.Healths;
using Animations;
using Hero;
using StateMachines.Enemies;
using StaticData.Enemies;
using UnityEngine;
using Utilities;

namespace Enemies.Entity
{
  public class EnemyStateMachine : MonoBehaviour
  {
    [SerializeField] protected BattleAnimator battleAnimator;
    [SerializeField] private EntitySearcher entitySearcher;
    [SerializeField] private EnemyMove move;
    [SerializeField] private EnemyRotate rotate;
    [SerializeField] private EnemyAttack attack;
    [SerializeField] private EnemyDeath death;
    
    private EnemiesMoveStaticData moveData;
    private EnemyAttackStaticData attackData;
    private float damageCoeff;

    private IHealth health;
    
    public EnemyAttackState AttackState { get; private set; }
    public EnemyDeathState DeathState { get; private set; }
    public EnemyHurtState ImpactState { get; private set; }
    public EnemyIdleState IdleState { get; private set; }
    public EnemyRunState RunState { get; private set; }
    public EnemySearchState SearchState { get; private set; }
    public EnemyWalkState WalkState { get; private set; }

    public void Construct(EnemiesMoveStaticData moveData, EnemyAttackStaticData attackData, float damageCoeff, IHealth health)
    {
      this.moveData = moveData;
      this.attackData = attackData;
      this.damageCoeff = damageCoeff;
      this.health = health;
      this.health.Dead += Dead;
      attack.Construct(this.attackData);
    }

    protected void Subscribe()
    {
   
      battleAnimator.Triggered += AnimationTriggered;
      death.Revived += Revive;
    }

    private void AnimationTriggered()
    {
      
    }

    protected void Cleanup()
    {
      
      battleAnimator.Triggered -= AnimationTriggered;
      health.Dead -= Dead;
      death.Revived -= Revive;
      AttackState.Cleanup();
    }
    
    

    public void Impact()
    {
     
    }

    public void UpdateDamageCoeff(float coeff)
    {
      damageCoeff = coeff;
      AttackState.UpdateDamageCoeff(damageCoeff);
    }

    private void Dead()
    {
      
    }

    private void Revive()
    {
      
    }
  }
}