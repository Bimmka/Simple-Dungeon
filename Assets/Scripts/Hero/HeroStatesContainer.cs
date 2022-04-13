using System;
using System.Collections.Generic;
using StateMachines.Player;
using StateMachines.Player.Attack;
using StateMachines.Player.Base;

namespace Hero
{
  public class HeroStatesContainer
  {
    private readonly HeroMachineStatesFactory _factory;
    private Dictionary<IHeroBaseUpMachineState, List<IHeroBaseSubStateMachineState>> _states;
    private Dictionary<Type, IHeroBaseSubStateMachineState> _subStates;
    private Dictionary<AttackType, HeroAttackSubState> _attackStates;

    public HeroStatesContainer(HeroMachineStatesFactory factory)
    {
      _states = new Dictionary<IHeroBaseUpMachineState, List<IHeroBaseSubStateMachineState>>(10);
      _subStates = new Dictionary<Type, IHeroBaseSubStateMachineState>(10);
      _attackStates = new Dictionary<AttackType, HeroAttackSubState>(10);
      _factory = factory;
    }

    public void CreateStates() => 
      _factory.CreateStates(ref _states, ref _subStates, ref _attackStates);


    public IHeroBaseSubStateMachineState GetState<TState>() where TState : IHeroBaseSubStateMachineState
    {
      if (_subStates.ContainsKey(typeof(TState)))
        return _subStates[typeof(TState)];
      return null;
    }

    public IHeroBaseUpMachineState GetUpStateForSubstate(IHeroBaseSubStateMachineState substate) 
    {
      foreach (KeyValuePair<IHeroBaseUpMachineState,List<IHeroBaseSubStateMachineState>> valuePair in _states)
      {
        if (valuePair.Value.Contains(substate))
          return valuePair.Key;
      }

      return null;
    }


    public HeroAttackSubState GetAttackState(AttackType type)
    {
      if (_attackStates.ContainsKey(type))
        return _attackStates[type];

      return default;
    }
  }
}