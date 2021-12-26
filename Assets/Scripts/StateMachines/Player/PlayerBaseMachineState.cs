using Animations;
using UnityEngine;

namespace StateMachines.Player
{
  public abstract class PlayerBaseMachineState : BaseStateMachineState
  {
    private readonly StateMachine stateMachine;
    protected readonly BattleAnimator animator;

    public PlayerBaseMachineState(StateMachine stateMachine, string animationName, BattleAnimator animator)
    {
      this.stateMachine = stateMachine;
      this.animationName = Animator.StringToHash(animationName);
      this.animator = animator;
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

    public void ChangeState(PlayerBaseMachineState state) => 
      stateMachine.ChangeState(state);

    public void SetFloat(int hash, float value) => 
      animator.SetFloat(hash, value);
    
  }
}