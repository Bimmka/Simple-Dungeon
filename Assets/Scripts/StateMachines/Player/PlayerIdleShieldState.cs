using Animations;
using Hero;
using StaticData.Hero.States;

namespace StateMachines.Player
{
  public class PlayerIdleShieldState : PlayerBaseMachineState
  {
    public override int Weight { get; }

    public PlayerIdleShieldState(StateMachine stateMachine, string triggerName,
      BattleAnimator animator, HeroStateMachine hero, HeroStateData stateData) : base(stateMachine, triggerName, animator, hero, stateData)
    {
    }

    public override void LogicUpdate()
    {
      base.LogicUpdate();
      if (hero.IsBlockingPressed)
      {
        if (IsStayHorizontal() == false)
          ChangeState(hero.State<PlayerShieldMoveState>());
      }
      else
      {
        if (IsStayVertical())
          ChangeState(hero.State<PlayerIdleState>());
        else
          ChangeState(hero.State<PlayerWalkState>());
      }
    }

    public override bool IsCanBeInterrupted(int weight) =>
      true;
  }
}