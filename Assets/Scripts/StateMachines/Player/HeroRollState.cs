using Animations;
using Hero;
using StaticData.Hero.States;
using UnityEngine;

namespace StateMachines.Player
{
  public class HeroRollState : HeroBaseMachineState
  {
    private readonly HeroMove heroMove;
    private readonly HeroStamina heroStamina;

    public override int Weight { get; }

    public HeroRollState(StateMachine stateMachine, string triggerName, BattleAnimator animator,
      HeroStateMachine hero, HeroMove heroMove, HeroStamina heroStamina, HeroStateData stateData) : base(stateMachine, triggerName, animator, hero, stateData)
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

    public override bool IsCanBeInterrupted(int weight) =>
      false;

    public override void TriggerAnimation()
    {
      base.TriggerAnimation();
      if (hero.IsBlockingPressed)
      {
        if (IsStayHorizontal() == false)
          ChangeState(hero.State<HeroShieldMoveState>());
        else
          ChangeState(hero.State<HeroIdleShieldState>());
      }
      else
      {
        if (IsStayVertical())
          ChangeState(hero.State<HeroIdleState>());
        else
          ChangeState(hero.State<HeroWalkState>());
      }
    }

    public bool IsCanRoll() => 
      heroStamina.IsCanRoll();
  }
}