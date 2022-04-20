using Animations;
using Hero;
using StateMachines.Player.AnimationStatesBehaviour;
using StaticData.Hero.States.Base;

namespace StateMachines.Player.Move
{
  public class HeroIdleState : HeroMoveSubState
  {
    public HeroIdleState(HeroMoveUpMachineState upState, HeroStateMachine hero,
      BattleAnimator animator, string triggerName, HeroMoveStateData stateData, MoveBehaviour behaviour) : base(upState, hero, animator, triggerName, stateData, behaviour)
    {
    }

    public override void LogicUpdate()
    {
      base.LogicUpdate();
      if (hero.IsBlockingPressed)
      {
        if (hero.IsNotMove())
          ChangeState(hero.State<HeroIdleShieldState>());
        else
          ChangeState(hero.State<HeroShieldMoveState>());
      }
      else if (hero.IsNotMove() == false)
      {
        if (hero.IsRunningPressed && hero.State<HeroRunState>().IsCanRun())
          ChangeState(hero.State<HeroRunState>());
        else
          ChangeState(hero.State<HeroWalkState>());
      }
    }
  }
}