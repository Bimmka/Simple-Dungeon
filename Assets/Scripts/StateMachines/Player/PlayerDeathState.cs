﻿using Animations;

namespace StateMachines.Player
{
  public class PlayerDeathState : PlayerBaseMachineState
  {
    public PlayerDeathState(StateMachine stateMachine, string animationName, SimpleAnimator animator) : base(stateMachine, animationName, animator)
    {
      
    }

    public override bool IsCanBeInterapted() => 
      true;
  }
}