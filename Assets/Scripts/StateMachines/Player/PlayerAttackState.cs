using Animations;

namespace StateMachines.Player
{
  public class PlayerAttackState : PlayerBaseMachineState
  {
    private float lastAttackTime;
    
    public PlayerAttackState(StateMachine stateMachine, string animationName, SimpleAnimator animator) : base(stateMachine, animationName, animator)
    {
    }

    public bool IsCanAttack()
    {
      return true;
    }
  }
}