using Animations;

namespace StateMachines.Player
{
  public class PlayerHurtState : PlayerBaseMachineState
  {
    public PlayerHurtState(StateMachine stateMachine, string animationName, SimpleAnimator animator) : base(stateMachine, animationName, animator)
    {
    }
  }
}