using Animations;

namespace StateMachines.Player
{
  public class PlayerIdleState : PlayerBaseMachineState
  {
    public PlayerIdleState(StateMachine stateMachine, string animationName, SimpleAnimator animator) : base(stateMachine, animationName, animator)
    {
    }
  }
}