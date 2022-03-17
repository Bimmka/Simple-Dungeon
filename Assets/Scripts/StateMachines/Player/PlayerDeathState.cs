using Animations;
using Hero;
using StaticData.Hero.States;

namespace StateMachines.Player
{
  public class PlayerDeathState : PlayerBaseMachineState
  {
    public override int Weight { get; }

    public PlayerDeathState(StateMachine stateMachine, string triggerName, BattleAnimator animator, HeroStateMachine hero, HeroStateData stateData) : base(stateMachine, triggerName, animator, hero, stateData)
    {
      
    }

    public override bool IsCanBeInterapted(int weight) =>
      true;
  }
}