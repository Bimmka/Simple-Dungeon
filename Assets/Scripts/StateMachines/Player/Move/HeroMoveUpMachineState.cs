using System;
using System.Collections;
using Animations;
using Hero;
using Services;
using StateMachines.Player.Base;
using UnityEngine;

namespace StateMachines.Player.Move
{
  public class HeroMoveUpMachineState : HeroBaseUpMachineState<HeroMoveSubState>
  {
    private const float CoeffToFrameSkipp = 3f;
    private event Func<float, float, bool> smoothChangeCheck;
    
    private readonly BattleAnimator _animator;

    private readonly int _moveHashName; 

    public HeroMoveUpMachineState(StateMachineWithSubstates stateMachine, HeroStateMachine hero,
      ICoroutineRunner coroutineRunner, BattleAnimator animator, string moveHashName) : base(stateMachine, hero, coroutineRunner)
    {
      _animator = animator;
      _moveHashName = Animator.StringToHash(moveHashName);
    }


    protected override void SetNewSubstate(IHeroBaseSubStateMachineState to)
    {
      if (to is HeroIdleState)
        TransferToIdleState();
      else if (to is HeroWalkState)
        TransferToWalkState();
      else if (to is HeroRunState)
        TransferToRunState();
          
      base.SetNewSubstate(to);
    }

    private void TransferToIdleState()
    {
      if (currentState is HeroWalkState)
        SmoothChange(SubState<HeroWalkState>().ExitCurve);
      else if (currentState is HeroRunState) 
        SmoothChange(SubState<HeroRunState>().DownStateEnterCurve);
    }

    private void TransferToWalkState()
    {
      if (currentState is HeroIdleState)
        SmoothChange(SubState<HeroWalkState>().EnterCurve);
      else if (currentState is HeroRunState) 
        SmoothChange(SubState<HeroRunState>().UpStateEnterCurve);
    }

    private void TransferToRunState()
    {
      if (currentState is HeroWalkState)
        SmoothChange(SubState<HeroWalkState>().UpStateEnterCurve);
      else if (currentState is HeroIdleState) 
        SmoothChange(SubState<HeroIdleState>().UpStateEnterCurve);
    }


    private void SmoothChange(AnimationCurve curve)
    {
      if (_changeCoroutine != null)
        _coroutineRunner.StopCoroutine(_changeCoroutine);

      _changeCoroutine = _coroutineRunner.StartCoroutine(Change(curve, curve[curve.length-1].value));
    }
    
    private void SetFloat(int hash, float value) => 
      _animator.SetFloat(hash, value);

    private IEnumerator Change(AnimationCurve curve, float endValue)
    {
      float curveValue;
      float currentValue = _animator.GetFloat(_moveHashName);
      float maxTime = curve[curve.length-1].time;
      float currentTime = 0f;

      if (currentValue > endValue)
        smoothChangeCheck = IsBigger;
      else
        smoothChangeCheck = IsSmaller;
      
      while (currentTime < maxTime && Mathf.Approximately(currentValue, endValue) == false)
      {
        curveValue = curve.Evaluate(currentTime);
        if (smoothChangeCheck.Invoke(currentValue, curveValue))
        {
          currentValue = curveValue;
          currentTime += Time.deltaTime;
        }
        else
          currentTime += CoeffToFrameSkipp * Time.deltaTime;
        
        yield return null;
        SetFloat(_moveHashName, currentValue);
      }
      
      SetFloat(_moveHashName, endValue);
      _changeCoroutine = null;
    }

    private bool IsSmaller(float currentValue, float endValue) => 
      currentValue < endValue;

    private bool IsBigger(float currentValue, float endValue) => 
      currentValue > endValue;
  }
}