namespace StateMachines
{
  public class StateMachine
  {
    private BaseStateMachineState currentState;

    public BaseStateMachineState State => currentState;

    public void Initialize(BaseStateMachineState state)
    {
      currentState = state;
      currentState.Enter();
    }

    public void ChangeState(BaseStateMachineState newState)
    {
      ExitState();
      Initialize(newState);
    }

    public void InterruptState(BaseStateMachineState newState)
    {
      InterruptState();
      Initialize(newState);
    }

    private void ExitState() => 
      currentState.Exit();

    private void InterruptState() => 
      State.Interrupt();
  }
}