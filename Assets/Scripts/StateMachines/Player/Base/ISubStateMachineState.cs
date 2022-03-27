namespace StateMachines.Player.Base
{
  public interface ISubStateMachineState
  {
    int Weight { get; }
    bool IsCanBeInterrupted(int weight);
    void Enter();
    void LogicUpdate();
    void Interrupt();
    void Exit();
  }
}