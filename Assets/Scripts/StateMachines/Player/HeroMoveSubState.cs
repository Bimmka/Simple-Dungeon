using Animations;
using Hero;
using StaticData.Hero.States;

namespace StateMachines.Player
{
  public class HeroMoveSubState : HeroBaseSubStateMachineState<HeroMoveUpMachineState, HeroMoveSubState>
  {
    public HeroMoveSubState(HeroMoveUpMachineState upState, HeroStateMachine hero, BattleAnimator animator, string animationName, HeroStateData stateData) : base(upState, hero, animator, animationName, stateData)
    {
    }
  }
}