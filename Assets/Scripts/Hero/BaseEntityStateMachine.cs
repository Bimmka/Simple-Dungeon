using UnityEngine;

namespace Hero
{
  
  public abstract class BaseEntityStateMachine : MonoBehaviour
  {

    protected virtual void Initialize()
    {
      CreateStateMachine();
      CreateStates();
      SetDefaultState();
      
      Subscribe();
    }

    private void OnDestroy() => 
      Cleanup();


    protected virtual void Subscribe() {}

    protected virtual void Cleanup() {}
    protected abstract void Update();

    protected abstract void CreateStates();

    protected abstract void SetDefaultState();

    protected abstract void CreateStateMachine();

    protected abstract void AnimationTriggered();
  }
}