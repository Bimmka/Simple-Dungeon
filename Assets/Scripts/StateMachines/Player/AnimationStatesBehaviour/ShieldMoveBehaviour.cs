﻿using UnityEngine;

namespace StateMachines.Player.AnimationStatesBehaviour
{
  public class ShieldMoveBehaviour : BaseMoveBehaviour
  {
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      base.OnStateEnter(animator, stateInfo, layerIndex);
      animator.SetLayerWeight(layerIndex, 1f);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      base.OnStateExit(animator, stateInfo, layerIndex);
      animator.SetLayerWeight(layerIndex, 0f);
    }
  }
}