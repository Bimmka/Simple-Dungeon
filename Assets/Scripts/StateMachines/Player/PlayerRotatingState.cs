using System;
using Animations;
using Hero;
using StaticData.Hero.States;
using UnityEngine;

namespace StateMachines.Player
{
  public class PlayerRotatingState : PlayerBaseMachineState
  {
    private readonly HeroRotate _rotate;
    private readonly AnimatorClipsContainer _clipsContainer;
    private readonly int _moveXHash;
    private readonly int _moveYHash;

    private Vector2 rotateDirection;

    public override int Weight { get; }

    public PlayerRotatingState(StateMachine stateMachine, string triggerName, BattleAnimator animator,
      HeroStateMachine hero, string moveXName, string moveYName, HeroRotate rotate, HeroStateData stateData) : base(stateMachine, triggerName, animator, hero, stateData)
    {
      _rotate = rotate;
      _moveXHash = Animator.StringToHash(moveXName);
      _moveYHash = Animator.StringToHash(moveYName);
    }

    public override bool IsCanBeInterapted(int weight) => 
      true;

    public override void Enter()
    {
      base.Enter();
      if (_rotate.IsTurning == false)
      {
        _rotate.SetIsTurning();
        SetTurn();
      }
    }

    public override void LogicUpdate()
    {
      base.LogicUpdate();
      if (_rotate.IsTurning) 
        UpdateAnimatorHash();
    }

    public override void Exit()
    {
      base.Exit();
      if (_rotate.IsTurning)
        _rotate.StopRotate();
      animator.SetFloat(_moveXHash, 0);
      animator.SetFloat(_moveYHash, 0);
    }

    private void UpdateAnimatorHash()
    {
      if (rotateDirection.x != 0)
        rotateDirection.x = Mathf.Sign(rotateDirection.x) * (Mathf.Abs(rotateDirection.x) - Time.deltaTime);
      
      if (rotateDirection.y != 0)
        rotateDirection.y = Mathf.Sign(rotateDirection.y) * (Mathf.Abs(rotateDirection.y) - Time.deltaTime);
      
      animator.SetFloat(_moveYHash, rotateDirection.y);
      animator.SetFloat(_moveXHash, rotateDirection.x);
    }

    private void SetTurn()
    {
      float angleBetweenDirection = Vector3.SignedAngle(hero.transform.forward, new Vector3(hero.MoveAxis.x, 0, hero.MoveAxis.y), Vector3.up);
      if (Math.Abs(Mathf.Abs(angleBetweenDirection) - 180) < 0.1f)
        SetTurnAround();
      else
        SetHalfTurn(Math.Sign(angleBetweenDirection));
    }

    private void SetTurnAround()
    {
      rotateDirection = Vector2.up;
      animator.SetFloat(_moveYHash, 1);
      _rotate.TurnAround(new Vector3(hero.MoveAxis.x, 0, hero.MoveAxis.y), _clipsContainer.ClipLength(PlayerActionsType.TurnAround),OnTurnEnd);
    }

    private void SetHalfTurn(int sign)
    {
      rotateDirection = Vector2.right * sign;
      animator.SetFloat(_moveXHash, sign);
      _rotate.Turn
      (
        new Vector3(hero.MoveAxis.x, 0, hero.MoveAxis.y),
        sign == -1 ? _clipsContainer.ClipLength(PlayerActionsType.TurnLeft) : _clipsContainer.ClipLength(PlayerActionsType.TurnRight),
        OnTurnEnd
        );
    }

    private void OnTurnEnd()
    {
      if (IsNotMove())
        ChangeState(hero.State<PlayerIdleState>());
      else
        ChangeState(hero.State<PlayerWalkState>());
    }
  }
}