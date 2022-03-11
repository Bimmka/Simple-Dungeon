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
    private readonly float _smoothChangeStep = 50f;
    private readonly float _smothChangeExitStep = 100f;

    private Coroutine _changeCoroutine;

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
        SmoothChange(1f, _smoothChangeStep, IsBigger);
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
      SmoothChange(0f, _smothChangeExitStep, IsSmaller);
    }

    public override bool IsCanBeInterapted() => 
      true;

    private Vector3 MoveAxis() => 
      new Vector3(hero.MoveAxis.x, 0 , hero.MoveAxis.y);

    private void SmoothChange(float newValue, float step, Func<float, float, bool> checkCallback)
    {
      if (_changeCoroutine != null)
        _coroutineRunner.StopCoroutine(_changeCoroutine);

      _changeCoroutine = _coroutineRunner.StartCoroutine(Change(newValue, step, checkCallback));
    }

    private IEnumerator Change(float newValue, float step, Func<float, float, bool> checkCallback)
    {
      float currentValue = animator.GetFloat(_floatValueHash);
      int direction = Math.Sign(newValue - currentValue);
      while (checkCallback(currentValue, newValue) == false)
      {
        currentValue += direction * step * Time.deltaTime;
        yield return null;
        SetFloat(_floatValueHash, currentValue);
      }

      _changeCoroutine = null;
    }

    private bool IsSmaller(float value, float endValue) => 
      value < endValue;
    private bool IsBigger(float value, float endValue) => 
      value > endValue;
  }
}