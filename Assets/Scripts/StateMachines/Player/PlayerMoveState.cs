using System;
using System.Collections;
using Animations;
using Hero;
using Services;
using StaticData.Hero.Components;
using UnityEngine;

namespace StateMachines.Player
{
  public class PlayerMoveState : PlayerBaseMachineState
  {
    private readonly int _floatValueHash;
    private readonly HeroMove _heroMove;
    private readonly HeroRotate _heroRotate;
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly HeroMoveStaticData _moveStaticData;
    private readonly float _smoothChangeStep = 5f;
    private readonly float _smothChangeExitStep = 100f;

    private Coroutine _changeCoroutine;
    public override int Weight { get; }


    public PlayerMoveState(StateMachine stateMachine, string animationName, string floatValueName,
      BattleAnimator animator,
      HeroStateMachine hero, HeroMove heroMove, HeroRotate heroRotate, ICoroutineRunner coroutineRunner, HeroMoveStaticData moveStaticData) : base(stateMachine, animationName, animator, hero)
    {
      _floatValueHash = Animator.StringToHash(floatValueName);
      _heroMove = heroMove;
      _heroRotate = heroRotate;
      _coroutineRunner = coroutineRunner;
      _moveStaticData = moveStaticData;
    }

    public override void Enter()
    {
      base.Enter();
      if (IsLowAngle(hero.MoveAxis) && _heroRotate.IsTurning == false)
        SmoothChange(ref _changeCoroutine, _coroutineRunner,_floatValueHash,1f, _smothChangeExitStep, IsBigger);
      else if (_heroRotate.IsTurning == false) 
        ChangeState(hero.State<PlayerRotatingState>());
    }

    public override void LogicUpdate()
    {
      base.LogicUpdate(); 
      Debug.DrawRay(hero.transform.position, hero.transform.forward, Color.red);
      Debug.DrawRay(hero.transform.position, MoveAxis(), Color.green);
      
      if (IsNotMove())
        ChangeState(hero.State<PlayerIdleState>());
      else if (IsLowAngle(hero.MoveAxis) && _heroRotate.IsTurning == false)
      {
        _heroRotate.RotateTo(hero.MoveAxis);
        _heroMove.Move(MoveAxis());
      }
      else if (_heroRotate.IsTurning == false) 
        ChangeState(hero.State<PlayerRotatingState>());
    }

    private bool IsLowAngle(Vector2 moveAxis) => 
      Vector3.Angle(hero.transform.forward, new Vector3(moveAxis.x, 0, moveAxis.y)) < _moveStaticData.BigAngleValue;

    public override void Exit()
    {
      base.Exit();
      SmoothChange(ref _changeCoroutine, _coroutineRunner,_floatValueHash,0f, _smothChangeExitStep, IsSmaller);
    }

    public override bool IsCanBeInterapted(int weight) =>
      true;

    private Vector3 MoveAxis() => 
      new Vector3(hero.MoveAxis.x, 0 , hero.MoveAxis.y);

   
  }
}