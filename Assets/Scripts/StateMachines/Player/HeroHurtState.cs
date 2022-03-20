using Animations;
using Hero;
using StaticData.Hero.States;

namespace StateMachines.Player
{
  public class HeroHurtState : HeroBaseImpactState
  {
    public HeroHurtState(StateMachine stateMachine, string triggerName, BattleAnimator animator,
      HeroStateMachine hero, float cooldown, HeroStateData stateData) : base(stateMachine, triggerName, animator, hero, cooldown, stateData)
    {
    }
  }
}