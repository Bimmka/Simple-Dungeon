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

    protected void SetFloat(int hash, float value) => 
      animator.SetFloat(hash, value);

    protected float ClipLength(PlayerActionsType actionsType) => 
      hero.ClipLength(actionsType);

    protected void SmoothChange(ref Coroutine changeCoroutine, ICoroutineRunner coroutineRunner, int valueHash, float newValue, float step, Func<float, float, bool> checkCallback)
    {
      if (changeCoroutine != null)
        coroutineRunner.StopCoroutine(changeCoroutine);

      changeCoroutine = coroutineRunner.StartCoroutine(Change(changeCoroutine, valueHash, newValue, step, checkCallback));
    }

    protected bool IsSmaller(float value, float endValue) => 
      value < endValue;

    protected bool IsBigger(float value, float endValue) => 
      value > endValue;


    protected bool IsStayHorizontal() => 
      Mathf.Approximately(hero.MoveAxis.x, 0);

    protected bool IsStayVertical() => 
      Mathf.Approximately(hero.MoveAxis.y, 0);

    protected bool IsNotMove() => 
      hero.MoveAxis == Vector2.zero;

    private IEnumerator Change(Coroutine changeCoroutine, int valueHash, float newValue, float step, Func<float, float, bool> checkCallback)
    {
      float currentValue = animator.GetFloat(valueHash);
      int direction = Math.Sign(newValue - currentValue);
      while (checkCallback(currentValue, newValue) == false)
      {
        currentValue += direction * step * Time.deltaTime;
        yield return null;
        SetFloat(valueHash, currentValue);
      }
      
      SetFloat(valueHash, newValue);
      changeCoroutine = null;
    }
  }
}