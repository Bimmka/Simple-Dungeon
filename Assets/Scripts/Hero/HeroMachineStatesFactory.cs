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
    private readonly StateMachineWithSubstates _stateMachine;
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
    private readonly HeroStatesStaticData _statesData;


    public HeroMachineStatesFactory(StateMachineWithSubstates stateMachine, HeroStateMachine hero, BattleAnimator animator,
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
      _statesData = statesData;
    }

    public void CreateStates(ref Dictionary<IHeroBaseUpMachineState, List<IHeroBaseSubStateMachineState>> states, ref Dictionary<Type,IHeroBaseSubStateMachineState> substates)
    {
      List<IHeroBaseSubStateMachineState> subStates = new List<IHeroBaseSubStateMachineState>(10);
      IHeroBaseUpMachineState upState;
      for (int i = 0; i < _statesData.StateDatas.Count; i++)
      {
        upState = CreateUpState(_statesData.StateDatas[i].UpState);
        subStates = CreateSubStates(upState, _statesData.StateDatas[i].SubstatesData, ref substates);
        states.Add(upState, subStates);
      }
    }

    private IHeroBaseUpMachineState CreateUpState(HeroUpState state)
    {
      switch (state)
      {
        case HeroUpState.Move:
          return new HeroMoveUpMachineState(_stateMachine, _hero, _coroutineRunner, _animator);
        case HeroUpState.Rotate:
          return new HeroRotatingUpMachineState(_stateMachine, _hero, _coroutineRunner);
        default:
          throw new ArgumentOutOfRangeException(nameof(state), state, null);
      }
    }

    private List<IHeroBaseSubStateMachineState> CreateSubStates(IHeroBaseUpMachineState upState, HeroStateData[] substatesData, ref Dictionary<Type,IHeroBaseSubStateMachineState> substates)
    {
      List<IHeroBaseSubStateMachineState> subStates = new List<IHeroBaseSubStateMachineState>(10);
      (Type, IHeroBaseSubStateMachineState) createdState;
      for (int i = 0; i < substatesData.Length; i++)
      {
        createdState = CreateSubState(upState, substatesData[i]);
        upState.AddSubstate(createdState.Item2);
        subStates.Add(createdState.Item2);
        substates.Add(createdState.Item1, createdState.Item2);
      }

      return subStates;
    }

    private (Type, IHeroBaseSubStateMachineState) CreateSubState(IHeroBaseUpMachineState upState, HeroStateData data)
    {
      switch (data.State)
      {
        case HeroState.Idle:
          return (typeof(HeroIdleState), new HeroIdleState((HeroMoveUpMachineState) upState, _hero,  _animator, "IsIdle", data));
        case HeroState.Walk:
          return (typeof(HeroWalkState), new HeroWalkState((HeroMoveUpMachineState) upState, _hero, _animator, "IsIdle", data, "MoveX", _move, _rotate, _moveStaticData));
        case HeroState.Run:
          return (typeof(HeroRunState), new HeroRunState( (HeroMoveUpMachineState) upState, _hero, _animator, "IsIdle", data, "MoveX", _stamina, _rotate, _move, _moveStaticData));
        case HeroState.Roll:
          return (typeof(HeroRollState), new HeroRollState( (HeroRollUpMachineState) upState, _hero, _animator, "IsRoll", data, _move, _stamina));
        case HeroState.Rotating:
          return (typeof(HeroRotatingState), new HeroRotatingState((HeroRotatingUpMachineState) upState, _hero, _animator, "IsRotating", data, "RotateX", "RotateY", _rotate));
        default:
          throw new ArgumentOutOfRangeException(nameof(data.State), data.State, null);
      }
    }
  }
}