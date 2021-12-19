using Animations;
using Hero;
using UnityEngine;

namespace StateMachines.Player
{
  public class PlayerIdleState : PlayerBaseMachineState
  {
    private readonly int floatValueHash;
    private readonly HeroStateMachine hero;

    public PlayerIdleState(StateMachine stateMachine, string animationName,  string floatValueName,
      SimpleAnimator animator, HeroStateMachine hero) : base(stateMachine, animationName, animator)
    {
      floatValueHash = Animator.StringToHash(floatValueName);
      this.hero = hero;
    }

    public override void LogicUpdate()
    {
      base.LogicUpdate();
      if (hero.IsBlocking)
      {
        if (hero.MoveAxis != Vector2.zero)
          ChangeState(hero.ShieldMoveState);
        else
          ChangeState(hero.IdleShieldState);
      }
      else
        if (hero.MoveAxis != Vector2.zero)
          ChangeState(hero.MoveState);
        else
          SetFloat(floatValueHash, hero.MouseRotation);
     
    }

    public override bool IsCanBeInterapted() => 
      true;
  }
}