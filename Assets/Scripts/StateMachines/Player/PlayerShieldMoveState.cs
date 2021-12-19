using Animations;
using Hero;
using UnityEngine;

namespace StateMachines.Player
{
  public class PlayerShieldMoveState : PlayerBaseMachineState
  {
    private readonly int floatValueHash;
    private readonly HeroStateMachine hero;
    private readonly HeroMove heroMove;

    public PlayerShieldMoveState(StateMachine stateMachine, string animationName, string floatValueName,
      SimpleAnimator animator, HeroStateMachine hero, HeroMove heroMove) : base(stateMachine, animationName, animator)
    {
      floatValueHash = Animator.StringToHash(floatValueName);
      this.hero = hero;
      this.heroMove = heroMove;
    }

    public override void Enter()
    {
      base.Enter();
      SetFloat(floatValueHash, hero.MoveAxis.x);
    }

    public override void LogicUpdate()
    {
      base.LogicUpdate();
      if (hero.IsBlocking == false)
      {
        if (Mathf.Approximately(hero.MoveAxis.y, 0) == false)
          ChangeState(hero.MoveState);
        else
          ChangeState(hero.IdleState);
      }
      else if (Mathf.Approximately(hero.MoveAxis.x, 0))
        ChangeState(hero.IdleShieldState);
      else
          heroMove.Move(Vector3.right * hero.MoveAxis.x);        
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