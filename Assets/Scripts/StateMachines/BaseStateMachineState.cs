namespace StateMachines
{
  public class BaseStateMachineState
  {
    protected int animationName;

    public virtual void Enter() => 
      Check();

    public virtual void Check() {}
    
    public virtual void LogicUpdate(){}

    public virtual void PhysicsUpdate() => 
      Check();

    public virtual void Exit() { }

    public virtual void AnimationTrigger() { }

    public virtual void AnimationTriggerFinish() { }
  }
}