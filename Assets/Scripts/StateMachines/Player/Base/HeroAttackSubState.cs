using Animations;
using Hero;
using StateMachines.Player.AnimationStatesBehaviour;
using StaticData.Hero.States.Base;

namespace StateMachines.Player.Base
{
  public class HeroAttackSubState : HeroBaseSubStateMachineState<HeroAttackUpMachineState, HeroAttackSubState, HeroBaseStateData, AttackBehaviour>
  {
    public HeroAttackSubState(HeroAttackUpMachineState upState, HeroStateMachine hero, BattleAnimator animator, string animationName, HeroBaseStateData stateData, AttackBehaviour behaviour) : base(upState, hero, animator, animationName, stateData, behaviour)
    {
    }
  }
}