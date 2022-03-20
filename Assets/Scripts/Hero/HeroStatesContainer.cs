﻿using System;
using System.Collections.Generic;
using StateMachines.Player;
using UnityEngine.UIElements;

namespace Hero
{
  public class HeroStatesContainer
  {
    private readonly HeroMachineStatesFactory _factory;
    private Dictionary<Type, HeroBaseMachineState> _states; 

    public HeroStatesContainer(HeroMachineStatesFactory factory)
    {

      _states = new Dictionary<Type, HeroBaseMachineState>(10);
      _factory = factory;
    }

    public void CreateState()
    {
      _factory.CreateStates(ref _states);
    }

    public TState GetState<TState>() where TState : HeroBaseMachineState
    {
      return _states[typeof(TState)] as TState;
    }
  }
}