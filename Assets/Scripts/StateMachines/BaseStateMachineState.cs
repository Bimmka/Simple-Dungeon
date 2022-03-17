namespace StateMachines
{
  public abstract class BaseStateMachineState
  {
    protected int animationName;

    public abstract bool IsCanBeInterapted(int weight);
    
    public abstract int Weight { get; }

    public virtual void Enter() => 
      Check();

    public virtual void Check() {}

    public virtual void LogicUpdate(){}

    public virtual void PhysicsUpdate() => 
      Check();
    
    public virtual void Exit() { }
    public virtual void Interapt() { }

    public virtual void TriggerAnimation() { }
  }
}