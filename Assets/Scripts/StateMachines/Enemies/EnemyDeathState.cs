using Animations;

namespace StateMachines.Enemies
{
  public class EnemyDeathState : EnemyBaseMachineState
  {
    public EnemyDeathState(StateMachine stateMachine, string animationName, BattleAnimator animator) : base(stateMachine, animationName, animator)
    {
    }

    public override bool IsCanBeInterapted()
    {
      return false;
    }
  }
}