using System;
using System.Collections.Generic;
using System.Linq;
using Animations;
using Services;
using StateMachines;
using StateMachines.Player;
using StaticData.Hero.Components;
using StaticData.Hero.States;

namespace Hero
{
  public class HeroMachineStatesFactory
  {
    private readonly StateMachine _stateMachine;
    private readonly HeroStateMachine _hero;
    private readonly BattleAnimator _animator;
    private readonly HeroMove _move;
    private readonly HeroAttack _attack;
    private readonly HeroRotate _rotate;
    private readonly HeroAttackStaticData _attackData;
    private readonly HeroStamina _stamina;
    private readonly HeroImpactsStaticData _impactData;
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly HeroMoveStaticData _moveStaticData;
    private readonly Dictionary<HeroState, HeroStateData> _statesData;


    public HeroMachineStatesFactory(StateMachine stateMachine, HeroStateMachine hero, BattleAnimator animator,
      HeroMove move,
      HeroAttack attack, HeroRotate rotate, HeroAttackStaticData attackData, HeroStamina stamina,
      HeroImpactsStaticData impactData, ICoroutineRunner coroutineRunner, HeroMoveStaticData moveStaticData, HeroStatesStaticData statesData)
    {
      _stateMachine = stateMachine;
      _hero = hero;
      _animator = animator;
      _move = move;
      _attack = attack;
      _rotate = rotate;
      _attackData = attackData;
      _stamina = stamina;
      _impactData = impactData;
      _coroutineRunner = coroutineRunner;
      _moveStaticData = moveStaticData;
      _statesData = statesData.StateDatas.ToDictionary(x => x.State, x=>x);
    }

    public void CreateStates(ref Dictionary<Type, PlayerBaseMachineState> states)
    {
      Array heroStates = Enum.GetValues(typeof(HeroState));
      (Type, PlayerBaseMachineState) state;
      for (int i = 0; i < heroStates.Length; i++)
      {
        state = CreateState((HeroState) i);
        states.Add(state.Item1, state.Item2);
      }
    }

    private (Type, PlayerBaseMachineState) CreateState(HeroState state)
    {
      switch (state)
      {
        case HeroState.Idle:
          return (typeof(PlayerIdleState), new PlayerIdleState(_stateMachine, "IsIdle", _animator, _hero, GetStateData(state)));
        case HeroState.Walk:
          return (typeof(PlayerWalkState), new PlayerWalkState(_stateMachine, "IsIdle", "MoveX", _animator, _hero, _move, _rotate, _coroutineRunner, _moveStaticData, GetStateData(state)));
        case HeroState.Run:
          return (typeof(PlayerRunState), new PlayerRunState(_stateMachine, "IsIdle", "MoveX", _animator, _hero, GetStateData(state)));
        case HeroState.Roll:
          return (typeof(PlayerRollState), new PlayerRollState(_stateMachine, "IsRoll", _animator, _hero, _move, _stamina, GetStateData(state)));
        case HeroState.Rotating:
          return (typeof(PlayerRotatingState), new PlayerRotatingState(_stateMachine, "IsRotating", _animator, _hero, "RotateX", "RotateY", _rotate,GetStateData(state)));
        default:
          throw new ArgumentOutOfRangeException(nameof(state), state, null);
      }
      
      /*
        states.Add(typeof(PlayerHurtState),new PlayerHurtState(_stateMachine, "IsImpact", _animator, _hero, _impactData.ImpactCooldown));
      states.Add(typeof(PlayerIdleShieldState),new PlayerIdleShieldState(_stateMachine, "IsBlocking", _animator, _hero));
     
      states.Add(typeof(PlayerRollState), new PlayerRollState(_stateMachine, "IsRoll", _animator, _hero, _move, _stamina));
      states.Add(typeof(PlayerShieldImpactState), new PlayerShieldImpactState(_stateMachine, "IsShieldImpact", _animator, _hero, _impactData.ShieldImpactCooldown));
      states.Add(typeof(PlayerShieldMoveState), new PlayerShieldMoveState(_stateMachine, "IsBlocking", "MoveY", _animator, _hero, _move, _rotate));
      states.Add(typeof(PlayerDeathState), new PlayerDeathState(_stateMachine, "IsDead", _animator, _hero));
       return ((typeof(PlayerAttackState),new PlayerAttackState(_stateMachine, "IsSimpleAttack", _animator, _hero, _attack, _attackData,
        _stamina));
        */
    }

    private HeroStateData GetStateData(HeroState state)
    {
      if (_statesData.ContainsKey(state))
        return _statesData[state];
      return new HeroStateData();
    }
  }
}