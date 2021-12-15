using Animations;

namespace StateMachines.Player
{
  public class PlayerIdleShieldState : PlayerBaseMachineState
  {
    public PlayerIdleShieldState(StateMachine stateMachine, string animationName, SimpleAnimator animator) : base(stateMachine, animationName, animator)
    {
    }

    public override bool IsCanBeInterapted() => 
      true;
  }
}