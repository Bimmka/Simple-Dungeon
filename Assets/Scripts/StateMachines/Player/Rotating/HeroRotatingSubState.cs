using System;
using Animations;
using Hero;
using StateMachines.Player.AnimationStatesBehaviour;
using StateMachines.Player.Base;
using StateMachines.Player.Move;
using StaticData.Hero.States.Base;
using UnityEngine;

namespace StateMachines.Player.Rotating
{
  public class HeroRotatingSubState : HeroBaseSubStateMachineState<HeroRotatingUpMachineState, HeroRotatingSubState, HeroRotateStateData, RotatingBehaviour>
  {
    private readonly HeroRotate _rotate;
 
    public AnimationCurve LeftTurnEnterCurve => stateData.LeftTurnEnterCurve;
    public AnimationCurve LeftTurnExitCurve => stateData.LeftTurnEnterCurve;
    public AnimationCurve RightTurnEnterCurve => stateData.LeftTurnEnterCurve;
    public AnimationCurve RightTurnExitCurve => stateData.LeftTurnEnterCurve;
    public AnimationCurve TurnAroundEnterCurve => stateData.LeftTurnEnterCurve;
    public AnimationCurve TurnAroundExitCurve => stateData.LeftTurnEnterCurve;

    public PlayerActionsType LastActionType { get; protected set; }

    public HeroRotatingSubState(HeroRotatingUpMachineState upState, HeroStateMachine hero, BattleAnimator animator,
      string triggerName, HeroRotateStateData stateData, RotatingBehaviour rotatingBehaviour, HeroRotate rotate) : base(upState, hero, animator, triggerName, stateData, rotatingBehaviour)
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
      LastActionType = PlayerActionsType.TurnAround;
    }

    private void SetHalfTurn(int sign)
    {
      _rotate.Turn
      (
        new Vector3(hero.MoveAxis.x, 0, hero.MoveAxis.y),
        sign == -1 ? ClipLength(PlayerActionsType.TurnLeft) : ClipLength(PlayerActionsType.TurnRight),
        OnTurnEnd
        );
      LastActionType = sign == -1 ? PlayerActionsType.TurnLeft : PlayerActionsType.TurnRight;
      upState.Turn(LastActionType);
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