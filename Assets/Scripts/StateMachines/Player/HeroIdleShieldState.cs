using Animations;
using Hero;
using StateMachines.Player.AnimationStatesBehaviour;
using StateMachines.Player.Move;
using StaticData.Hero.States.Base;

namespace StateMachines.Player
{
  public class HeroIdleShieldState : HeroShieldMoveSubState
  {
    public HeroIdleShieldState(HeroShieldMoveUpMachineState upState, HeroStateMachine hero,
      BattleAnimator animator, string triggerName, HeroMoveStateData stateData, ShieldMoveBehaviour behaviour) : base(upState, hero, animator, triggerName, stateData, behaviour)
    {
      
    }

    public override void LogicUpdate()
    {
      base.LogicUpdate();
      if (hero.IsNotMove() == false)
      {
        if (hero.IsBlockingPressed)
          ChangeState(hero.State<HeroShieldMoveState>());
        else if (hero.IsRunningPressed)
          ChangeState(hero.State<HeroRunState>());
        else
          ChangeState(hero.State<HeroWalkState>());
      }
      else if (hero.IsBlockingPressed == false)
        ChangeState(hero.State<HeroIdleState>());
    }
  }
}