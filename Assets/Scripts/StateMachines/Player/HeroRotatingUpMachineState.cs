using System;
using System.Collections;
using Animations;
using Hero;
using Services;
using UnityEngine;

namespace StateMachines.Player
{
  public class HeroRotatingUpMachineState : HeroBaseUpMachineState<HeroRotatingSubState>
  {
    private readonly BattleAnimator _animator;
    private const float CoeffToFrameSkipp = 3f;
    private event Func<float, float, bool> smoothChangeCheck;
    
    private readonly int _moveXHash;
    private readonly int _moveYHash;

    private Coroutine _turnCoroutine;
    private Coroutine _turnAroundCoroutine;
    
    public HeroRotatingUpMachineState(StateMachineWithSubstates stateMachine, HeroStateMachine hero, ICoroutineRunner coroutineRunner,  BattleAnimator animator, string moveXName, string moveYName) : base(stateMachine, hero, coroutineRunner)
    {
      _animator = animator;
      _moveXHash = Animator.StringToHash(moveXName);
      _moveYHash = Animator.StringToHash(moveYName);
    }

    public override void Exit()
    {
      base.Exit();
      if (_turnCoroutine != null)
        _coroutineRunner.StopCoroutine(_turnCoroutine);
      _turnCoroutine = _coroutineRunner.StartCoroutine(Change(_moveXHash, currentState.ExitCurve, 1));
      if (_turnAroundCoroutine != null)
        _coroutineRunner.StopCoroutine(_turnAroundCoroutine);
      _turnAroundCoroutine = _coroutineRunner.StartCoroutine(Change(_moveYHash, currentState.ExitCurve, 1));
    }

    public void TurnAround()
    {
      if (_turnAroundCoroutine != null)
        _coroutineRunner.StopCoroutine(_turnAroundCoroutine);
      _turnAroundCoroutine = _coroutineRunner.StartCoroutine(Change(_moveYHash, currentState.EnterCurve, 1));
    }

    public void Turn(int sign)
    {
      if (_turnCoroutine != null)
        _coroutineRunner.StopCoroutine(_turnCoroutine);
      _turnCoroutine = _coroutineRunner.StartCoroutine(Change(_moveXHash, currentState.EnterCurve, sign));
    }

    private IEnumerator Change(int valueHash, AnimationCurve curve, int direction)
    {
      float curveValue;
      float currentValue = _animator.GetFloat(valueHash);
      float maxCurveValue = curve[curve.length-1].value;
      float maxTime = curve[curve.length-1].time;
      float currentTime = 0f;

      if (currentValue > direction * maxCurveValue)
        smoothChangeCheck = IsBigger;
      else
        smoothChangeCheck = IsSmaller;
      
      while (currentTime < maxTime && Mathf.Approximately(currentValue, maxCurveValue) == false)
      {
        curveValue = curve.Evaluate(currentTime);
        if (smoothChangeCheck.Invoke(currentValue, curveValue))
        {
          currentValue = direction * curveValue;
          currentTime += Time.deltaTime;
        }
        else
          currentTime += CoeffToFrameSkipp * Time.deltaTime;
        
        yield return null;
        SetFloat(valueHash, currentValue);
      }
      
      SetFloat(valueHash, direction * maxCurveValue);
    }

    private void SetFloat(int hash, float value) => 
      _animator.SetFloat(hash, value);
    
    private bool IsSmaller(float currentValue, float endValue) => 
      currentValue < endValue;

    private bool IsBigger(float currentValue, float endValue) => 
      currentValue > endValue;
  }
}