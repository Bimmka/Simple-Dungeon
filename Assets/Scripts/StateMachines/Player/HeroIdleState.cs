using Animations;
using Hero;
using StaticData.Hero.States;
using UnityEngine;

namespace StateMachines.Player
{
  public class HeroIdleState : HeroBaseMachineState
  {
    public HeroIdleState(StateMachine stateMachine, string triggerName,
      BattleAnimator animator, HeroStateMachine hero, HeroStateData stateData) : base(stateMachine, triggerName, animator, hero, stateData)
    {
    }

    public override void LogicUpdate()
    {
      base.LogicUpdate();
      if (IsNotMove() == false)
      {
        if (hero.IsRunningPressed && hero.State<HeroRunState>().IsCanRun())
          ChangeState(hero.State<HeroRunState>());
        else
          ChangeState(hero.State<HeroWalkState>());
      }
    }
  }
}