using Animations;
using Hero;
using StateMachines.Player.AnimationStatesBehaviour;
using StaticData.Hero.States.Base;

namespace StateMachines.Player.Move
{
  public class HeroIdleState : HeroMoveSubState
  {
    public HeroIdleState(HeroMoveUpMachineState upState, HeroStateMachine hero,
      BattleAnimator animator, string triggerName, string speed, HeroMoveStateData stateData, MoveBehaviour behaviour) : base(upState, hero, animator, triggerName, speed, stateData, behaviour)
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