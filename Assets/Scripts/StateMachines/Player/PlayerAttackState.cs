using Animations;
using Hero;
using UnityEngine;

namespace StateMachines.Player
{
  public class PlayerAttackState : PlayerBaseMachineState
  {
    private readonly HeroStateMachine hero;
    private float lastAttackTime;
    
    public PlayerAttackState(StateMachine stateMachine, string animationName, SimpleAnimator animator, HeroStateMachine hero) : base(stateMachine, animationName, animator)
    {
      this.hero = hero;
    }

    public bool IsCanAttack() => 
      true;

    public override bool IsCanBeInterapted() => 
      IsAttackEnded();

    public override void AnimationTrigger()
    {
      base.AnimationTrigger();
      if (hero.MoveAxis == Vector2.zero)
        ChangeState(hero.IdleState);
      else
        ChangeState(hero.MoveState);
    }

    private bool IsAttackEnded()
    {
      return true;
    }
    
    
  }
}