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
    private readonly int _moveXHash;
    private readonly int _moveYHash;

    private Coroutine _turnCoroutine;
    private Coroutine _turnAroundCoroutine;

    public override int Weight { get; }

    public PlayerRotatingState(StateMachine stateMachine, string triggerName, BattleAnimator animator,
      HeroStateMachine hero, string moveXName, string moveYName, HeroRotate rotate, HeroStateData stateData) : base(stateMachine, triggerName, animator, hero, stateData)
    {
      _rotate = rotate;
      _moveXHash = Animator.StringToHash(moveXName);
      _moveYHash = Animator.StringToHash(moveYName);
    }

    public override bool IsCanBeInterrupted(int weight) => 
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

    public override void Exit()
    {
      base.Exit();
      if (_rotate.IsTurning)
        _rotate.StopRotate();
      SmoothChange(ref _turnCoroutine, hero, _moveXHash,  stateData.ExitCurve);
      SmoothChange(ref _turnAroundCoroutine, hero, _moveYHash,  stateData.ExitCurve);
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
      SmoothChange(ref _turnAroundCoroutine, hero, _moveYHash, stateData.EnterCurve);
      _rotate.TurnAround
      (
        new Vector3(hero.MoveAxis.x, 0, hero.MoveAxis.y), 
        ClipLength(PlayerActionsType.TurnAround),
        OnTurnEnd
        );
    }

    private void SetHalfTurn(int sign)
    {
      SmoothChange(ref _turnCoroutine, hero, _moveXHash, stateData.EnterCurve, sign);
      _rotate.Turn
      (
        new Vector3(hero.MoveAxis.x, 0, hero.MoveAxis.y),
        sign == -1 ? ClipLength(PlayerActionsType.TurnLeft) : ClipLength(PlayerActionsType.TurnRight),
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