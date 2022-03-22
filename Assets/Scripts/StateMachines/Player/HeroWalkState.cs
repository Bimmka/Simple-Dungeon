using Animations;
using Hero;
using StaticData.Hero.Components;
using StaticData.Hero.States;
using UnityEngine;

namespace StateMachines.Player
{
  public class HeroWalkState : HeroMoveSubState
  {
    private readonly int _floatValueHash;
    private readonly HeroMove _heroMove;
    private readonly HeroRotate _heroRotate;
    private readonly HeroMoveStaticData _moveStaticData;

    private Coroutine _changeCoroutine;

    public HeroWalkState(HeroMoveUpMachineState upState, HeroStateMachine hero, BattleAnimator animator,
      string triggerName,
      HeroStateData stateData, string floatValueName, HeroMove heroMove, HeroRotate heroRotate, HeroMoveStaticData moveStaticData) : base( upState, hero, animator, triggerName, stateData)
    {
      _floatValueHash = Animator.StringToHash(floatValueName);
      _heroMove = heroMove;
      _heroRotate = heroRotate;
      _moveStaticData = moveStaticData;
    }

    public override void Enter()
    {
      base.Enter(); 
      if (_heroRotate.IsTurning == false && IsLowAngle(hero.MoveAxis) == false) 
        ChangeState(hero.State<HeroRotatingState>());
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
      }
      else if (hero.IsRunningPressed && hero.State<HeroRunState>().IsCanRun())
        ChangeState(hero.State<HeroRunState>());
      else if (IsLowAngle(hero.MoveAxis) && _heroRotate.IsTurning == false)
      {
        _heroRotate.RotateTo(hero.MoveAxis);
        _heroMove.Move(MoveAxis());
      }
      else if (_heroRotate.IsTurning == false) 
        InterruptState(hero.State<HeroRotatingState>());
    }

    public override void Interrupt()
    {
      base.Interrupt();
      SetFloat(_floatValueHash, 0f);
    }


    private Vector3 MoveAxis() => 
      new Vector3(hero.MoveAxis.x, 0 , hero.MoveAxis.y);

    private bool IsLowAngle(Vector2 moveAxis) => 
      Vector3.Angle(hero.transform.forward, new Vector3(moveAxis.x, 0, moveAxis.y)) < _moveStaticData.BigAngleValue;
  }
}