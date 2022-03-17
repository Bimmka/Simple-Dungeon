using Animations;
using Hero;
using StaticData.Hero.States;

namespace StateMachines.Player
{
  public class PlayerHurtState : PlayerBaseImpactState
  {
    public PlayerHurtState(StateMachine stateMachine, string triggerName, BattleAnimator animator,
      HeroStateMachine hero, float cooldown, HeroStateData stateData) : base(stateMachine, triggerName, animator, hero, cooldown, stateData)
    {
    }
  }
}