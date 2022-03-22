using Animations;
using Hero;
using StaticData.Hero.Components;
using StaticData.Hero.States;
using UnityEngine;

namespace StateMachines.Player
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

    public HeroRunState(HeroMoveUpMachineState upState, HeroStateMachine hero, BattleAnimator animator, string triggerName,
      HeroStateData stateData, HeroStamina heroStamina,
      HeroRotate heroRotate, HeroMove heroMove, HeroMoveStaticData heroMoveStaticData) : base(upState, hero, animator, triggerName, stateData)
    {
      _heroStamina = heroStamina;
      _heroRotate = heroRotate;
      _heroMove = heroMove;
      _moveStaticData = heroMoveStaticData;
    }
    
     public override void Enter()
    {
      base.Enter();
      if (_heroRotate.IsTurning == false && IsLowAngle(hero.MoveAxis, _moveStaticData.BigAngleValue) == false)
        ChangeState(hero.State<HeroRotatingState>());
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
      if (IsLowAngle(hero.MoveAxis, _moveStaticData.BigAngleValue) && _heroRotate.IsTurning == false)
      {
        _heroRotate.RotateTo(hero.MoveAxis);
        _heroMove.Run(MoveAxis());
      }
      else if (_heroRotate.IsTurning == false) 
        InterruptState(hero.State<HeroRotatingState>());
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
        ChangeState(hero.State<HeroIdleState>());
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