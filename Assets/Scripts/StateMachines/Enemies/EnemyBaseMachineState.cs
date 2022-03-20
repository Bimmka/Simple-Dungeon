using Animations;
using StaticData.Enemies;
using UnityEngine;

namespace StateMachines.Enemies
{
  public abstract class EnemyBaseMachineState 
  {
    protected readonly BattleAnimator animator;
    private int _triggerName;
    
    public virtual int Weight { get; }

    public EnemyBaseMachineState(StateMachine stateMachine, string animationName, BattleAnimator animator)
    {
     
      _triggerName = Animator.StringToHash(animationName);
      this.animator = animator;
    }
    
    public virtual void Enter()
    {
      
      animator.SetBool(_triggerName,true);
    }

    public virtual void Exit()
    {
      
      animator.SetBool(_triggerName, false);
    }

    public virtual void LogicUpdate()
    {
      
    }
    
    public void ChangeState(EnemyBaseMachineState state)
    {
      
    }
    
    public virtual bool IsCanBeInterrupted(int weight) =>
      false;
    
    public virtual void TriggerAnimation()
    {
      
      
    }


    public void SetFloat(int hash, float value) => 
      animator.SetFloat(hash, value);
  }
}