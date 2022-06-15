using System;
using System.Collections.Generic;
using Animations;
using Services;
using StateMachines;
using StateMachines.Player;
using StateMachines.Player.AnimationStatesBehaviour;
using StateMachines.Player.Attack;
using StateMachines.Player.Base;
using StateMachines.Player.Move;
using StateMachines.Player.Roll;
using StateMachines.Player.Rotating;
using StaticData.Hero.Attacks;
using StaticData.Hero.Components;
using StaticData.Hero.States.Base;
using UnityEngine;

namespace Hero
{
  public class HeroMachineStatesFactory
  {
    private readonly StateMachineWithSubstates _stateMachine;
    private readonly HeroStateMachine _hero;
    private readonly HeroAnimator _animator;
    private readonly HeroMove _move;
    private readonly HeroAttack _attack;
    private readonly HeroRotate _rotate;
    private readonly AttacksStaticData _attackData;
    private readonly HeroStamina _stamina;
    private readonly HeroImpactsStaticData _impactData;
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly HeroMoveStaticData _moveStaticData;
    private readonly HeroStatesStaticData _statesData;
    private readonly AnimatorStateBehaviourContainer _behaviourContainer;


    public HeroMachineStatesFactory(StateMachineWithSubstates stateMachine, HeroStateMachine hero, HeroAnimator animator,
      HeroMove move,
      HeroAttack attack, HeroRotate rotate, AttacksStaticData attackData, HeroStamina stamina,
      HeroImpactsStaticData impactData, ICoroutineRunner coroutineRunner, HeroMoveStaticData moveStaticData, HeroStatesStaticData statesData, 
      AnimatorStateBehaviourContainer behaviourContainer)
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
      _behaviourContainer = behaviourContainer;
    }

    public void CreateStates(
      ref Dictionary<IHeroBaseUpMachineState, List<IHeroBaseSubStateMachineState>> states,
      ref Dictionary<Type, IHeroBaseSubStateMachineState> substates, ref Dictionary<AttackType, HeroAttackSubState> attackStates)
    {
      List<IHeroBaseSubStateMachineState> subStates = new List<IHeroBaseSubStateMachineState>(10);
      IHeroBaseUpMachineState upState;
      for (int i = 0; i < _statesData.StateDatas.Count; i++)
      {
        upState = CreateUpState(_statesData.StateDatas[i].UpState);
        subStates = CreateSubStates(upState, _statesData.StateDatas[i].SubstatesData, ref substates, ref attackStates);
        states.Add(upState, subStates);
      }
    }

    private IHeroBaseUpMachineState CreateUpState(HeroParentStateType state)
    {
      switch (state)
      {
        case HeroParentStateType.Move:
          return new HeroMoveUpMachineState(_stateMachine, _hero, _coroutineRunner, _animator, "MoveX");
        case HeroParentStateType.Rotate:
          return new HeroRotatingUpMachineState(_stateMachine, _hero, _coroutineRunner, _animator, "RotateX", "RotateY");
        case HeroParentStateType.Roll:
          return new HeroRollUpMachineState(_stateMachine, _hero, _coroutineRunner);
        case HeroParentStateType.Attack:
          return new HeroAttackUpMachineState(_stateMachine, _hero, _coroutineRunner);
        default:
          throw new ArgumentOutOfRangeException(nameof(state), state, null);
      }
    }

    private List<IHeroBaseSubStateMachineState> CreateSubStates(IHeroBaseUpMachineState upState, HeroBaseStateData[] substatesData, 
      ref Dictionary<Type, IHeroBaseSubStateMachineState> subStates, ref Dictionary<AttackType, HeroAttackSubState> attackStates)
    {
      List<IHeroBaseSubStateMachineState> createdSubStates = new List<IHeroBaseSubStateMachineState>(10);
      (Type, IHeroBaseSubStateMachineState) createdState;
      for (int i = 0; i < substatesData.Length; i++)
      {
        createdState = CreateSubState(upState, substatesData[i], ref attackStates);
        upState.AddSubstate(createdState.Item2);
        createdSubStates.Add(createdState.Item2);
        subStates.Add(createdState.Item1, createdState.Item2);
      }

      return createdSubStates;
    }

    private (Type, IHeroBaseSubStateMachineState) CreateSubState(IHeroBaseUpMachineState upState, HeroBaseStateData data, ref Dictionary<AttackType, HeroAttackSubState> attackStates)
    {
      switch (data.State)
      {
        case HeroState.Idle:
          return (typeof(HeroIdleState), new HeroIdleState((HeroMoveUpMachineState) upState, _hero,  _animator, "IsIdle", 
            "Speed", (HeroMoveStateData) data, _behaviourContainer.GetStateBehaviour<MoveBehaviour>()));
        
        case HeroState.Walk:
          return (typeof(HeroWalkState), new HeroWalkState((HeroMoveUpMachineState) upState, _hero, _animator, "IsIdle", 
            "Speed", (HeroMoveStateData) data,_behaviourContainer.GetStateBehaviour<MoveBehaviour>(), _move, _rotate, _moveStaticData));
        
        case HeroState.Run:
          return (typeof(HeroRunState), new HeroRunState( (HeroMoveUpMachineState) upState, _hero, _animator, "IsIdle", 
            "Speed", (HeroMoveStateData) data,_behaviourContainer.GetStateBehaviour<MoveBehaviour>(), _stamina, _rotate, _move, _moveStaticData));
        
        case HeroState.Roll:
          return (typeof(HeroRollSubState), new HeroRollSubState( (HeroRollUpMachineState) upState, _hero, _animator, "IsRoll",
            data, _behaviourContainer.GetStateBehaviour<RollBehaviour>(), _move, _stamina));
        
        case HeroState.Rotating:
          return (typeof(HeroRotatingSubState), new HeroRotatingSubState((HeroRotatingUpMachineState) upState, _hero, _animator, 
            "IsRotating", (HeroRotateStateData) data,_behaviourContainer.GetStateBehaviour<RotatingBehaviour>(), _rotate));
        
        case HeroState.SimpleAttack:
          HeroAttackState attack = new HeroAttackState((HeroAttackUpMachineState) upState, _hero, _animator, "IsSimpleAttack",
            data, _behaviourContainer.GetStateBehaviour<AttackBehaviour>(), _attack, AttackData(AttackType.BaseAttack), _stamina);
          attackStates.Add(AttackType.BaseAttack, attack);
          return (attack.GetType(), attack);
        
        case HeroState.ComboAttack:
          HeroComboAttackState comboAttack = new HeroComboAttackState((HeroAttackUpMachineState) upState, _hero, _animator, 
            "IsComboAttack", data, _behaviourContainer.GetStateBehaviour<ComboAttackBehaviour>(), _attack, AttackData(AttackType.Combo),
            _stamina);
          attackStates.Add(AttackType.Combo, comboAttack);
          return (comboAttack.GetType(), comboAttack);
        
        case HeroState.FatalityAttack:
          HeroFatalityAttackState fatalityAttack = new HeroFatalityAttackState((HeroAttackUpMachineState) upState, _hero, _animator, 
            "IsFatalityAttack", data, _behaviourContainer.GetStateBehaviour<FatalityAttackBehaviour>(), _attack, 
            AttackData(AttackType.Fatality), _stamina);
          attackStates.Add(AttackType.Fatality, fatalityAttack);
          return (fatalityAttack.GetType(), fatalityAttack);
        
        default:
          throw new ArgumentOutOfRangeException($"Incorrect state {data.State.ToString()}", data.State, null);
      }
    }

    private AttackStaticData AttackData(AttackType attack)
    {
      if (_attackData.AttacksData.ContainsKey(attack))
        return _attackData.AttacksData[attack];
      return new AttackStaticData();
    }
  }
}