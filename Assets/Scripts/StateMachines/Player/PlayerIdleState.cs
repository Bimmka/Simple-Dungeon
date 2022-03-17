using Animations;
using Hero;
using StaticData.Hero.States;
using UnityEngine;

namespace StateMachines.Player
{
  public class PlayerIdleState : PlayerBaseMachineState
  {
    public override int Weight { get; }

    public PlayerIdleState(StateMachine stateMachine, string triggerName,
      BattleAnimator animator, HeroStateMachine hero, HeroStateData stateData) : base(stateMachine, triggerName, animator, hero, stateData)
    {
    }

    public override void LogicUpdate()
    {
      base.LogicUpdate();
      if (IsNotMove() == false)
          ChangeState(hero.State<PlayerWalkState>());
       
    }

    public override bool IsCanBeInterapted(int weight) =>
      true;
  }
}