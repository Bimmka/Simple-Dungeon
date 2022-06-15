using Animations;
using Hero;
using StateMachines.Player.AnimationStatesBehaviour;
using StateMachines.Player.Rotating;
using StaticData.Hero.Components;
using StaticData.Hero.States.Base;
using UnityEngine;

namespace StateMachines.Player.Move
{
  public class HeroWalkState : HeroMoveSubState
  {
    private readonly HeroMove _heroMove;
    private readonly HeroRotate _heroRotate;
    private readonly HeroMoveStaticData _moveStaticData;

    

    private Coroutine _changeCoroutine;

    public HeroWalkState(HeroMoveUpMachineState upState, HeroStateMachine hero, BattleAnimator animator,
      string triggerName, string speedName, HeroMoveStateData stateData, MoveBehaviour behaviour, HeroMove heroMove, HeroRotate heroRotate, 
      HeroMoveStaticData moveStaticData) : base( upState, hero, animator, triggerName, speedName,stateData, behaviour)
    {
      _heroMove = heroMove;
      _heroRotate = heroRotate;
      _moveStaticData = moveStaticData;
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
        ChangeState(hero.State<HeroIdleState>());
        SetTriggerSpeedValue(0f);
      }
      else if (hero.IsRunningPressed && hero.State<HeroRunState>().IsCanRun())
        ChangeState(hero.State<HeroRunState>());
      else if (IsNeedTurnAround(hero.MoveAxis, _moveStaticData.TurnAroundTriggerValue) == false && _heroRotate.IsTurning == false)
      {
        _heroRotate.RotateTo(hero.MoveAxis);
        if (hero.IsBlockingPressed == false)
        {
          _heroMove.Move(MoveAxis());
          SetTriggerSpeedValue(_moveStaticData.MoveSpeed);
        }
        else
        {
          _heroMove.ShieldMove(MoveAxis());
          SetTriggerSpeedValue(_moveStaticData.ShieldMoveSpeed);
        }
      }
      else if (_heroRotate.IsTurning == false) 
        InterruptState(hero.State<HeroRotatingSubState>());
    }
  }
}