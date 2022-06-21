using StateMachines.Player.Attack;

namespace StateMachines.Player.Base
{
  public interface IHeroBaseSubStateMachineState
  {
    int Weight { get; }
    bool IsAnimationInit { get; }
    bool IsCanBeInterrupted(int weight);
    void Enter();
    void LogicUpdate();
    void Interrupt();
    void Exit();
    void AnimationTriggered();
    void SetAnimationFinishType(StateFinishType type);
  }
}