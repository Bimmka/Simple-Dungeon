using Animations;
using StateMachines;
using UnityEngine;

namespace Hero
{
  
  public abstract class BaseEntityStateMachine : MonoBehaviour
  {
    [SerializeField] protected SimpleAnimator simpleAnimator;
        
    protected StateMachine stateMachine;
        
    private void Awake()
    {
      CreateStateMachine();
      CreateStates();
      SetDefaultState();
      Subscribe();
    }

    private void OnDestroy() => 
      Cleanup();

    private void Update() => 
      stateMachine.State.LogicUpdate();

    protected virtual void Subscribe() => 
      simpleAnimator.Triggered += AnimationTriggered;

    protected virtual void Cleanup() => 
      simpleAnimator.Triggered -= AnimationTriggered;

    protected abstract void CreateStates();

    protected abstract void SetDefaultState();

    private void CreateStateMachine() => 
      stateMachine = new StateMachine();

    private void AnimationTriggered() => 
      stateMachine.State.AnimationTrigger();
  }
}