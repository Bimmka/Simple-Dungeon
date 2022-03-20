using Animations;
using Hero;
using StaticData.Hero.States;

namespace StateMachines.Player
{
  public class HeroIdleShieldState : HeroBaseMachineState
  {
    public HeroIdleShieldState(StateMachine stateMachine, string triggerName,
      BattleAnimator animator, HeroStateMachine hero, HeroStateData stateData) : base(stateMachine, triggerName, animator, hero, stateData)
    {
    }

    public override void LogicUpdate()
    {
      base.LogicUpdate();
      if (hero.IsBlockingPressed)
      {
        if (IsStayHorizontal() == false)
          ChangeState(hero.State<HeroShieldMoveState>());
      }
      else
      {
        if (IsStayVertical())
          ChangeState(hero.State<HeroIdleState>());
        else
          ChangeState(hero.State<HeroWalkState>());
      }
    }
  }
}