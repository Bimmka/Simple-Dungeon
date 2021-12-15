using Animations;

namespace StateMachines.Player
{
  public class PlayerShieldImpactState : PlayerBaseMachineState
  {
    public PlayerShieldImpactState(StateMachine stateMachine, string animationName, SimpleAnimator animator) : base(stateMachine, animationName, animator)
    {
    }

    public override bool IsCanBeInterapted() => 
      true;
  }
}