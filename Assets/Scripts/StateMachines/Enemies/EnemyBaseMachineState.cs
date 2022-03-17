using Animations;
using StaticData.Enemies;
using UnityEngine;

namespace StateMachines.Enemies
{
  public abstract class EnemyBaseMachineState : BaseStateMachineState
  {
    private readonly StateMachine stateMachine;
    protected readonly BattleAnimator animator;

    public EnemyBaseMachineState(StateMachine stateMachine, string animationName, BattleAnimator animator)
    {
      this.stateMachine = stateMachine;
      this._triggerName = Animator.StringToHash(animationName);
      this.animator = animator;
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
    
    public void ChangeState(EnemyBaseMachineState state) => 
      stateMachine.ChangeState(state);
    
    public void SetFloat(int hash, float value) => 
      animator.SetFloat(hash, value);
  }
}