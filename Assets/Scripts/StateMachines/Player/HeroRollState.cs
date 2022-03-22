﻿using Animations;
using Hero;
using StaticData.Hero.States;
using UnityEngine;

namespace StateMachines.Player
{
  public class HeroRollState : HeroBaseSubStateMachineState<HeroRollUpMachineState, HeroRollState>
  {
    private readonly HeroMove heroMove;
    private readonly HeroStamina heroStamina;
    public HeroRollState(HeroRollUpMachineState upState, HeroStateMachine hero, BattleAnimator animator,
      string triggerName, HeroStateData stateData, HeroMove heroMove, HeroStamina heroStamina) : base(upState, hero, animator, triggerName, stateData)
    {
      this.heroMove = heroMove;
      this.heroStamina = heroStamina;
    }

    public override void Enter()
    {
      base.Enter();
      heroStamina.WasteToRoll();
    }

    public override void LogicUpdate()
    {
      base.LogicUpdate();
      heroMove.Roll();
    }

    public override void AnimationTriggered()
    {
      base.AnimationTriggered();
      if (hero.IsBlockingPressed)
      {
       /* if (hero.IsStayHorizontal() == false)
          ChangeState(hero.State<HeroShieldMoveState>());
        else
          ChangeState(hero.State<HeroIdleShieldState>());*/
      }
      else
      {
        if (hero.IsNotMove())
          ChangeState(hero.State<HeroIdleState>());
        else
          ChangeState(hero.State<HeroWalkState>());
      }
    }

    public bool IsCanRoll() => 
      heroStamina.IsCanRoll();
  }
}