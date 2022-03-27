using Animations;
using Hero;
using StaticData.Hero.States.Base;

namespace StateMachines.Player.Move
{
  public class HeroIdleState : HeroMoveSubState
  {
    public HeroIdleState(HeroMoveUpMachineState upState, HeroStateMachine hero,
      BattleAnimator animator, string triggerName, HeroMoveStateData stateData) : base(upState, hero, animator, triggerName, stateData)
    {
    }

    public override void LogicUpdate()
    {
      base.LogicUpdate();
      if (hero.IsNotMove() == false)
      {
        if (hero.IsRunningPressed && hero.State<HeroRunState>().IsCanRun())
          ChangeState(hero.State<HeroRunState>());
        else
          ChangeState(hero.State<HeroWalkState>());
      }
    }
  }
}