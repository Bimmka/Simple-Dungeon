using Animations;
using Hero;
using UnityEngine;

namespace StateMachines.Player
{
  public class PlayerMoveState : PlayerBaseMachineState
  {
    private readonly int floatValueHash;
    private readonly HeroMove heroMove;
    private readonly HeroRotate heroRotate;

    public PlayerMoveState(StateMachine stateMachine, string animationName, string floatValueName,
      BattleAnimator animator,
      HeroStateMachine hero, HeroMove heroMove, HeroRotate heroRotate) : base(stateMachine, animationName, animator, hero)
    {
      floatValueHash = Animator.StringToHash(floatValueName);
      this.heroMove = heroMove;
      this.heroRotate = heroRotate;
    }

    public override void Enter()
    {
      base.Enter();
      SetFloat(floatValueHash, 1f);
    }

    public override void LogicUpdate()
    {
      base.LogicUpdate(); 
      Debug.DrawRay(hero.transform.position, hero.transform.forward, Color.red);
      Debug.DrawRay(hero.transform.position, MoveAxis(), Color.green);
      if (IsNotMove())
        ChangeState(hero.State<PlayerIdleState>());
      else if (IsLowAngle(hero.MoveAxis) && heroRotate.IsTurning == false)
      {
        heroRotate.RotateTo(hero.MoveAxis);
        heroMove.Move(MoveAxis());
      }
      else if (heroRotate.IsTurning == false) 
        ChangeState(hero.State<PlayerRotatingState>());
    }

    private bool IsLowAngle(Vector2 moveAxis)
    {
      Debug.Log($"Signed Angle {Vector3.SignedAngle(hero.transform.forward, new Vector3(moveAxis.x, 0, moveAxis.y), Vector3.up)} Forward {hero.transform.forward} MoveAxis {moveAxis}");
      return Mathf.Abs(Vector3.SignedAngle(hero.transform.forward, new Vector3(moveAxis.x, 0, moveAxis.y), Vector3.up)) < 60;
    }

    public override void Exit()
    {
      base.Exit();
      SetFloat(floatValueHash, 0);
    }

    public override bool IsCanBeInterapted() => 
      true;

    private Vector3 MoveAxis() => 
      new Vector3(hero.MoveAxis.x, 0 , hero.MoveAxis.y);
  }
}