using Animations;

namespace StateMachines.Player
{
  public class PlayerRollState : PlayerBaseMachineState
  {
    public PlayerRollState(StateMachine stateMachine, string animationName, SimpleAnimator animator) : base(stateMachine, animationName, animator)
    {
    }
  }
}