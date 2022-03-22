using System;
using System.Collections;
using Animations;
using Hero;
using Services;
using UnityEngine;

namespace StateMachines.Player
{
  public class HeroMoveUpMachineState : HeroBaseUpMachineState<HeroMoveSubState>
  {
    private const float CoeffToFrameSkipp = 3f;
    private event Func<float, float, bool> smoothChangeCheck;
    
    private readonly BattleAnimator _animator;

    public HeroMoveUpMachineState(StateMachineWithSubstates stateMachine, HeroStateMachine hero, ICoroutineRunner coroutineRunner, BattleAnimator animator) : base(stateMachine, hero, coroutineRunner)
    {
      _animator = animator;
    }
    
    public void SmoothChange(int valueHash, AnimationCurve curve, int direction = 1, Action callback = null)
    {
      if (_changeCoroutine != null)
        _coroutineRunner.StopCoroutine(_changeCoroutine);

      _changeCoroutine = _coroutineRunner.StartCoroutine(Change(_changeCoroutine, valueHash, curve, direction, callback));
    }

    private void SetFloat(int hash, float value) => 
      _animator.SetFloat(hash, value);
    
    private IEnumerator Change(Coroutine changeCoroutine, int valueHash, AnimationCurve curve, int direction, Action callback)
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
      changeCoroutine = null;
      
      callback?.Invoke();
    }

    private bool IsSmaller(float currentValue, float endValue) => 
      currentValue < endValue;

    private bool IsBigger(float currentValue, float endValue) => 
      currentValue > endValue;
  }
}