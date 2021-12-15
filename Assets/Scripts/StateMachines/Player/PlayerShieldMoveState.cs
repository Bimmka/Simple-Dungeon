using Animations;

namespace StateMachines.Player
{
  public class PlayerShieldMoveState : PlayerBaseMachineState
  {
    public PlayerShieldMoveState(StateMachine stateMachine, string animationName, SimpleAnimator animator) : base(stateMachine, animationName, animator)
    {
    }

    public override bool IsCanBeInterapted() => 
      true;
  }
}