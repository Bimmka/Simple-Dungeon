using Animations;
using Hero;
using Services;
using StaticData.Hero.Components;
using StaticData.Hero.States;
using UnityEngine;

namespace StateMachines.Player
{
  public class HeroRunState : HeroBaseMachineState
  {
    private readonly int _floatValueHash;
    private readonly HeroStamina _heroStamina;
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly HeroRotate _heroRotate;
    private readonly HeroMove _heroMove;
    private readonly HeroMoveStaticData _moveStaticData;

    private Coroutine _changeCoroutine;

    private bool _isStopping;
    private float time = 1f;

    public HeroRunState(StateMachine stateMachine, string triggerName, string floatValue, BattleAnimator animator,
      HeroStateMachine hero, HeroStateData stateData, HeroStamina heroStamina, ICoroutineRunner coroutineRunner,
      HeroRotate heroRotate, HeroMove heroMove, HeroMoveStaticData heroMoveStaticData) : base(stateMachine, triggerName, animator, hero, stateData)
    {
      _floatValueHash = Animator.StringToHash(floatValue);
      _heroStamina = heroStamina;
      _coroutineRunner = coroutineRunner;
      _heroRotate = heroRotate;
      _heroMove = heroMove;
      _moveStaticData = heroMoveStaticData;
    }
    
     public override void Enter()
    {
      base.Enter();
      if (IsLowAngle(hero.MoveAxis) && _heroRotate.IsTurning == false)
        SmoothChange(ref _changeCoroutine, _coroutineRunner,_floatValueHash,stateData.EnterCurve);
      else if (_heroRotate.IsTurning == false) 
        ChangeState(hero.State<HeroRotatingState>());
    }

    public override void LogicUpdate()
    {
      base.LogicUpdate(); 
#if DEBUG_MOVE
      Debug.DrawRay(hero.transform.position, hero.transform.forward, Color.red);
      Debug.DrawRay(hero.transform.position, MoveAxis(), Color.green);
#endif

      if (_heroStamina.IsCanRun() && hero.IsRunningPressed && IsNotMove() == false)
      {
        Run();
        UpdateTimerAndStamina();
      }
      else
        TransitionToAnotherState();
    }

    private void Run()
    {
      if (IsLowAngle(hero.MoveAxis) && _heroRotate.IsTurning == false)
      {
        _heroRotate.RotateTo(hero.MoveAxis);
        _heroMove.Run(MoveAxis());
        if (_isStopping)
          RefreshState();
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
      if (IsNotMove())
      {
        if (_isStopping == false)
          StartStopping();
        _heroMove.StoppingMove();
      }
      else
        ChangeState(hero.State<HeroWalkState>());
    }

    public override void Interrupt()
    {
      base.Interrupt();
      ResetIsStopping();
      ResetTime();
      SetFloat(_floatValueHash, 0f);
    }

    public override void Exit()
    {
      base.Exit();
      ResetIsStopping();
      ResetTime();
    }

    public bool IsCanRun() => 
      _heroStamina.IsCanRun();

    private void RefreshState()
    {
      ResetIsStopping();
      ResetTime();
      SmoothChange(ref _changeCoroutine, _coroutineRunner,_floatValueHash,stateData.EnterCurve);
    }

    private void StartStopping()
    {
      _isStopping = true;
      SmoothChange(ref _changeCoroutine, _coroutineRunner,_floatValueHash,stateData.ExitCurve, callback:SetIdleState);
    }

    private void SetIdleState()
    {
      ChangeState(hero.State<HeroIdleState>());
    }

    private void ResetIsStopping() => 
      _isStopping = false;

    private void ResetTime() => 
      time = 1f;

    private void UpdateTime(float deltaTime) => 
      time -= deltaTime;

    private bool IsNeedWasteStamina() => 
      time <= 0;


    private Vector3 MoveAxis() => 
      new Vector3(hero.MoveAxis.x, 0 , hero.MoveAxis.y);

    private bool IsLowAngle(Vector2 moveAxis) => 
      Vector3.Angle(hero.transform.forward, new Vector3(moveAxis.x, 0, moveAxis.y)) < _moveStaticData.BigAngleValue;
  }
}