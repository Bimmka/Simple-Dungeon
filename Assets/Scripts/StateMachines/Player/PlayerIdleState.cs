using Animations;
using Hero;
using UnityEngine;

namespace StateMachines.Player
{
  public class PlayerIdleState : PlayerBaseMachineState
  {
    public override int Weight { get; }

    public PlayerIdleState(StateMachine stateMachine, string animationName,
      BattleAnimator animator, HeroStateMachine hero) : base(stateMachine, animationName, animator, hero)
    {
    }

    public override void LogicUpdate()
    {
      base.LogicUpdate();
      if (IsNotMove() == false)
          ChangeState(hero.State<PlayerMoveState>());
       
    }

    public override bool IsCanBeInterapted(int weight) =>
      true;
  }
}