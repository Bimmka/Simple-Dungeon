namespace StateMachines.Player.Base
{
  public interface IHeroBaseUpMachineState
  {
    bool IsAnimationInit { get; }
    void AddSubstate(IHeroBaseSubStateMachineState state);
    void Exit();
    void Initialize(IHeroBaseSubStateMachineState state);
    void LogicUpdate();
    void AnimationTriggered();
    bool IsCanBeInterrupted(int weight);
    void ChangeState(IHeroBaseSubStateMachineState to);
    void InterruptState(IHeroBaseSubStateMachineState to);
    float ClipLength(PlayerActionsType actionsType);
    void InterruptState();
    bool IsSameState(IHeroBaseSubStateMachineState state);
  }
}