using System;
using Animations;
using Hero;
using Services;
using UnityEngine;

namespace StateMachines.Player
{
  public class PlayerRotatingState : PlayerBaseMachineState
  {
    private readonly HeroRotate _rotate;
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly int moveXHash;
    private readonly int moveYHash;

    private Vector2 rotateDirection;

    public PlayerRotatingState(StateMachine stateMachine, string animationName, BattleAnimator animator,
      HeroStateMachine hero, string moveXName, string moveYName, HeroRotate rotate, ICoroutineRunner coroutineRunner) : base(stateMachine, animationName, animator, hero)
    {
      _rotate = rotate;
      _coroutineRunner = coroutineRunner;
      moveXHash = Animator.StringToHash(moveXName);
      moveYHash = Animator.StringToHash(moveYName);
    }

    public override bool IsCanBeInterapted() => 
      true;

    public override void Enter()
    {
      base.Enter();
      if (_rotate.IsTurning == false)
      {
        _rotate.SetIsTurning();
        SetAnimatorHash();
      }
    }

    public override void LogicUpdate()
    {
      base.LogicUpdate();
      if (_rotate.IsTurning)
      {
        UpdateAnimatorHash();
        _rotate.Rotate();
      }
    }

    public override void TriggerAnimation()
    {
      base.TriggerAnimation();
      _rotate.StopRotate();
      if (IsNotMove())
        ChangeState(hero.State<PlayerIdleState>());
      else
        ChangeState(hero.State<PlayerMoveState>());
    }

    public override void Exit()
    {
      base.Exit();
      if (_rotate.IsTurning)
        _rotate.StopRotate();
      animator.SetFloat(moveXHash, 0);
      animator.SetFloat(moveYHash, 0);
    }

    private void UpdateAnimatorHash()
    {
      if (rotateDirection.x != 0)
        rotateDirection.x = Mathf.Sign(rotateDirection.x) * (Mathf.Abs(rotateDirection.x) - Time.deltaTime);
      
      if (rotateDirection.y != 0)
        rotateDirection.y = Mathf.Sign(rotateDirection.y) * (Mathf.Abs(rotateDirection.y) - Time.deltaTime);
      
      animator.SetFloat(moveYHash, rotateDirection.y);
      animator.SetFloat(moveXHash, rotateDirection.x);
    }

    private void SetAnimatorHash()
    {
      float angleBetweenDirection = Vector3.SignedAngle(hero.transform.forward, new Vector3(hero.MoveAxis.x, 0, hero.MoveAxis.y), Vector3.up);
      if (Math.Abs(Mathf.Abs(angleBetweenDirection) - 180) < 0.1f)
        SetBehindTurn();
      else
        SetHalfTurn(Math.Sign(angleBetweenDirection));
    }

    private void SetBehindTurn()
    {
      rotateDirection = Vector2.up;
      animator.SetFloat(moveYHash, 1);
    }

    private void SetHalfTurn(int sign)
    {
      rotateDirection = Vector2.right * sign;
      animator.SetFloat(moveXHash, sign);
    }
  }
}