using Animations;
using Hero;
using UnityEngine;

namespace StateMachines.Player
{
  public class PlayerMoveState : PlayerBaseMachineState
  {
    private readonly int floatValueHash;
    private readonly HeroStateMachine hero;
    private readonly HeroMove heroMove;

    public PlayerMoveState(StateMachine stateMachine, string animationName, string floatValueName,
      SimpleAnimator animator,
      HeroStateMachine hero, HeroMove heroMove) : base(stateMachine, animationName, animator)
    {
      floatValueHash = Animator.StringToHash(floatValueName);
      this.hero = hero;
      this.heroMove = heroMove;
    }

    public override void Enter()
    {
      base.Enter();
      SetFloat(floatValueHash, hero.MoveAxis.y);
    }

    public override void LogicUpdate()
    {
      base.LogicUpdate();
      if (hero.IsBlocking)
      {
        if (Mathf.Approximately(hero.MoveAxis.x, 0) == false)
          ChangeState(hero.ShieldMoveState);
        else
          ChangeState(hero.IdleShieldState);
      }
      else if (Mathf.Approximately(hero.MoveAxis.y, 0))
        ChangeState(hero.IdleState);
      else
        heroMove.Move(Vector3.forward * hero.MoveAxis.y);     
    }

    public override void Exit()
    {
      base.Exit();
      SetFloat(floatValueHash, 0);
    }

    public override bool IsCanBeInterapted() => 
      true;
  }
}