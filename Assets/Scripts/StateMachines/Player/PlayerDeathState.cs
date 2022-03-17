﻿using Animations;
using Hero;

namespace StateMachines.Player
{
  public class PlayerDeathState : PlayerBaseMachineState
  {
    public override int Weight { get; }

    public PlayerDeathState(StateMachine stateMachine, string animationName, BattleAnimator animator, HeroStateMachine hero) : base(stateMachine, animationName, animator, hero)
    {
      
    }

    public override bool IsCanBeInterapted(int weight) =>
      true;
  }
}