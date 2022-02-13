using Animations;
using Hero;
using UnityEngine;

namespace StateMachines.Player
{
  public class PlayerRollState : PlayerBaseMachineState
  {
    private readonly HeroMove heroMove;
    private readonly HeroStamina heroStamina;

    public PlayerRollState(StateMachine stateMachine, string animationName, BattleAnimator animator,
      HeroStateMachine hero, HeroMove heroMove, HeroStamina heroStamina) : base(stateMachine, animationName, animator, hero)
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

    public override bool IsCanBeInterapted() => 
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
          ChangeState(hero.State<PlayerMoveState>());
      }
    }

    public bool IsCanRoll() => 
      heroStamina.IsCanRoll();
  }
}