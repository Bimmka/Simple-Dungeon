using UnityEngine;

namespace StateMachines.Player.AnimationStatesBehaviour
{
  public abstract class BaseAttackBehaviour : BaseStateBehaviour
  {
    [SerializeField] private float notInterruptedTime = 0.7f;

    public bool IsCanBeInterrupted { get; private set; }
    

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo,
      int layerIndex)
    {
      SetInCanBeInterrupted(CheckInterrupted(stateInfo.normalizedTime));
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      base.OnStateExit(animator, stateInfo, layerIndex);
      SetInCanBeInterrupted(false);
    }
    
    private void SetInCanBeInterrupted(bool isInterrupted) => 
      IsCanBeInterrupted = isInterrupted;
    private bool CheckInterrupted(float time) => 
      time >= notInterruptedTime;
  }
}