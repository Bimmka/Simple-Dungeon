using Hero;
using StateMachines.Enemies;
using StaticData.Enemies;
using UnityEngine;
using Utilities;

namespace Enemies
{
  public class EnemyStateMachine : BaseEntityStateMachine
  {
    [SerializeField] private EntitySearcher entitySearcher;
    [SerializeField] private EnemyMove move;
    [SerializeField] private EnemiesMoveStaticData moveData;
    
    public EnemyAttackState AttackState { get; private set; }
    public EnemyDeathState DeathState { get; private set; }
    public EnemyHurtState HurtState { get; private set; }
    public EnemyIdleState IdleState { get; private set; }
    public EnemyRunState RunState { get; private set; }
    public EnemySearchState SearchState { get; private set; }
    public EnemyWalkState WalkState { get; private set; }
    
    protected override void CreateStates()
    {
      AttackState = new EnemyAttackState(stateMachine, "IsSimpleAttack", simpleAnimator, this);
      DeathState = new EnemyDeathState(stateMachine, "IsDead", simpleAnimator);
      HurtState = new EnemyHurtState(stateMachine, "IsImpact", simpleAnimator);
      IdleState = new EnemyIdleState(stateMachine, "IsIdle", simpleAnimator, move, moveData, this);
      RunState = new EnemyRunState(stateMachine, "IsRun", simpleAnimator, move, moveData, this);
      SearchState = new EnemySearchState(stateMachine, "IsIdle", simpleAnimator, entitySearcher, move, this);
      WalkState = new EnemyWalkState(stateMachine, "IsWalk", simpleAnimator, move, moveData, this);
    }

    protected override void SetDefaultState() => 
      stateMachine.Initialize(SearchState);
  }
}