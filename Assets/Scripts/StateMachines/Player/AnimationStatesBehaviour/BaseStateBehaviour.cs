using UnityEngine;

namespace StateMachines.Player.AnimationStatesBehaviour
{
  public abstract class BaseStateBehaviour : StateMachineBehaviour
  {
    public bool IsPlaying { get; protected set; }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      IsPlaying = true;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      IsPlaying = false;
    }
  }
}