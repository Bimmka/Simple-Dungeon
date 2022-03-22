using Animations;
using Hero;
using StaticData.Hero.States;

namespace StateMachines.Player
{
  public abstract class HeroRotatingSubState : HeroBaseSubStateMachineState<HeroRotatingUpMachineState, HeroRotatingSubState>
  {
    public HeroRotatingSubState(HeroRotatingUpMachineState _upState, HeroStateMachine hero, BattleAnimator animator, string animationName, HeroStateData stateData) : base(_upState, hero, animator, animationName, stateData)
    {
    }
  }
}