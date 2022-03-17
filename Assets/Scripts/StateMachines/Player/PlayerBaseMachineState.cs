using System;
using System.Collections;
using Animations;
using Hero;
using Services;
using UnityEngine;

namespace StateMachines.Player
{
  public abstract class PlayerBaseMachineState : BaseStateMachineState, IPlayerMachineState
  {
    private readonly StateMachine stateMachine;
    
    protected readonly HeroStateMachine hero;
    protected readonly BattleAnimator animator;

    public PlayerBaseMachineState(StateMachine stateMachine, string animationName, BattleAnimator animator, HeroStateMachine hero)
    {
      this.stateMachine = stateMachine;
      this.animationName = Animator.StringToHash(animationName);
      this.animator = animator;
      this.hero = hero;
    }

    public override void Enter()
    {
      base.Enter();
      animator.SetBool(animationName,true);
    }

    public override void Exit()
    {
      base.Exit();
      animator.SetBool(animationName, false);
    }

    protected void ChangeState(PlayerBaseMachineState state) => 
      stateMachine.ChangeState(state);

    protected void SetFloat(int hash, float value) => 
      animator.SetFloat(hash, value);
    
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