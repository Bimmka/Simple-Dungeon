using Animations;

namespace StateMachines.Enemies
{
  public class EnemyPatrolState : EnemyBaseMachineState
  {
    public override int Weight { get; }

    public EnemyPatrolState(StateMachine stateMachine, string animationName, BattleAnimator animator) : base(stateMachine, animationName, animator)
    {
    }

    public override bool IsCanBeInterrupted(int weight) =>
      true;
  }
}