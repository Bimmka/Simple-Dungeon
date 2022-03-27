using System;
using System.Collections;
using Animations;
using Hero;
using Services;
using StateMachines.Player.Base;
using UnityEngine;

namespace StateMachines.Player.Rotating
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
      switch (currentState.LastActionType)
      {
        case PlayerActionsType.TurnAround:
          SmoothChange(ref _turnAroundCoroutine, _moveYHash, currentState.TurnAroundExitCurve);
          break;
        case PlayerActionsType.TurnLeft:
          SmoothChange(ref _turnCoroutine, _moveXHash, currentState.LeftTurnExitCurve);
          break;
        case PlayerActionsType.TurnRight:
          SmoothChange(ref _turnCoroutine, _moveXHash, currentState.RightTurnExitCurve);
          break;
      }
    }

    public void TurnAround() => 
      SmoothChange(ref _turnAroundCoroutine, _moveYHash, currentState.TurnAroundEnterCurve);

    public void Turn(PlayerActionsType playerActionsType)
    {
      if (playerActionsType == PlayerActionsType.TurnLeft)
        SmoothChange(ref _turnCoroutine, _moveXHash, currentState.LeftTurnEnterCurve);
      else if (playerActionsType == PlayerActionsType.TurnRight)
        SmoothChange(ref _turnCoroutine, _moveXHash, currentState.RightTurnEnterCurve);
    }

    private void SmoothChange(ref Coroutine coroutine, int valueHash, AnimationCurve curve)
    {
      if (coroutine != null)
        _coroutineRunner.StopCoroutine(coroutine);

      coroutine = _coroutineRunner.StartCoroutine(Change(valueHash, curve));
    }

    private IEnumerator Change(int valueHash, AnimationCurve curve)
    {
      float curveValue;
      float currentValue = _animator.GetFloat(valueHash);
      float maxCurveValue = curve[curve.length-1].value;
      float maxTime = curve[curve.length-1].time;
      float currentTime = 0f;

      if (currentValue > maxCurveValue)
        smoothChangeCheck = IsBigger;
      else
        smoothChangeCheck = IsSmaller;
      
      while (currentTime < maxTime && Mathf.Approximately(currentValue, maxCurveValue) == false)
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
        SetFloat(valueHash, currentValue);
      }
      
      SetFloat(valueHash, maxCurveValue);
    }

    private void SetFloat(int hash, float value) => 
      _animator.SetFloat(hash, value);
    
    private bool IsSmaller(float currentValue, float endValue) => 
      currentValue < endValue;

    private bool IsBigger(float currentValue, float endValue) => 
      currentValue > endValue;
  }
}