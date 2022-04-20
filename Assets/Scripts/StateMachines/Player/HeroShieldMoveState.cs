using Animations;
using Hero;
using StateMachines.Player.AnimationStatesBehaviour;
using StateMachines.Player.Move;
using StateMachines.Player.Rotating;
using StaticData.Hero.Components;
using StaticData.Hero.States.Base;
using UnityEngine;

namespace StateMachines.Player
{
  public class HeroShieldMoveState : HeroShieldMoveSubState
  {
    private readonly int _floatValueHash;
    private readonly HeroMove _heroMove;
    private readonly HeroRotate _heroRotate;
    private readonly HeroMoveStaticData _moveStaticData;

    public HeroShieldMoveState(HeroShieldMoveUpMachineState upState, HeroStateMachine hero,
      BattleAnimator animator, string triggerName, HeroMoveStateData stateData, ShieldMoveBehaviour behaviour, HeroMove heroMove, HeroRotate heroRotate, HeroMoveStaticData moveStaticData) : base(upState, hero, animator, triggerName, stateData, behaviour)
    {
      _heroMove = heroMove;
      _heroRotate = heroRotate;
      _moveStaticData = moveStaticData;
    }

    public override void Enter()
    {
      base.Enter();
      if (_heroRotate.IsTurning == false && IsLowAngle(hero.MoveAxis, _moveStaticData.BigAngleValue) == false) 
        ChangeState(hero.State<HeroRotatingSubState>());
    }

    public override void LogicUpdate()
    {
      base.LogicUpdate(); 
#if DEBUG_MOVE
      Debug.DrawRay(hero.transform.position, hero.transform.forward, Color.red);
      Debug.DrawRay(hero.transform.position, MoveAxis(), Color.green);
#endif
      if (hero.IsNotMove())
      {
        if (hero.IsBlockingPressed)
          ChangeState(hero.State<HeroIdleShieldState>());
        else
          ChangeState(hero.State<HeroIdleState>());
      }
      else if (hero.IsBlockingPressed == false)
      {
        ChangeState(hero.State<HeroWalkState>());
      }
      else if (IsLowAngle(hero.MoveAxis, _moveStaticData.BigAngleValue) && _heroRotate.IsTurning == false)
      {
        _heroRotate.RotateTo(hero.MoveAxis);
        _heroMove.Move(MoveAxis());
      }
      else if (_heroRotate.IsTurning == false) 
        InterruptState(hero.State<HeroRotatingSubState>());
    }
  }
}