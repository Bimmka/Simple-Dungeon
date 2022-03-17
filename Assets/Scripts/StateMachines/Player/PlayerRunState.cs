using Animations;
using Hero;
using StaticData.Hero.States;

namespace StateMachines.Player
{
  public class PlayerRunState : PlayerBaseMachineState
  {
    public override int Weight { get; }

    public PlayerRunState(StateMachine stateMachine, string triggerName, string floatValue, BattleAnimator animator, HeroStateMachine hero, HeroStateData stateData) : base(stateMachine, triggerName, animator, hero, stateData)
    {
    }

    public override bool IsCanBeInterapted(int weight)
    {
      return true;
    }
  }
}