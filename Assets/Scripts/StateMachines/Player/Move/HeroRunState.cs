using Animations;
using Hero;
using StateMachines.Player.AnimationStatesBehaviour;
using StateMachines.Player.Rotating;
using StaticData.Hero.Components;
using StaticData.Hero.States.Base;
using UnityEngine;

namespace StateMachines.Player.Move
{
  public class HeroRunState : HeroMoveSubState
  {
    private readonly HeroStamina _heroStamina;
    private readonly HeroRotate _heroRotate;
    private readonly HeroMove _heroMove;
    private readonly HeroMoveStaticData _moveStaticData;

    private Coroutine _changeCoroutine;

    private bool _isStopping;
    private float time = 1f;

    public HeroRunState(HeroMoveUpMachineState upState, HeroStateMachine hero, BattleAnimator animator,
      string triggerName, string speed, HeroMoveStateData stateData, MoveBehaviour behaviour, HeroStamina heroStamina,
      HeroRotate heroRotate, HeroMove heroMove, HeroMoveStaticData heroMoveStaticData) : 
      base(upState, hero, animator, triggerName, speed,stateData, behaviour)
    {
      _heroStamina = heroStamina;
      _heroRotate = heroRotate;
      _heroMove = heroMove;
      _moveStaticData = heroMoveStaticData;
    }

    public override void LogicUpdate()
    {
      base.LogicUpdate(); 
#if DEBUG_MOVE
      Debug.DrawRay(hero.transform.position, hero.transform.forward, Color.red);
      Debug.DrawRay(hero.transform.position, MoveAxis(), Color.green);
#endif
      if (_heroStamina.IsCanRun() && hero.IsRunningPressed && hero.IsNotMove() == false)
      {
        Run();
        UpdateTimerAndStamina();
      }
      else
        TransitionToAnotherState();
    }

    private void Run()
    {
      if (IsNeedTurnAround(hero.MoveAxis, _moveStaticData.TurnAroundTriggerValue) == false)
      {
        _heroRotate.RotateTo(hero.MoveAxis);
        if (hero.IsBlockingPressed == false)
        {
          _heroMove.Run(MoveAxis());
          SetTriggerSpeedValue(_moveStaticData.RunSpeed);
        }
        else
        {
          _heroMove.ShieldRun(MoveAxis());
          SetTriggerSpeedValue(_moveStaticData.ShieldRunMoveSpeed);
        }
      }
      else if (_heroRotate.IsTurning == false) 
        InterruptState(hero.State<HeroRotatingSubState>());
    }

    private void UpdateTimerAndStamina()
    {
      if (IsNeedWasteStamina())
      {
        _heroStamina.WasteToRun();
        ResetTime();
      }
      else
        UpdateTime(Time.deltaTime);
    }

    private void TransitionToAnotherState()
    {
      if (hero.IsNotMove())
      {
        ChangeState(hero.State<HeroIdleState>());
        SetTriggerSpeedValue(0f);
      }
      else
        ChangeState(hero.State<HeroWalkState>());
    }

    public override void Interrupt()
    {
      base.Interrupt();
      ResetTime();
    }

    public override void Exit()
    {
      base.Exit();
      ResetTime();
    }

    public bool IsCanRun() => 
      _heroStamina.IsCanRun();

    private void ResetTime() => 
      time = 1f;

    private void UpdateTime(float deltaTime) => 
      time -= deltaTime;

    private bool IsNeedWasteStamina() => 
      time <= 0;
    
    
  }
}