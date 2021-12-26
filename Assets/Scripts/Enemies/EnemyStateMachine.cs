using Animations;
using Hero;
using StateMachines.Enemies;
using StaticData.Enemies;
using UnityEngine;
using Utilities;

namespace Enemies
{
  public class EnemyStateMachine : BaseEntityStateMachine
  {
    [SerializeField] protected BattleAnimator battleAnimator;
    [SerializeField] private EntitySearcher entitySearcher;
    [SerializeField] private EnemyMove move;
    [SerializeField] private EnemyRotate rotate;
    [SerializeField] private EnemyAttack attack;
    [SerializeField] private EnemiesMoveStaticData moveData;
    [SerializeField] private EnemyAttackData attackData;
    
    public EnemyAttackState AttackState { get; private set; }
    public EnemyDeathState DeathState { get; private set; }
    public EnemyHurtState ImpactState { get; private set; }
    public EnemyIdleState IdleState { get; private set; }
    public EnemyRunState RunState { get; private set; }
    public EnemySearchState SearchState { get; private set; }
    public EnemyWalkState WalkState { get; private set; }

    protected override void Subscribe()
    {
      base.Subscribe();
      battleAnimator.Triggered += AnimationTriggered;
    }

    protected override void Cleanup()
    {
      base.Cleanup();
      battleAnimator.Triggered -= AnimationTriggered;
      AttackState.Cleanup();
    }

    protected override void CreateStates()
    {
      AttackState = new EnemyAttackState(stateMachine, "IsSimpleAttack", battleAnimator, this, attack, attackData);
      DeathState = new EnemyDeathState(stateMachine, "IsDead", battleAnimator);
      ImpactState = new EnemyHurtState(stateMachine, "IsImpact", battleAnimator, this);
      IdleState = new EnemyIdleState(stateMachine, "IsIdle", battleAnimator, move, moveData, this, rotate);
      RunState = new EnemyRunState(stateMachine, "IsRun", battleAnimator, move, moveData, this);
      SearchState = new EnemySearchState(stateMachine, "IsIdle", battleAnimator, entitySearcher, move, this);
      WalkState = new EnemyWalkState(stateMachine, "IsWalk", battleAnimator, move, moveData, this);
    }

    protected override void SetDefaultState() => 
      stateMachine.Initialize(SearchState);

    public void Impact()
    {
      if (stateMachine.State.IsCanBeInterapted())
        stateMachine.ChangeState(ImpactState);
    }
  }
}