using UnityEngine;

namespace StateMachines.Player.AnimationStatesBehaviour
{
  public abstract class BaseAttackBehaviour : BaseStateBehaviour
  {
    [SerializeField] private Vector2 notInterruptedRange;

    public bool IsCanBeInterrupted { get; private set; }


    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      base.OnStateEnter(animator, stateInfo, layerIndex);
      animator.SetLayerWeight(layerIndex, 1f);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo,
      int layerIndex)
    {
      SetInCanBeInterrupted(CheckInterrupted(stateInfo.normalizedTime));
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      base.OnStateExit(animator, stateInfo, layerIndex);
      SetInCanBeInterrupted(false);
      animator.SetLayerWeight(layerIndex, 0f);
    }
    
    private void SetInCanBeInterrupted(bool isInterrupted) => 
      IsCanBeInterrupted = isInterrupted;
    private bool CheckInterrupted(float time) => 
      notInterruptedRange.x > time || notInterruptedRange.y < time;
  }
}