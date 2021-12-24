using Animations;

namespace StateMachines.Enemies
{
  public class EnemyHurtState : EnemyBaseMachineState
  {
    public EnemyHurtState(StateMachine stateMachine, string animationName, SimpleAnimator animator) : base(stateMachine, animationName, animator)
    {
    }

    public override bool IsCanBeInterapted()
    {
      return false;
    }
  }
}