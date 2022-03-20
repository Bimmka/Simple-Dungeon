using Animations;
using Hero;
using StaticData.Hero.States;

namespace StateMachines.Player
{
  public class HeroDeathState : HeroBaseMachineState
  {
    public HeroDeathState(StateMachine stateMachine, string triggerName, BattleAnimator animator, HeroStateMachine hero, HeroStateData stateData) : base(stateMachine, triggerName, animator, hero, stateData)
    {
      
    }

    public override bool IsCanBeInterrupted(int weight) =>
      false;
  }
}