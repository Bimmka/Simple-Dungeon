using Animations;
using Hero;
using StaticData.Hero.States;
using UnityEngine;

namespace StateMachines.Player
{
  public class PlayerRollState : PlayerBaseMachineState
  {
    private readonly HeroMove heroMove;
    private readonly HeroStamina heroStamina;

    public override int Weight { get; }

    public PlayerRollState(StateMachine stateMachine, string triggerName, BattleAnimator animator,
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

    public override bool IsCanBeInterapted(int weight) =>
      false;

    public override void TriggerAnimation()
    {
      base.TriggerAnimation();
      if (hero.IsBlockingPressed)
      {
        if (IsStayHorizontal() == false)
          ChangeState(hero.State<PlayerShieldMoveState>());
        else
          ChangeState(hero.State<PlayerIdleShieldState>());
      }
      else
      {
        if (IsStayVertical())
          ChangeState(hero.State<PlayerIdleState>());
        else
          ChangeState(hero.State<PlayerWalkState>());
      }
    }

    public bool IsCanRoll() => 
      heroStamina.IsCanRoll();
  }
}