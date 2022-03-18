using System;
using System.Collections;
using Animations;
using Hero;
using Services;
using StaticData.Hero.States;
using UnityEngine;

namespace StateMachines.Player
{
  public abstract class PlayerBaseMachineState : BaseStateMachineState, IPlayerMachineState
  {
    private const float CoeffToFrameSkipp = 3f;
    private event Func<float, float, bool> smoothChangeCheck;
    
    private readonly StateMachine _stateMachine;
    
    protected readonly HeroStateMachine hero;
    protected readonly HeroStateData stateData;
    protected readonly BattleAnimator animator;

    protected PlayerBaseMachineState(StateMachine stateMachine, string triggerName, BattleAnimator animator, HeroStateMachine hero, HeroStateData stateData)
    {
      _stateMachine = stateMachine;
      _triggerName = Animator.StringToHash(triggerName);
      this.animator = animator;
      this.hero = hero;
      this.stateData = stateData;
    }

    public override void Enter()
    {
      base.Enter();
      animator.SetBool(_triggerName,true);
    }

    public override void Exit()
    {
      base.Exit();
      animator.SetBool(_triggerName, false);
    }

    protected void ChangeState(PlayerBaseMachineState state) => 
      _stateMachine.ChangeState(state);

    protected void InterruptState(PlayerBaseMachineState state) => 
      _stateMachine.InterruptState(state);

    protected void SetFloat(int hash, float value) => 
      animator.SetFloat(hash, value);

    protected float ClipLength(PlayerActionsType actionsType) => 
      hero.ClipLength(actionsType);

    protected void SmoothChange(ref Coroutine changeCoroutine, ICoroutineRunner coroutineRunner, int valueHash, AnimationCurve curve, int direction = 1, Action callback = null)
    {
      if (changeCoroutine != null)
        coroutineRunner.StopCoroutine(changeCoroutine);

      changeCoroutine = coroutineRunner.StartCoroutine(Change(changeCoroutine, valueHash, curve, direction, callback));
    }
    protected bool IsStayHorizontal() => 
      Mathf.Approximately(hero.MoveAxis.x, 0);

    protected bool IsStayVertical() => 
      Mathf.Approximately(hero.MoveAxis.y, 0);

    protected bool IsNotMove() => 
      hero.MoveAxis == Vector2.zero;

    private IEnumerator Change(Coroutine changeCoroutine, int valueHash, AnimationCurve curve, int direction, Action callback)
    {
      float curveValue;
      float currentValue = animator.GetFloat(valueHash);
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