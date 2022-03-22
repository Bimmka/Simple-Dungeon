using Animations;
using Hero;
using StaticData.Hero.States;
using UnityEngine;

namespace StateMachines.Player
{
  public class HeroIdleState : HeroMoveSubState
  {
    public HeroIdleState(HeroMoveUpMachineState upState, HeroStateMachine hero,
      BattleAnimator animator, string triggerName, HeroStateData stateData) : base(upState, hero, animator, triggerName, stateData)
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