using StateMachines.Player;
using StateMachines.Player.Base;

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

    public void LogicUpdate() => 
      State.LogicUpdate();

    public void AnimationTriggered() => 
      State.AnimationTriggered();

    public void InterruptState(IHeroBaseUpMachineState upState, IHeroBaseSubStateMachineState state)
    {
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