using Animations;
using Hero;
using Services;
using StaticData.Hero.Components;
using StaticData.Hero.States;
using UnityEngine;

namespace StateMachines.Player
{
  public class PlayerWalkState : PlayerBaseMachineState
  {
    private readonly int _floatValueHash;
    private readonly HeroMove _heroMove;
    private readonly HeroRotate _heroRotate;
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly HeroMoveStaticData _moveStaticData;

    private Coroutine _changeCoroutine;

    private bool _isStopping;
    
    public override int Weight { get; }


    public PlayerWalkState(StateMachine stateMachine, string triggerName, string floatValueName,
      BattleAnimator animator,
      HeroStateMachine hero, HeroMove heroMove, HeroRotate heroRotate, ICoroutineRunner coroutineRunner, HeroMoveStaticData moveStaticData, HeroStateData stateData) : base(stateMachine, triggerName, animator, hero, stateData)
    {
      _floatValueHash = Animator.StringToHash(floatValueName);
      _heroMove = heroMove;
      _heroRotate = heroRotate;
      _coroutineRunner = coroutineRunner;
      _moveStaticData = moveStaticData;
    }

    public override void Enter()
    {
      base.Enter();
      if (IsLowAngle(hero.MoveAxis) && _heroRotate.IsTurning == false)
        SmoothChange(ref _changeCoroutine, _coroutineRunner,_floatValueHash,stateData.EnterCurve);
      else if (_heroRotate.IsTurning == false) 
        ChangeState(hero.State<PlayerRotatingState>());
    }

    public override void LogicUpdate()
    {
      base.LogicUpdate(); 
#if DEBUG_MOVE
      Debug.DrawRay(hero.transform.position, hero.transform.forward, Color.red);
      Debug.DrawRay(hero.transform.position, MoveAxis(), Color.green);
#endif

      if (IsNotMove())
      {
        if (_isStopping == false)
          StartStopping();
        _heroMove.StoppingMove();
      }
      else if (IsLowAngle(hero.MoveAxis) && _heroRotate.IsTurning == false)
      {
        _heroRotate.RotateTo(hero.MoveAxis);
        _heroMove.Move(MoveAxis());
        if (_isStopping)
          RefreshState();
      }
      else if (_heroRotate.IsTurning == false) 
        InterruptState(hero.State<PlayerRotatingState>());
    }

    public override void Interrupt()
    {
      base.Interrupt();
      ResetIsStopping();
      SetFloat(_floatValueHash, 0f);
    }

    public override void Exit()
    {
      base.Exit();
      ResetIsStopping();
      SmoothChange(ref _changeCoroutine, _coroutineRunner,_floatValueHash, stateData.ExitCurve);
    }

    public override bool IsCanBeInterrupted(int weight)
    {
      if (stateData.IsInteraptedBySameWeight)
        return weight >= Weight;
      return weight > Weight;
    }

    private void RefreshState()
    {
      ResetIsStopping();
      SmoothChange(ref _changeCoroutine, _coroutineRunner,_floatValueHash,stateData.EnterCurve);
    }

    private void StartStopping()
    {
      _isStopping = true;
      SmoothChange(ref _changeCoroutine, _coroutineRunner,_floatValueHash,stateData.ExitCurve, callback:SetIdleState);
    }

    private void SetIdleState()
    {
      ChangeState(hero.State<PlayerIdleState>());
    }

    private void ResetIsStopping() => 
      _isStopping = false;


    private Vector3 MoveAxis() => 
      new Vector3(hero.MoveAxis.x, 0 , hero.MoveAxis.y);

    private bool IsLowAngle(Vector2 moveAxis) => 
      Vector3.Angle(hero.transform.forward, new Vector3(moveAxis.x, 0, moveAxis.y)) < _moveStaticData.BigAngleValue;
  }
}