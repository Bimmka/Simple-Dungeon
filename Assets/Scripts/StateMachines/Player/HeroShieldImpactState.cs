using Animations;
using Hero;
using StaticData.Hero.States;

namespace StateMachines.Player
{
  public class HeroShieldImpactState : HeroBaseImpactState
  {
    public HeroShieldImpactState(StateMachine stateMachine, string triggerName, BattleAnimator animator,
      HeroStateMachine hero, float cooldown, HeroStateData stateData) : base(stateMachine, triggerName, animator, hero, cooldown, stateData)
    {
    }
  }
}