using StateMachines.Player;
using StateMachines.Player.Base;
using UnityEngine;

namespace StateMachines
{
  public class StateMachineWithSubstates
  {
    private IHeroBaseUpMachineState currentState;

    public IHeroBaseUpMachineState State => currentState;

    public void Initialize(IHeroBaseUpMachineState state, IHeroBaseSubStateMachineState substate)
    {
      currentState = state;
      InitializeCurrentState(substate);
    }

    public void ChangeState(IHeroBaseUpMachineState state, IHeroBaseSubStateMachineState substate)
    {
      if (State.IsAnimationInit == false)
        return;
      
      State.Exit();
      currentState = state;
      InitializeCurrentState(substate);
    }

    public void LogicUpdate()
    {
      Debug.Log($"<color=yellow>Logic Update {State.GetType()}</color>");
      State.LogicUpdate();
    }

    public void AnimationTriggered() => 
      State.AnimationTriggered();

    public void InterruptState(IHeroBaseUpMachineState upState, IHeroBaseSubStateMachineState state)
    {
      Debug.Log($"<color=green>Interrupt state {State.GetType()}. IsInit {State.IsAnimationInit}</color>");
      if (State.IsAnimationInit == false)
        return;
      
      State.InterruptState();
      currentState = upState;
      InitializeCurrentState(state);
    }

    private void InitializeCurrentState(IHeroBaseSubStateMachineState state) => 
      State.Initialize(state);
  }
}