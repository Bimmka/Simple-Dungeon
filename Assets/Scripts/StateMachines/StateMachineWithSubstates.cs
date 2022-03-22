using StateMachines.Player;

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
      State.Exit();
      currentState = state;
      InitializeCurrentState(substate);
    }

    public void LogicUpdate() => 
      State.LogicUpdate();

    private void InitializeCurrentState(IHeroBaseSubStateMachineState state) => 
      State.Initialize(state);

    public void AnimationTriggered() => 
      State.AnimationTriggered();

    public void InterruptState(IHeroBaseUpMachineState upState, IHeroBaseSubStateMachineState state)
    {
      State.InterruptState();
      currentState = upState;
      InitializeCurrentState(state);
    }
  }
}