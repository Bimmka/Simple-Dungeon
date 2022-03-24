using System;
using Animations;
using Hero;
using StaticData.Hero.States;
using UnityEngine;

namespace StateMachines.Player
{
  public class HeroRotatingState : HeroRotatingSubState
  {
    private readonly HeroRotate _rotate;
 

    public HeroRotatingState(HeroRotatingUpMachineState upState, HeroStateMachine hero, BattleAnimator animator,
      string triggerName, HeroStateData stateData, HeroRotate rotate) : base(upState, hero, animator, triggerName, stateData)
    {
      _rotate = rotate;
    }

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
      upState.TurnAround();
      _rotate.TurnAround
      (
        new Vector3(hero.MoveAxis.x, 0, hero.MoveAxis.y), 
        ClipLength(PlayerActionsType.TurnAround),
        OnTurnEnd
        );
    }

    private void SetHalfTurn(int sign)
    {
      upState.Turn(sign);
      _rotate.Turn
      (
        new Vector3(hero.MoveAxis.x, 0, hero.MoveAxis.y),
        sign == -1 ? ClipLength(PlayerActionsType.TurnLeft) : ClipLength(PlayerActionsType.TurnRight),
        OnTurnEnd
        );
    }

    private void OnTurnEnd()
    {
      if (hero.IsNotMove())
        ChangeState(hero.State<HeroIdleState>());
      else
        ChangeState(hero.State<HeroWalkState>());
    }
  }
}