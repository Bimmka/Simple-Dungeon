using Animations;
using Hero;
using UnityEngine;

namespace StateMachines.Player
{
  public class PlayerIdleShieldState : PlayerBaseMachineState
  {

    public PlayerIdleShieldState(StateMachine stateMachine, string animationName,
      BattleAnimator animator, HeroStateMachine hero) : base(stateMachine, animationName, animator, hero)
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
          ChangeState(hero.State<PlayerMoveState>());
      }
    }

    public override bool IsCanBeInterapted() => 
      true;

  }
}