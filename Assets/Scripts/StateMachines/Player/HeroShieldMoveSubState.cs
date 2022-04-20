using Animations;
using Hero;
using StateMachines.Player.AnimationStatesBehaviour;
using StateMachines.Player.Move;
using StaticData.Hero.States.Base;

namespace StateMachines.Player
{
  public class HeroShieldMoveSubState : HeroBaseMoveSubState
  {
    public HeroShieldMoveSubState(HeroShieldMoveUpMachineState upState, HeroStateMachine hero, BattleAnimator animator, string animationName, HeroMoveStateData stateData, ShieldMoveBehaviour behaviour) : base(upState, hero, animator, animationName, stateData, behaviour)
    {
    }
  }
}