using Animations;
using Hero;
using StateMachines.Player.AnimationStatesBehaviour;
using StateMachines.Player.Attack;
using StaticData.Hero.Attacks;
using StaticData.Hero.States.Base;

namespace StateMachines.Player
{
  public class HeroAttackState : HeroAttackSubState
  {
    public HeroAttackState(HeroAttackUpMachineState upState, HeroStateMachine hero, BattleAnimator animator, string animationName, HeroBaseStateData stateData, AttackBehaviour behaviour, HeroAttack heroAttack, AttackStaticData attackData, HeroStamina heroStamina) : base(upState, hero, animator, animationName, stateData, behaviour, heroAttack, attackData, heroStamina)
    {
    }
  }
}